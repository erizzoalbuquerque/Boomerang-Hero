using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] DamageHitbox _damageHitbox;

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
        print("Receive Hit");
        GetComponent<Blinking>().StartBlinking();
        GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulseWithForce(0.1f);
    }
}
