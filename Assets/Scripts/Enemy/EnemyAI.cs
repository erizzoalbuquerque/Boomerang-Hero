using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Hero _hero;
    [SerializeField] Enemy _enemy;

    [SerializeField] EnemyMovement _enemyMovement;

    [SerializeField] ContactAttackSkill _contactAttackSkill;
    [SerializeField] JumpAttackSkill _jumptAttackSkill;

    [SerializeField] float _proximityThreshold = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 heroDirection = _hero.transform.position - _enemy.transform.position;

        if (heroDirection.magnitude < _proximityThreshold)
        {
            Jump(heroDirection);

        } else
        {
            MoveTowardsHero(heroDirection);

        }
        
    }


    void Jump(Vector2 direction)
    {
        _jumptAttackSkill.Do(direction);
    }


    void MoveTowardsHero(Vector2 direction)
    {
        _enemyMovement.Move(direction);
    }
}
