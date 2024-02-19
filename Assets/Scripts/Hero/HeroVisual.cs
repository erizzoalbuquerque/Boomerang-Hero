using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroVisual : MonoBehaviour
{
    [SerializeField] Hero _hero;
    [SerializeField] HeroMovement _heroMovement;
    [SerializeField] SpriteRenderer _heroSprite;
    [SerializeField] Animator _heroAnimator;
    [SerializeField] Blinking _blinking;

    bool _flipped = false;

    bool _wasInvencibleLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_heroMovement.MovingDirection.x > 0)
        {
            _flipped = false;
            _heroSprite.flipX = _flipped;
        }
        else if (_heroMovement.MovingDirection.x < 0)
        {
            _flipped = true;
            _heroSprite.flipX = _flipped;
        }


        _heroAnimator.SetBool("IsMoving", _heroMovement.MovingDirection.sqrMagnitude > 0 ? true : false);

        if (_hero.IsInvencible && !_wasInvencibleLastFrame)
        {
            _blinking.StartBlinking();
            _wasInvencibleLastFrame = true;
        }
        else if (!_hero.IsInvencible && _wasInvencibleLastFrame)
        {
            _blinking.StopBlinking();
            _wasInvencibleLastFrame = false;
        }
    }
}
