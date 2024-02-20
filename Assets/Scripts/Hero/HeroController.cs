using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.PackageManager;

public class HeroController : BaseCharacterController
{
    [Header("References")]
    [SerializeField] Boomerang _boomerang;

    [Header("Settings")]
    [SerializeField] float _boomerangThrowOriginDistance = 0.5f;


    // Reserved Methods


    // Public Methods

    public void ThrowBoomerang()
    {
        if (_isStaggered != false)
            return;

        Vector2 nomalizedDirection = _lastNonZeroInputDirection.normalized;

        Vector2 startPosition = (Vector2) (this.transform.position) + (_boomerangThrowOriginDistance * nomalizedDirection);

        _boomerang.Throw(nomalizedDirection, startPosition);
    }

    public void Teleport()
    {
        if (_boomerang.CanBePulled && _boomerang.IsThrown)
            this.transform.position = _boomerang.transform.position;
    }


    // Private Methods

}
