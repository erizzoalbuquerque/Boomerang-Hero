using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] InputMaster _inputMaster;
    [SerializeField] Hero hero;
    [SerializeField] Boomerang boomerang;

    // Start is called before the first frame update
    void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ////Hero direction
        //currentInputDirection.x = (Keyboard.current.leftArrowKey.isPressed ? -1f : 0f) + (Keyboard.current.rightArrowKey.isPressed ? 1f : 0f); 
        //currentInputDirection.y = (Keyboard.current.upArrowKey.isPressed ? 1f : 0f) + (Keyboard.current.downArrowKey.isPressed ? -1f : 0f);
        //
        //hero.SetDirection(currentInputDirection);
        //
        ////Boomerang        
        //if (Keyboard.current.xKey.wasPressedThisFrame)
        //{
        //    hero.ThrowBoomerang();
        //    boomerang.Pull(true);
        //}
        //
        //if (Keyboard.current.xKey.wasReleasedThisFrame)
        //{
        //    boomerang.Pull(false);
        //}

        hero.SetDirection(_inputMaster.Player.Movement.ReadValue<Vector2>());
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        //currentInputDirection = context.action.
        //hero.SetDirection(cur)
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            hero.ThrowBoomerang();
            boomerang.Pull(true);
        }

        //if (context.phase == InputActionPhase.Canceled)
        //{
        //    boomerang.Pull(false);
        //}
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

