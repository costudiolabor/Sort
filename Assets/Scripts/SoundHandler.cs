using UnityEngine;

public class SoundHandler : MonoBehaviour {
    [SerializeField] private AudioSource step;
    [SerializeField] private AudioSource fullBasket;
    [SerializeField] private AudioSource win;
    public void PlayStep() {  step.Play(); }
    public void PlayFullBasket() { fullBasket.Play(); }
    public void PlayWin() { win.Play(); }
}
