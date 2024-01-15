using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttackSkill : Skill
{
    [SerializeField] EnemyStagger _enemyStagger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Do(Vector3 direction)
    {
        if (_enemyStagger.IsStaggered)
            return false;

        return true;
    }


    public void Halt()
    {
        
    }

}
