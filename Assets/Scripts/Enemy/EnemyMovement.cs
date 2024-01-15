using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] EnemyStagger _enemyStagger;

    [SerializeField] float _maxSpeed = 1f;

    [SerializeField] float _knockbackTime= 0.5f;
    [SerializeField] float _knockbackDistance = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Move(Vector2 direction)
    {
        if (_enemyStagger.IsStaggered)
            return;

        _rb.velocity = direction.normalized  * _maxSpeed;
    }


    public void Knockback(Vector2 direction)
    {
        StartCoroutine(KnockbackCoroutine(direction));
    }

    IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        _rb.velocity = Vector3.zero;

        Vector3 knockBackVector = this.transform.position + (Vector3)direction.normalized * _knockbackDistance;
        Tween tween = _rb.DOMove(knockBackVector, _knockbackTime).SetEase(Ease.OutCubic);

        yield return tween.WaitForCompletion();
    }

}
