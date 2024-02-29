using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] bool _isActive = true;
    [SerializeField] float _intensity = 1f;
    [SerializeField] LayerMask _whatIsHittable;
    [SerializeField] LayerMask _whatIsSolid;

    [SerializeField] public UnityEvent CollidedWithHittable;
    [SerializeField] public UnityEvent CollidedWithSolid;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    private void Awake()
    {
        _isActive = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!_isActive)
            return;

        //Ignore Collision if both objects are under the same tree
        if (this.transform.root == c.gameObject.transform.root)
            return;

        if (_whatIsHittable.Contains(c.gameObject.layer))
        {
            DamageHitbox damageable = c.GetComponent<DamageHitbox>();
            if (damageable != null)
            {
                damageable.Hit(new HitInfo(c.transform.position - this.transform.position, _intensity));
                CollidedWithHittable.Invoke();
            }
        }

        if (_whatIsSolid.Contains(c.gameObject.layer))
        {
            CollidedWithSolid.Invoke();
        }        
    }
}
