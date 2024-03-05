using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _blobPrefab;
    [SerializeField] GameObject _shooterPrefab;
    [SerializeField] GameObject _shieldPrefab;
    [SerializeField] float _distanceFromPlayer = 3f;
    [SerializeField] LayerMask _solidMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool TryInstatiateEnemy(GameObject prefab)
    {
        HeroController heroController = FindAnyObjectByType<HeroController>();

        Vector2 position = (Vector2) heroController.transform.position + Random.insideUnitCircle.normalized * _distanceFromPlayer;

        for (int attempts = 0; attempts < 50; attempts++) 
        {
            bool isColliding = Physics2D.OverlapCircle(position, 2f, _solidMask);

            if (!isColliding)
            {
                GameObject.Instantiate(prefab,position,Quaternion.identity);
                return true;
            }
        }

        return false;
    }

    public void InstatiateBlob()
    {
        TryInstatiateEnemy(_blobPrefab);
    }

    public void InstatiateShooter()
    {
        TryInstatiateEnemy(_shooterPrefab);
    }

    public void InstatiateShield()
    {
        TryInstatiateEnemy(_shieldPrefab);
    }
}
