using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _duration = 0.6f;
    [SerializeField] float _numberOfBlinks = 3f;

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
        if (time <= _duration || _duration < 0f)
        {
            //Blink
            float value = Mathf.Sin(time * _numberOfBlinks/_duration * 2 * Mathf.PI);

            if (value >= 0)
                _spriteRenderer.enabled = false;
            else
                _spriteRenderer.enabled = true;
        }
        else
        {
            StopBlinking();
        }
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
