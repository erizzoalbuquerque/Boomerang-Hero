using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangVisual : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
