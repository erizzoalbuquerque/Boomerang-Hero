using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Health _health;
    [SerializeField] protected Movement _movement;
    [SerializeField] protected DamageHitbox _damageHitbox;

    [Header("Settings")]
    [SerializeField] protected float _invencibilityDuration = 0.5f;
    [SerializeField] protected float _staggerDuration = 0.3f;

    protected Vector2 _lastNonZeroInputDirection;
    protected bool _isStaggered = false;
    protected bool _isInvencible = false;


    //Coroutines
    Coroutine _invencibilityCoroutine;
    Coroutine _staggerCoroutine;

    public bool IsInvencible { get => _isInvencible; }
    public bool IsStaggered { get => _isStaggered; }
    public Vector2 LastNonZeroInputDirection { get => _lastNonZeroInputDirection; }


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
        if (_damageHitbox != null)
            _damageHitbox.GotHit += OnHit;
    }

    void OnDisable()
    {
        if (_damageHitbox != null)
            _damageHitbox.GotHit -= OnHit;
    }


    // Public Methods

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
            _lastNonZeroInputDirection = direction;

        if (_isStaggered == false)
            _movement.Move(direction);
    }


    // Private Methods

    void OnHit(HitInfo hitInfo)
    {
        if (_isInvencible == true)
            return;

        _health?.ApplyDamage(hitInfo.Instensity);

        _movement.ApplyKnockback(hitInfo.Direction);

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
