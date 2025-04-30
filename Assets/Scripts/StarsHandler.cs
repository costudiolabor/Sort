using System;
using UnityEngine;

[Serializable]
public class StarsHandler {
    [SerializeField] private Stars stars;
    [SerializeField] private Stars starsWinView;
    [SerializeField] private int minSteps;
    [SerializeField] private int currentSteps;
    private int middleSteps;
    private int maxSteps;
    public void Initialize(int minSteps) {
        stars.Initialize(minSteps);
        this.minSteps = minSteps;
        middleSteps = minSteps / 2 + minSteps;
        maxSteps = this.minSteps * 2;
    }

    public void AddStep() {
        currentSteps++;
        CheckSteps();
    }
    
    public void RemoveStep() {
        currentSteps--;
    }

    private void CheckSteps() {
        stars.SetStep(1, currentSteps <= middleSteps);
        stars.SetStep(2, currentSteps <= minSteps);
        
        starsWinView.SetStep(1, currentSteps <= middleSteps);
        starsWinView.SetStep(2, currentSteps <= minSteps);
        
        float value = maxSteps - currentSteps;
        stars.SetSlider(value);
    }

    public int GetActiveStar() => stars.GetActiveStar();
}