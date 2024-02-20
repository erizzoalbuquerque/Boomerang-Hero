using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] InputMaster _inputMaster;
    [SerializeField] HeroController _hero;
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
        _hero.Move(_inputMaster.Player.Movement.ReadValue<Vector2>());
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _hero.ThrowBoomerang();
            _boomerang.Pull(true);
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _hero.Teleport();
        }
    }


    private void OnEnable()
    {
        _inputMaster.Player.Fire.performed += OnFire;
        _inputMaster.Player.Special.performed += OnSpecial;
    }


    private void OnDisable()
    {
        _inputMaster.Player.Fire.performed -= OnFire;
        _inputMaster.Player.Special.performed -= OnSpecial;
    }
}

