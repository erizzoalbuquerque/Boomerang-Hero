using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] float _intensity = 1f;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] UnityEvent _collidedWithHit;
    [SerializeField] UnityEvent _collidedWithAnythingButLayer;

    public bool isActive = true;

    private void Awake()
    {
        isActive = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!isActive)
            return;

        if (this.transform.root == c.gameObject.transform.root)
            return;

        if (!_layerMask.Contains(c.gameObject.layer))
        {
            _collidedWithAnythingButLayer.Invoke();
            return;
        }

        DamageHitbox damageable = c.GetComponent<DamageHitbox>();
        if (damageable == null)
        {
            _collidedWithAnythingButLayer.Invoke();
            return;
        }

        damageable.Hit(new HitInfo(c.transform.position - this.transform.position, _intensity));

        _collidedWithHit.Invoke();
    }
}
