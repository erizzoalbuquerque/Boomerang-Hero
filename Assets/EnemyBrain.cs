using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] EnemyMovement _enemyMovement;
    [SerializeField] Hero _hero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMovement.Move(_hero.transform.position - this.transform.position);
    }
}
