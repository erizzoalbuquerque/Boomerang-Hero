using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] Boomerang boomerang;

    Vector2 currentInput;

    // Start is called before the first frame update
    void Start()
    {
        currentInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Hero direction
        currentInput.x = (Keyboard.current.leftArrowKey.isPressed ? -1f : 0f) + (Keyboard.current.rightArrowKey.isPressed ? 1f : 0f); 
        currentInput.y = (Keyboard.current.upArrowKey.isPressed ? 1f : 0f) + (Keyboard.current.downArrowKey.isPressed ? -1f : 0f);

        hero.SetDirection(currentInput);

        //Boomerang        
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            hero.ThrowBoomerang();
            boomerang.Pull(true);
        }

        if (Keyboard.current.xKey.wasReleasedThisFrame)
        {
            boomerang.Pull(false);
        }
    }
}
