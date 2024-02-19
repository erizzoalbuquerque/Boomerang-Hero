using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 4f;
    [SerializeField] float _knockbackDuration = 0.5f;
    [SerializeField] float _knockbackDistance = 0.3f;

    Rigidbody2D _rb;
    Vector2 _movingDirection;


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
        Tween tween = _rb.DOMove(knockBackVector, _knockbackDuration).SetEase(Ease.OutCubic);

        yield return tween.WaitForCompletion();
    }
}
