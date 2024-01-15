using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : Skill
{
    [SerializeField] EnemyStagger _enemyStagger;
    [SerializeField] GameObject _projectilePrefab;

    [SerializeField] float _projectileSpeed = 6f;
    [SerializeField] float _startOriginDistance = 1.5f;

    [SerializeField] float _coolDown = 3f;

    float _coolDownStartTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override bool Do(Vector3 direction)
    {
        if (_enemyStagger.IsStaggered)
            return false;

        if (Time.time - _coolDownStartTime < _coolDown)
            return false;

        Shoot(direction);

        return true;
    }


    public override void Halt()
    {
        _coolDownStartTime = 0f;
    }


    void Shoot(Vector3 direction)
    {
        _coolDownStartTime = Time.time;

        Bounds bounds = transform.root.gameObject.GetComponent<Collider2D>().bounds;
        Ray ray = new Ray(transform.position, direction);

        Vector2 originPosition;
        if (bounds.IntersectRay(ray, out float intersectionDistance))
        {
            // The ray intersects the bounds, and the intersection point is stored in the "intersection" variable
            originPosition = transform.position + direction.normalized * (-intersectionDistance + 0.6f);
            Debug.Log("Intersection point: " + intersectionDistance);
        } else
        {
            originPosition = transform.position + direction.normalized * _startOriginDistance;
        }

        GameObject projectileInstance = Instantiate(_projectilePrefab, originPosition, Quaternion.identity);

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        projectile.Initialize(direction, _projectileSpeed);        
    }

}
