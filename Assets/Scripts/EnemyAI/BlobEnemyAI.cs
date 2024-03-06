using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobEnemyAI : MonoBehaviour
{
    [SerializeField] BaseCharacterController _enemy;

    [SerializeField] JumpAttackSkill _jumptAttackSkill;

    [SerializeField] float _jumpThreshold = 2f;

    [SerializeField] float _jumpAttackCooldown = 3f;

    HeroController _hero;
    float _lastJumpAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        _hero = GameManager.Instance.Hero;
        _lastJumpAttackTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 heroDirection = _hero.transform.position - _enemy.transform.position;      

        if ( (Time.time - _lastJumpAttackTime) < _jumpAttackCooldown)
        {
            _enemy.Move(Vector2.zero);
        }
        else
        {
            if (heroDirection.magnitude > _jumpThreshold)
            {
                MoveTowardsHero(heroDirection);
            }
            else
            {
                Jump(heroDirection);
            }
        }
    }


    void Jump(Vector2 direction)
    {
        bool success = _jumptAttackSkill.Execute(direction);

        if ( success == true )
        {
            _lastJumpAttackTime = Time.time;
        }
    }


    void MoveTowardsHero(Vector2 direction)
    {
        _enemy.Move(direction);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, Vector3.forward, _jumpThreshold);
    }
#endif

}
