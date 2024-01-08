using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event Action<HitInfo> GotHit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveHit(Vector3 direction)
    {
        GotHit.Invoke(new HitInfo(direction));
        GetComponent<Blinking>().StartBlinking();
        GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulseWithForce(0.1f);
    }
}
