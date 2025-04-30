using System;
using UnityEngine;

[Serializable]
public class LearningService {
    [SerializeField] private LearningView learningView;
    private event Action<int> StepEvent;
    private int currentStep = 1;  

    public void Initialize() { StepEvent += CheckStep; }

    public void Step(int step) => StepEvent?.Invoke(step); 

    public void CheckStep(int step) {
        switch (step) {
            case 1:
                Step1(step);
                break;
            case 2:
                Step2(step); 
                break;
            case 3:
                Step3(step); 
                break;
            case 4:
                Step4(step); 
                break;
        }
    }
    
    public void Step1(int step) {
        if (step == currentStep) {
            learningView.Step1();
            currentStep++;
        }
    }
    
    public void Step2(int step) {
        if (step == currentStep) {
            learningView.Step2();
            currentStep++;
        }
        
    }
    
    public void Step3(int step) {
        if (step == currentStep) {
            learningView.Step3();
            //StepEvent = null;
            currentStep++;
        }
       
    }
    
    public void Step4(int step) {
        if (step == currentStep) {
            StepEvent = null;
            Hide();
            currentStep++;
        }
       
    }

    public void Hide() {
        learningView.Hide();
    }
}
