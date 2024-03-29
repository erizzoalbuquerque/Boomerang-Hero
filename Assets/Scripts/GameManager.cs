using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] HeroController _hero;

    public HeroController Hero { get => GetHero(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    HeroController GetHero()
    {
        if (_hero == null)
            _hero = FindAnyObjectByType<HeroController>();

        return _hero;
    }
}
