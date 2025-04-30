using UnityEngine;

public class PointItem : MonoBehaviour {
    public Item item;
    public Animator animator;
    public int orderLayer;

    public void Initialize() {
        if (item == null) return;
        item.Initialize();
    }

    public void ShowHelp() { animator.SetBool("Help", true); }  
    
    public void HideHelp() {  animator.SetBool("Help", false); }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (item == null) {
            int childCount = transform.childCount;
            if (childCount > 0) {
                item = transform.GetChild(0).GetComponent<Item>();
                item.SetOrderLayer(orderLayer);
            }
        }
    }
#endif
}
