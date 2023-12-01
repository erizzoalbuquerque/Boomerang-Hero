using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
    [SerializeField] UnityEvent _timeOut;

    float _timer;

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0f)
                _timeOut.Invoke();
        }        
    }

    void OnEnable()
    {
        Reset();
    }

    void Reset()
    {
        _timer = _duration;
    }
}
