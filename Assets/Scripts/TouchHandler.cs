using System;
using UnityEngine;

[Serializable]
public class TouchHandler {
    [SerializeField] private Camera mainCamera;
    
    public event Action ClickEvent;
    public event Action<Slot> ClickSlotEvent;
    public void Update() { DetectObjectWithRaycast(); }
    
    private void DetectObjectWithRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            ClickEvent?.Invoke();
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit) { 
                if (hit.collider.TryGetComponent(out Slot slot)) {
                    ClickSlotEvent?.Invoke(slot);
                    return;
                }
            }
        }
    }
}
