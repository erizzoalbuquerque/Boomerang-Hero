using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _frequency = 5f;

    bool _isBlinking;
    float _startBlinkingTime;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBlinking == false)
            return; 

        float time = Time.time - _startBlinkingTime;

        float value = Mathf.Sin(time * _frequency * 2 * Mathf.PI);

        if (value >= 0)
            _spriteRenderer.enabled = false;
        else
            _spriteRenderer.enabled = true;
    }

    [ContextMenu("StartBlinking")]
    public void StartBlinking()
    {
        _isBlinking = true;
        _startBlinkingTime = Time.time;
    }

    public void StopBlinking() 
    {
        _spriteRenderer.enabled = true;
        _isBlinking = false;
    }
}
