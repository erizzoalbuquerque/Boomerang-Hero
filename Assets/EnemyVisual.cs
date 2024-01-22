using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] Blinking _blinkingComponent;
    [SerializeField] Cinemachine.CinemachineImpulseSource _cinemachineImpulseSource;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _enemy.HitReceived += OnEnemyHitReceived;
    }

    private void OnDisable()
    {
        _enemy.HitReceived -= OnEnemyHitReceived;
    }

    void ApplyHitEffects()
    {
        _blinkingComponent.StartBlinking();
        _cinemachineImpulseSource.GenerateImpulseWithForce(0.1f);
    }

    void OnEnemyHitReceived()
    {
        ApplyHitEffects();
    }
}
