using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _health = 3;
    [SerializeField] GameObject _explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float value)
    {
        _health -= value;

        if (_health <= 0)
            Kill();
    }

    virtual protected void Kill()
    {
        Instantiate(_explosionPrefab,this.transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
