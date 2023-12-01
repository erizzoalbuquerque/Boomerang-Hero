using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onEnabled;
    [SerializeField] UnityEvent _onDisabled;

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
        _onEnabled.Invoke();
    }

    private void OnDisable()
    {
        _onDisabled.Invoke();
    }
}
