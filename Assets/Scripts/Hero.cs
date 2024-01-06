using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] Boomerang _boomerang;
    [SerializeField] DamageHitbox _damageHitbox;
    [SerializeField] float _maxSpeed = 1f;
    [SerializeField] float _boomerangThrowOriginDistance = 0.5f;

    Rigidbody2D _rb;
    Vector2 _movingDirection;
    Vector2 _lastNonZeroDirection;

    public Vector2 MovingDirection { get => _movingDirection; }
    public Vector2 LastNonZeroDirection { get => _lastNonZeroDirection;  }

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


    public void SetDirection (Vector2 direction)
    {
        _movingDirection = direction.normalized;

        if (_movingDirection != Vector2.zero)
        {
            _lastNonZeroDirection = _movingDirection;
        }
    }

    public void ThrowBoomerang()
    {
        Vector2 nomalizedDirection = LastNonZeroDirection.normalized;
        Vector2 startPosition = new Vector2(this.transform.position.x, this.transform.position.y) + (_boomerangThrowOriginDistance * nomalizedDirection);
        _boomerang.Throw(nomalizedDirection, startPosition);
    }

    void Move()
    {
        _rb.velocity = _movingDirection * _maxSpeed;
    }

    void OnHit(HitInfo hitInfo)
    {
        print("Hero Got Hit!");
    }

    private void OnEnable()
    {
        _damageHitbox.GotHit += OnHit;
    }

    private void OnDisable()
    {
        _damageHitbox.GotHit -= OnHit;
    }
}
