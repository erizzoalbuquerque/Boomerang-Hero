using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] InputMaster _inputMaster;

    [SerializeField] Hero _hero;
    [SerializeField] HeroMovement _heroMovement;
    [SerializeField] Boomerang _boomerang;

    // Start is called before the first frame update
    void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _heroMovement.SetDirection(_inputMaster.Player.Movement.ReadValue<Vector2>());
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _hero.ThrowBoomerang();
            _boomerang.Pull(true);
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

