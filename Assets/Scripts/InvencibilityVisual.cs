using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Blinking))]
public class InvencibilityVisual : MonoBehaviour
{
    [SerializeField] BaseCharacterController _character;

    Blinking _blinking;
    bool _wasInvencibleLastFrame;

    void Awake()
    {
        _blinking = GetComponent<Blinking>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_character.IsInvencible && !_wasInvencibleLastFrame)
        {
            _blinking.StartBlinking();
            _wasInvencibleLastFrame = true;
        }
        else if (!_character.IsInvencible && _wasInvencibleLastFrame)
        {
            _blinking.StopBlinking();
            _wasInvencibleLastFrame = false;
        }
    }
}
