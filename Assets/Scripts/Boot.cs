using UnityEngine;

public class Boot : MonoBehaviour {
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private AudioSource backSound;

    private void Start() {
        DontDestroyOnLoad(backSound.gameObject);
        sceneHandler.LoadCurrentScene();
    }
}
