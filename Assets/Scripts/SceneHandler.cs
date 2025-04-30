using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class SceneHandler {
    [SerializeField] private int loadScene; 
    public void LoadCurrentScene() { SceneManager.LoadScene(loadScene); }
}