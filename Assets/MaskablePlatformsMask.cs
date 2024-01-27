using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskablePlatformsMask : MonoBehaviour
{
    public bool InsideMask;

    // Start is called before the first frame update
    void Start()
    {
        InsideMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        GetComponent<Animator>().Rebind();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Hero"))
            return;
        
        InsideMask = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Hero"))
            return;
        
        InsideMask = false;
    }
}
