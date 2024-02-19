using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;

    Camera mainCamera;
    Vector2 movementInput;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        print(movementInput.ToString());        
    }

    void Move()
    {
        Vector3 movement;

        Vector3 forwardDirection = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z);
        Vector3 rightDirection = new Vector3(mainCamera.transform.right.x, 0f, mainCamera.transform.right.z);

        Vector3 movementDirection = forwardDirection * movementInput.y + rightDirection * movementInput.x;

        movement = movementDirection * speed * Time.deltaTime;

        if (!characterController.isGrounded)
        {
            movement += (Vector3.up * characterController.velocity.y + Physics.gravity * Time.deltaTime)*Time.deltaTime ;            
        }

        characterController.Move(movement);
    }
}
