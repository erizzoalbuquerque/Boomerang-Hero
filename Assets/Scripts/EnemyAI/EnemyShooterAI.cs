using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooterAI : MonoBehaviour
{
    [SerializeField] BaseCharacterController _enemy;

    [SerializeField] ShootSkill _shootSkill;

    [SerializeField] float _proximityShootingThreshold = 10f;
    [SerializeField] float _proximityFleeThreshold = 5f;

    Hero _hero;


    // Start is called before the first frame update
    void Start()
    {
        _hero = GameManager.Instance.Hero;
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
        _shootSkill.Execute(direction);
    }


    void MoveTowardsHero(Vector2 direction)
    {
        _enemy.Move(direction);
    }


    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(this.transform.position,Vector3.forward, _proximityShootingThreshold);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(this.transform.position,Vector3.forward, _proximityFleeThreshold);
    }
}
