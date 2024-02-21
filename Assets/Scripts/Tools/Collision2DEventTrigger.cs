using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers events when this Collider collides with another colliders. 
/// </summary>
public class Collision2DEventTrigger : MonoBehaviour
{
    [Space()]
    [Header("You'll probably need a Rigidbody!")]
    [Header("Also, remember to check if the collider is trigger or not")]
    [Space()]
    [Space()]

    [SerializeField] LayerMask _colisionLayers;
    [SerializeField] bool _isTriggerCollider = true;

    public UnityEvent _onCollisionEnter;
    public UnityEvent _onCollisionExit;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!_isTriggerCollider)
            return;

        if (_colisionLayers == (_colisionLayers | (1 << collider2D.gameObject.layer)))
        {
            _onCollisionEnter.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (!_isTriggerCollider)
            return;

        if (_colisionLayers == (_colisionLayers | (1 << collider2D.gameObject.layer)))
        {
            _onCollisionExit.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isTriggerCollider)
            return;

        if (_colisionLayers == (_colisionLayers | (1 << collision.gameObject.layer)))
        {
            _onCollisionEnter.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_isTriggerCollider)
            return;

        if (_colisionLayers == (_colisionLayers | (1 << collision.gameObject.layer)))
        {
            _onCollisionExit.Invoke();
        }
    }
}
