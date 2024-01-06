using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _maxSpeed = 1f;

    [SerializeField] float _staggerTime = 0.5f;

    [SerializeField] float _knockbackTime= 0.5f;
    [SerializeField] float _knockbackDistance = 0.3f;

    bool _canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        _enemy.GotHit += OnHit;
    }

    private void OnDisable()
    {
        _enemy.GotHit -= OnHit;
    }


    public void Move(Vector2 direction)
    {
        if (_canMove)
            _rb.velocity = direction.normalized  * _maxSpeed;
    }

    void OnHit(HitInfo hitInfo)
    {
        ApplyKnockback(hitInfo.Direction);
        ApplyStagger();
    }

    void ApplyKnockback(Vector2 direction)
    {
        StartCoroutine(KnockbackCoroutine(direction));
    }

    void ApplyStagger()
    {
        StartCoroutine(StaggerCoroutine());
    }

    IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        this._rb.velocity = Vector3.zero;
        Tween tween = this._rb.DOMove(this.transform.position + (Vector3) direction * _knockbackDistance, _knockbackTime).SetEase(Ease.OutCubic);

        yield return tween.WaitForCompletion();
    }

    IEnumerator StaggerCoroutine()
    {
        _canMove = false;
        yield return new WaitForSeconds(_staggerTime);
        _canMove = true;
    }
}
