using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float value)
    {
        _health -= value;

        if (_health <= 0)
            Kill();
    }

    virtual protected void Kill()
    {
        Destroy(gameObject);
    }
}
