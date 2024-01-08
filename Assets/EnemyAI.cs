using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] EnemyMovement _enemyMovement;
    [SerializeField] Hero _hero;

    [SerializeField] float _proximityThreshold = 2f;

    [SerializeField] ContactAttackSkill _contactAttackSkill;
    [SerializeField] JumpAttackSkill _jumptAttackSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float heroDistance = (_hero.transform.position - _enemy.transform.position).magnitude;

        if (heroDistance < _proximityThreshold)
        {
            Jump();

        } else
        {
            MoveTowardsHero();

        }
        
    }

    void Jump()
    {
        _jumptAttackSkill.Do(_enemyMovement, _hero.transform.position - this.transform.position);
    }

    void MoveTowardsHero()
    {
        _enemyMovement.Move(_hero.transform.position - this.transform.position);
    }
}
