using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskablePlatform : MonoBehaviour
{
    public bool IsInside;


    // Start is called before the first frame update
    void Start()
    {
        IsInside = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( (collision.gameObject.layer != LayerMask.NameToLayer("Hero")) || collision.gameObject.tag != "Player" )
            return;

        IsInside = true;
        print(collision.gameObject.name + " entered Platform: " + this.gameObject.name.ToString() + " on Frame: " + Time.renderedFrameCount.ToString());

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( (collision.gameObject.layer != LayerMask.NameToLayer("Hero")) || collision.gameObject.tag != "Player" )
            return;

        IsInside = false;
        print(collision.gameObject.name + " exited Platform: " + this.gameObject.name.ToString() + " on Frame: " + Time.renderedFrameCount.ToString());
    }
}

