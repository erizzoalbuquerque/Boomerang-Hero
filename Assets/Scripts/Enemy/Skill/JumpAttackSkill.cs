using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpAttackSkill : Skill
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] AttackHitbox _attackHitbox;

    [SerializeField] float _jumpDuration = 1.5f;
    [SerializeField] float _jumpWaitDuration = 0.5f;
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

    private void OnDestroy()
    {
        _tween?.Kill();
    }

    public override bool Execute(Vector3 direction)
    {
        if (_character.IsStaggered)
            return false;

        if (Time.time - _coolDownStartTime < _coolDown)
            return false;
        
        ApplyJump(direction);

        return true;
    }


    public override void Cancel()
    {
        base.Cancel();

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
        Cancel();
        _coroutine = StartCoroutine(JumpCoroutine(direction));
    }

    IEnumerator JumpCoroutine(Vector2 direction)
    {
        _attackHitbox.enabled = true;

        _coolDownStartTime = Time.time;

        _rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(_jumpWaitDuration);

        Vector3 jumpVector = this.transform.position + (Vector3)direction.normalized * _jumpDistance;
        _tween = _rb.DOMove(jumpVector, _jumpDuration).SetEase(Ease.OutCubic);

        yield return _tween.WaitForCompletion();

        _attackHitbox.enabled = false;
    }
}
; 