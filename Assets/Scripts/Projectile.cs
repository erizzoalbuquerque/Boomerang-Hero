using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void Initialize(Vector2 direction, float speed)
    {
        _rb.velocity = direction.normalized * speed;
    }


    public void DestroySelf()
    {

        GameObject.Destroy(this.gameObject);
    }
}
