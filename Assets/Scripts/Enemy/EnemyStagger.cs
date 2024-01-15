using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStagger : MonoBehaviour
{
    [SerializeField] float _staggerDuration = 2f;

    bool _isStaggered;
    public bool IsStaggered { get => _isStaggered; }


    // Start is called before the first frame update
    void Start()
    {
        _isStaggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply()
    {
        StartCoroutine(StaggerCoroutine(_staggerDuration));
    }

    IEnumerator StaggerCoroutine(float duration)
    {
        _isStaggered = true;
        yield return new WaitForSeconds(duration);
        _isStaggered = false;
    }
}
