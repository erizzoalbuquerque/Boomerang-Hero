using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyAI : MonoBehaviour
{
    [SerializeField] BaseCharacterController _character;
    [SerializeField] float _followDistance = 10f;
    [SerializeField] float _restAfterHitDuration = 2f;

    HeroController _hero;
    List<AttackHitbox> _attackHitboxes = new List<AttackHitbox>();
    float _lastTimeHitPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        _hero = GameManager.Instance.Hero;
        _attackHitboxes = new List<AttackHitbox>(GetComponentsInChildren<AttackHitbox>());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - _lastTimeHitPlayer > _restAfterHitDuration) == false)
        {
            DoIdle();
        }
        else
        {
            Vector2 heroDirection = _hero.transform.position - _character.transform.position;
            if (heroDirection.magnitude < _followDistance)
            {
                MoveTowardsHero(heroDirection);
            }
            else
            {
                DoIdle();
            }
        }
    }
    void MoveTowardsHero(Vector2 direction)
    {
        _character.Move(direction);
    }

    void DoIdle()
    {
        _character.Move(Vector2.zero);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, Vector3.forward, _followDistance);
    }
#endif

    private void OnEnable()
    {
        foreach (var attack in _attackHitboxes) 
        {
            attack.CollidedWithHittable.AddListener(OnHitPlayer);
        }
    }

    private void OnDisable()
    {
        foreach (var attack in _attackHitboxes)
        {
            attack.CollidedWithHittable.RemoveListener(OnHitPlayer);
        }
    }

    void OnHitPlayer()
    {
        _lastTimeHitPlayer = Time.time;
    }
}
