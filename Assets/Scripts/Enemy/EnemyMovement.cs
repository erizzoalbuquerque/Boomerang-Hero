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

    [SerializeField] float _jumpTime = 2f;
    [SerializeField] float _jumpDistance = 3f;

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

    public void Jump(Vector2 direction)
    {
        if (_canMove)
            ApplyJump(direction);
    }

    void ApplyJump(Vector2 direction)
    {
        StartCoroutine(JumpCoroutine(direction));
    }

    void ApplyKnockback(Vector2 direction)
    {
        StartCoroutine(KnockbackCoroutine(direction));
    }

    void ApplyStagger()
    {
        StartCoroutine(StaggerCoroutine());
    }

    IEnumerator JumpCoroutine(Vector2 direction)
    {
        _canMove = false;
        _rb.velocity = Vector3.zero;
        Tween tween = _rb.DOMove(this.transform.position + (Vector3)direction.normalized * _jumpDistance, _jumpTime).SetEase(Ease.OutCubic);

        yield return tween.WaitForCompletion();
        _canMove = true;
    }

    IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        _rb.velocity = Vector3.zero;
        Tween tween = _rb.DOMove(this.transform.position + (Vector3) direction.normalized * _knockbackDistance, _knockbackTime).SetEase(Ease.OutCubic);

        yield return tween.WaitForCompletion();
    }

    IEnumerator StaggerCoroutine()
    {
        _canMove = false;
        yield return new WaitForSeconds(_staggerTime);
        _canMove = true;
    }
}
