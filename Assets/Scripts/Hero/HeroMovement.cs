using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 4f;

    Rigidbody2D _rb;
    Vector2 _movingDirection;
    Vector2 _lastNonZeroDirection;

    public Vector2 MovingDirection { get => _movingDirection; }
    public Vector2 LastNonZeroDirection { get => _lastNonZeroDirection; }


    // Reserverd Methods

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        _movingDirection = Vector2.zero;
        _lastNonZeroDirection = Vector2.up;
    }


    void Update()
    {

    }


    void FixedUpdate()
    {
        Move();
    }


    // Public Methods

    public void SetDirection(Vector2 direction)
    {
        _movingDirection = direction.normalized;

        if (_movingDirection != Vector2.zero)
        {
            _lastNonZeroDirection = _movingDirection;
        }
    }


    // Private Methods

    void Move()
    {
        _rb.velocity = _movingDirection * _maxSpeed;
    }
}
