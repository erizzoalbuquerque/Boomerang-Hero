using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangVisual : MonoBehaviour
{
    [SerializeField] Boomerang _boomerang;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _rotationSpeed;
    [SerializeField] ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    void EmitDashParticles()
    {
        if (_particleSystem == null)
            return;

        _particleSystem.Play();
    }

    private void OnEnable()
    {
        _boomerang.dashStarted += EmitDashParticles;
    }

    private void OnDisable()
    {
        _boomerang.dashStarted -= EmitDashParticles;
    }
}
