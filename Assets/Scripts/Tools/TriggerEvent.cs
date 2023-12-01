using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers events when this Trigger Collider collides with another colliders. 
/// </summary>
public class TriggerEvent : MonoBehaviour
{       
    [SerializeField] LayerMask _colisionLayers;
    [SerializeField] UnityEvent _onTriggerEnter;
    [SerializeField] UnityEvent _onTriggerExit;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (_colisionLayers == (_colisionLayers | (1 << collider2D.gameObject.layer)))
        {
            _onTriggerEnter.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (_colisionLayers == (_colisionLayers | (1 << collider2D.gameObject.layer)))
        {
            _onTriggerExit.Invoke();
        }
    }
}
