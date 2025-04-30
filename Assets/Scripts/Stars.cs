using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour {
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Slider slider;
    
    public void Initialize(int maxValueSlider) {
        slider.maxValue = maxValueSlider;
        SetSlider(maxValueSlider);
    }

    public void SetStep(int indexStar, bool state) { stars[indexStar].SetActive(state); }

    public void SetSlider(float value) { slider.value = value; }

    public int GetActiveStar() {
        int result = 0;
        for (int i = 0; i < stars.Length; i++) {
            if (stars[i].activeInHierarchy) result++;
        }
        return result;
    }
}