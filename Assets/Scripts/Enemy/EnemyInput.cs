using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyInput : MonoBehaviour
{
    [SerializeField] InputMaster _inputMaster;

    [SerializeField] EnemyMovement _enemyMovement;

    [SerializeField] ContactAttackSkill _contactAttackSkill;
    [SerializeField] JumpAttackSkill _jumptAttackSkill;
    [SerializeField] ShootSkill _shotSkill;


    // Start is called before the first frame update
    void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMovement.Move(_inputMaster.Player.Movement.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _shotSkill.Do(_inputMaster.Player.Movement.ReadValue<Vector2>());
        }
    }


    private void OnEnable()
    {
        _inputMaster.Player.Fire.performed += OnFire;
    }


    private void OnDisable()
    {
        _inputMaster.Player.Fire.performed -= OnFire;
    }
}


