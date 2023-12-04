using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _knockbackDistance = 0.3f;

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
        GetComponent<Blinking>().StartBlinking();
        GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulseWithForce(0.1f);
        this.transform.DOMove(this.transform.position + direction * _knockbackDistance, 0.5f).SetEase(Ease.OutCubic);
    }
}
