using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVelocity2D : MonoBehaviour
{
    [SerializeField] Vector2 startVelocity;

    Rigidbody2D rigidbody2d;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d.velocity = startVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
