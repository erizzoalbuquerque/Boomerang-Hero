using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : Skill
{
    [SerializeField] GameObject _projectilePrefab;

    [SerializeField] float _projectileSpeed = 6f;
    [SerializeField] float _startOriginDistance = 1.5f;

    [SerializeField] float _coolDown = 3f;

    float _coolDownStartTime = 0f;

    Collider2D _collider2D;


    // Start is called before the first frame update
    void Start()
    {
        _collider2D = transform.root.gameObject.GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public override bool Execute(Vector3 direction)
    {
        if (_character.IsStaggered)
            return false;

        if (Time.time - _coolDownStartTime < _coolDown)
            return false;

        Shoot(direction);

        return true;
    }


    public override void Cancel()
    {
        base.Cancel();
        _coolDownStartTime = 0f;
    }


    void Shoot(Vector3 direction)
    {
        _coolDownStartTime = Time.time;

        Vector3 projectileOrigin = CalculateProjectileOrigin(direction);

        GameObject projectileInstance = Instantiate(_projectilePrefab, projectileOrigin, Quaternion.identity);

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        projectile.Initialize(direction, _projectileSpeed);        
    }


    Vector3 CalculateProjectileOrigin(Vector3 direction)
    {
        Bounds bounds = _collider2D.bounds;
        Ray ray = new Ray(transform.position, direction);

        if (bounds.IntersectRay(ray, out float intersectionDistance))
        {
            return transform.position + (direction.normalized * (-intersectionDistance + 0.6f));
        }

        return transform.position + (direction.normalized * _startOriginDistance);
    }

}
