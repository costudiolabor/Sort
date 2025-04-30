using UnityEngine;

public class Item : View {
    private Transform thisTransform;
    private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string animState = "Jump";
    [SerializeField] private int itemId;

    public void Initialize() {
        thisTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Close();
    }

    public bool SetItem(Transform parent, bool isSlot, int orderLayer) {
        thisTransform.SetParent(parent);
        thisTransform.localPosition = Vector3.zero;
        if (isSlot) {
            SetOrderLayer(orderLayer);
            animator.SetTrigger(animState);
        }
        return true;
    }
    
    public void SetOrderLayer(int orderLayer) => spriteRenderer.sortingOrder = orderLayer;
    
    //public void SetItemId(int itemId) => this.itemId = itemId; 
    public int GetItemId() => itemId;
    
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
#endif
}
