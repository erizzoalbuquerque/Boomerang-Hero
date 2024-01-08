using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackSkill : Skill
{
    // Start is called before the first frame update
    void Start()
    {
        _attackHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Do(EnemyMovement enemyMovement, Vector3 direction)
    {
        _attackHitbox.enabled = true;
        enemyMovement.Jump(direction);
        _attackHitbox.enabled = false;
    }
}
