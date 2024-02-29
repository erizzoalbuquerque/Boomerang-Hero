using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] BaseCharacterController _characterController;
    [SerializeField] LayerMask _boomerangLayer;
    [SerializeField] Movement _movement;
    [SerializeField] Collider2D _shieldCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, _characterController.FacingDirection);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider != _shieldCollider)
            return;

        if (_boomerangLayer == (_boomerangLayer | (1 << collision.gameObject.layer)))
        {
            _movement.ApplyKnockback(collision.GetContact(0).normal);
        }
    }
}
