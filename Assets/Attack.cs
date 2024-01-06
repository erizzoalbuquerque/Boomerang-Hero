using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] float _intensity = 1f;
    [SerializeField] LayerMask _layerMask;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (! _layerMask.Contains(c.gameObject.layer))
            return;

        DamageHitbox damageable = c.GetComponent<DamageHitbox>();
        if (damageable == null)
            return;

        damageable.Hit(new HitInfo(c.transform.position - this.transform.position, _intensity));
    }
}
