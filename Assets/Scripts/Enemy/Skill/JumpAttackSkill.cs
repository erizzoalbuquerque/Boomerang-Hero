using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpAttackSkill : Skill
{
    [SerializeField] EnemyStagger _enemyStagger;
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] float _jumpTime = 1.5f;
    [SerializeField] float _jumpDistance = 6f;

    [SerializeField] float _coolDown = 2f;

    float _coolDownStartTime = 0f;

    Tween _tween;
    Coroutine _coroutine;


    // Start is called before the first frame update
    void Start()
    {
        _attackHitbox.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public override bool Do(Vector3 direction)
    {
        if (_enemyStagger.IsStaggered)
            return false;

        if (Time.time - _coolDownStartTime < _coolDown)
            return false;
        
        ApplyJump(direction);

        return true;
    }


    public override void Halt()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);

        _tween?.Kill();

        _rb.velocity = Vector3.zero;
        _attackHitbox.enabled = false;
        _coolDownStartTime = 0f;
    }


    void ApplyJump(Vector2 direction)
    {
        Halt();
        _coroutine = StartCoroutine(JumpCoroutine(direction));
    }


    IEnumerator JumpCoroutine(Vector2 direction)
    {
        _attackHitbox.enabled = true;

        _coolDownStartTime = Time.time;

        _rb.velocity = Vector3.zero;

        Vector3 jumpVector = this.transform.position + (Vector3)direction.normalized * _jumpDistance;
        _tween = _rb.DOMove(jumpVector, _jumpTime).SetEase(Ease.OutCubic);

        yield return _tween.WaitForCompletion();

        _attackHitbox.enabled = false;
    }
}
; 