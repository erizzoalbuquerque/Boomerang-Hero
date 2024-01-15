using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyStagger _enemyStagger;
    [SerializeField] DamageHitbox _damageHitbox;
    [SerializeField] EnemyMovement _enemyMovement;

    [SerializeField] ContactAttackSkill _contactAttackSkill;
    [SerializeField] JumpAttackSkill _jumptAttackSkill;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void OnEnable()
    {
        _damageHitbox.GotHit += ReceiveHit;
    }


    void OnDisable()
    {
        _damageHitbox.GotHit -= ReceiveHit;
    }


    void ReceiveHit(HitInfo hitInfo)
    {
        _enemyStagger.Apply();
        _enemyMovement.Knockback(hitInfo.Direction);

        _contactAttackSkill.Halt();
        _jumptAttackSkill.Halt();

        GetComponent<Blinking>().StartBlinking();
        GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulseWithForce(0.1f);
    }
}
