using UnityEngine;

public class LearningView : View {
    [SerializeField] private Animator animator;
    [SerializeField] private string moveArm = "Move";
    [SerializeField] private string addArm = "Add";
    [SerializeField] private string helpArm = "Help";
    
    public void Step1() {
        Open();
        animator.SetBool(moveArm, true);
    }
    
    public void Step2() {
        Open();
        animator.SetBool(moveArm, false);
        animator.SetBool(addArm, true);
    }
    
    public void Step3() {
        Open();
        animator.SetBool(addArm, false);
        animator.SetBool(helpArm, true);
    }

    public void Hide() {
        animator.SetBool(helpArm, false);
        Close();
    }
}
