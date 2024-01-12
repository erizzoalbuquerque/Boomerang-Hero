using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroVisual : MonoBehaviour
{
    [SerializeField] HeroMovement _heroMovement;
    [SerializeField] SpriteRenderer _heroSprite;
    [SerializeField] Animator _heroAnimator;

    bool _flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_heroMovement.LastNonZeroDirection.x > 0)
        {
            _flipped = false;
            _heroSprite.flipX = _flipped;
        }
        else if (_heroMovement.LastNonZeroDirection.x < 0)
        {
            _flipped = true;
            _heroSprite.flipX = _flipped;
        }


        _heroAnimator.SetBool("IsMoving", _heroMovement.MovingDirection.sqrMagnitude > 0 ? true : false);
    }
}
