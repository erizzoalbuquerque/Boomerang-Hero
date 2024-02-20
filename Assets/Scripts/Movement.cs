using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 4f;
    [SerializeField] float _knockbackDuration = 0.5f;
    [SerializeField] float _knockbackDistance = 1f;

    Rigidbody2D _rb;
    Vector2 _movingDirection;
    Tween _knockbackTween;

    public Vector2 MovingDirection { get => _movingDirection; }


    // Reserverd Methods

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        _movingDirection = Vector2.zero;
    }

    // Public Methods

    public void Move(Vector2 direction)
    {
        _movingDirection = direction.normalized;
        _rb.velocity = _movingDirection * _maxSpeed;
    }

    public void ApplyKnockback(Vector2 direction)
    {
        StartCoroutine(KnockbackCoroutine(direction));
    }

    IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        _rb.velocity = Vector3.zero;

        Vector3 knockBackVector = this.transform.position + (Vector3)direction.normalized * _knockbackDistance;
        _knockbackTween = _rb.DOMove(knockBackVector, _knockbackDuration).SetEase(Ease.OutCubic);

        yield return _knockbackTween.WaitForCompletion();
    }

    private void OnDestroy()
    {
        if (_knockbackTween != null) 
        {
            _knockbackTween.Kill();
        }
    }
}
