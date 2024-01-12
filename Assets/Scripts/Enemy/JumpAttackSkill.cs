using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackSkill : Skill
{
    float coolDown = 3f;
    float coolDownStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _attackHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Do(EnemyMovement enemyMovement, Vector3 direction)
    {
        if (Time.time - coolDownStartTime < coolDown)
            return false;

        _attackHitbox.enabled = true;

        coolDownStartTime = Time.time;
        enemyMovement.Jump(direction);

        _attackHitbox.enabled = false;

        return true;
    }
}
