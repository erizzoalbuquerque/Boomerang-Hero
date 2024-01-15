using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected AttackHitbox _attackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool Do(Vector3 direction)
    {
        return true;
    }


    public virtual void Halt()
    {
        
    }
}