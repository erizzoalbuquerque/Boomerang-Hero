using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string _loadScene; 

    public void LoadScene()
    {
        SceneManager.LoadScene(_loadScene);
    }
}
