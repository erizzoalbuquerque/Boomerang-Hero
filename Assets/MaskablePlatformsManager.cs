using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskablePlatformsManager : MonoBehaviour
{
    [SerializeField] MaskablePlatformsMask mask;
    [SerializeField] Hero hero;
    
    List<MaskablePlatform> _platforms;

    Vector3 _heroStartPosition;
    Vector3 _maskStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        _platforms = new List<MaskablePlatform>( GetComponentsInChildren<MaskablePlatform>() );

        _heroStartPosition = hero.transform.position;
        _maskStartPosition = mask.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsHeroInsideAnyPlatform() && mask.InsideMask)
        {
            //Nothing to do;
        }
        else
        {
            print("Inside Any Platform: " + IsHeroInsideAnyPlatform().ToString() + "   Inside Mask: " + mask.InsideMask.ToString() + "   Frame: " + Time.renderedFrameCount.ToString());

            // Reset Challenge
            hero.transform.position = _heroStartPosition;
            mask.Reset();
        }
    }

    bool IsHeroInsideAnyPlatform()
    {
        foreach(MaskablePlatform platform in _platforms) 
        {
            if (platform.IsInside)
                return true;
        }

        return false;
    }
}
