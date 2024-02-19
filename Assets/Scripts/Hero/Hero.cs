using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.PackageManager;

public class Hero : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Boomerang _boomerang;
    [SerializeField] DamageHitbox _damageHitbox;
    [SerializeField] HeroMovement _heroMovement;

    [Header("Settings")]
    [SerializeField] float _boomerangThrowOriginDistance = 0.5f;
    [SerializeField] float _invencibilityDuration = 0.5f;
    [SerializeField] float _staggerDuration = 0.3f;

    Vector2 _lastNonZeroInputDirection;
    bool _isStaggered = false;
    bool _isInvencible = false;


    //Coroutines
    Coroutine _invencibilityCoroutine;
    Coroutine _staggerCoroutine;

    public bool IsInvencible { get => _isInvencible; }
    public bool IsStaggered { get => _isStaggered; }


    // Reserverd Methods

    void Awake()
    {

    }


    void Start()
    {
        _lastNonZeroInputDirection = Vector2.up;

        _isStaggered = false;
        _isInvencible = false;
    }

    void OnEnable()
    {
        _damageHitbox.GotHit += OnHit;
    }

    void OnDisable()
    {
        _damageHitbox.GotHit -= OnHit;
    }


    // Public Methods

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
            _lastNonZeroInputDirection = direction;

        if (_isStaggered == false)
            _heroMovement.Move(direction);
    }

    public void ThrowBoomerang()
    {
        if (_isStaggered != false)
            return;

        Vector2 nomalizedDirection = _lastNonZeroInputDirection.normalized;

        Vector2 startPosition = (Vector2) (this.transform.position) + (_boomerangThrowOriginDistance * nomalizedDirection);

        _boomerang.Throw(nomalizedDirection, startPosition);
    }

    public void Teleport()
    {
        if (_boomerang.CanBePulled && _boomerang.IsThrown)
            this.transform.position = _boomerang.transform.position;
    }


    // Private Methods

    void OnHit(HitInfo hitInfo)
    {
        if (_isInvencible == true)
            return;

        _heroMovement.ApplyKnockback(hitInfo.Direction);

        if (_invencibilityCoroutine != null)
            StopCoroutine(_invencibilityCoroutine);
        _invencibilityCoroutine = StartCoroutine(InvencibilityCoroutine());

        if (_staggerCoroutine != null)
            StopCoroutine(_staggerCoroutine);
        _staggerCoroutine = StartCoroutine(StaggerCoroutine());
    }

   

    IEnumerator InvencibilityCoroutine()
    {
        _isInvencible = true;

        yield return new WaitForSeconds(_invencibilityDuration);

        _isInvencible = false;
    }

    IEnumerator StaggerCoroutine()
    {
        _isStaggered = true;

        yield return new WaitForSeconds(_staggerDuration);

        _isStaggered = false;
    }

}
