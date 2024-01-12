using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] Boomerang _boomerang;
    [SerializeField] DamageHitbox _damageHitbox;

    [SerializeField] HeroMovement _heroMovement;

    [SerializeField] float _boomerangThrowOriginDistance = 0.5f;


    // Reserverd Methods

    void Awake()
    {

    }


    void Start()
    {

    }


    // Public Methods

    public void ThrowBoomerang()
    {
        Vector2 nomalizedDirection = _heroMovement.LastNonZeroDirection.normalized;

        Vector2 startPosition = (Vector2) (this.transform.position) + (_boomerangThrowOriginDistance * nomalizedDirection);

        _boomerang.Throw(nomalizedDirection, startPosition);
    }


    // Private Methods

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
