using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    [SerializeField] Hero _hero;
    [SerializeField] Enemy _enemy;

    [SerializeField] EnemyMovement _enemyMovement;

    [SerializeField] ShootSkill _shootSkill;

    [SerializeField] float _proximityShootingThreshold = 10f;
    [SerializeField] float _proximityFleeThreshold = 5f;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Vector2 heroDirection = _hero.transform.position - _enemy.transform.position;

        if (heroDirection.magnitude < _proximityFleeThreshold)
        {
            Shoot(heroDirection);
            MoveTowardsHero(-heroDirection);

        }
        else if (heroDirection.magnitude < _proximityShootingThreshold)
        {
            MoveTowardsHero(Vector2.zero);
            Shoot(heroDirection);
        }
        else
        {
            MoveTowardsHero(heroDirection);

        }

    }


    void Shoot(Vector2 direction)
    {
        _shootSkill.Do(direction);
    }


    void MoveTowardsHero(Vector2 direction)
    {
        _enemyMovement.Move(direction);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _proximityShootingThreshold);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, _proximityFleeThreshold);
    }
}
