using System;
using System.Collections;
using UnityEngine;

public class Slot : View {
    public Transform pointTeleport;
    public PointItem[] pointsItem;
    public Collider2D collider;
    public Animator animatorView;
    public ParticleSystem particleSystem;
    public bool isEnabled;
    public bool isEmpty;
    public int countWinItem;
    public bool checkCountItem;
    [SerializeField] private int currentGetIndex;
    [SerializeField] private int currentSetIndex;
    [SerializeField] private float delayTeleport = 0.5f;

    public event Action<bool> LockEvent;
    public event Action CountWinEvent;
    public void Initialize() {
        gameObject.SetActive(isEnabled);
        if (isEmpty) return;
        foreach (var point in pointsItem) {
            point.Initialize();
        }
    }
    
    public void ShowHelp() { animatorView.SetBool("Help", true); }  
    
    public void HideHelp() {  animatorView.SetBool("Help", false); }

    public void SpawnItem(Item item) {
        
        pointsItem[currentSetIndex].item = item;
        item.transform.SetParent(pointsItem[currentSetIndex].transform);
        item.transform.localPosition = Vector3.zero;
        int orderLayer = pointsItem[currentSetIndex].orderLayer;
        item.SetOrderLayer(orderLayer);
        currentGetIndex = currentSetIndex; 
        currentSetIndex--;
        if (currentSetIndex < 0) currentSetIndex = 0;
    }
    
    public Item GetItem() {
        Item result = null;
        if (pointsItem[currentGetIndex].item == null) return null;
        
        if (currentGetIndex < pointsItem.Length) {
            result = pointsItem[currentGetIndex].item;
            pointsItem[currentGetIndex].item = null;
            currentSetIndex = currentGetIndex;
            currentGetIndex++;
            if (currentGetIndex == pointsItem.Length) currentGetIndex = pointsItem.Length - 1;
            SetItemToTeleport(result);
        } 
        return result;
    }

    private void SetItemToTeleport(Item item) { item.SetItem(pointTeleport, false, 100); }

    public void SetItem(Item item, Action SetLastSlotEvent) {
        StartCoroutine(TimerItemToTeleport(item, SetLastSlotEvent));
    }

    private IEnumerator TimerItemToTeleport(Item item, Action SetLastSlotEvent) {
        if (currentGetIndex == 0) {
            SetLastSlotEvent?.Invoke();
            yield break;
        }
        LockEvent?.Invoke(true);
        pointsItem[currentSetIndex].item = item;
        item.SetItem(pointTeleport, false, 100);
        yield return new WaitForSeconds(delayTeleport);
        int orderLayer = pointsItem[currentSetIndex].orderLayer;
        item.SetItem(pointsItem[currentSetIndex].transform, true, orderLayer);
        currentGetIndex = currentSetIndex; 
        currentSetIndex--;
        if (currentSetIndex < 0) currentSetIndex = 0;
        LockEvent?.Invoke(false);
        CheckSlot();
    }

    private void CheckSlot() {
        if (checkCountItem == false) return;
        int i = 0;
        int tempId = 0; 
        int countWinItem = 0;
        for (; i < pointsItem.Length; i++) {
            if (pointsItem[i].item == null) continue;
            if (countWinItem == 0) { tempId = pointsItem[i].item.GetItemId(); }
            if (tempId == pointsItem[i].item.GetItemId()) { countWinItem++; }
            else {
                countWinItem = 0;
                break;
            }
        }

        if (countWinItem == this.countWinItem) {
            Win();
        }
    }

    private void Win() {
        collider.enabled = false;
        isEnabled = false;
        particleSystem.Play();
        CountWinEvent?.Invoke();
    }

    public PointItem GetLastItem() {
        PointItem result = null;
        result = pointsItem[currentGetIndex];
        return result;
    }

    public bool CheckSetItem(Item item, ref bool isEmptySlot) {
        if (currentSetIndex == pointsItem.Length - 1) {
            isEmptySlot = true;
            return true;
        }
        
        //if (currentSetIndex >= 0) {
        if (currentGetIndex > 0) {
            if (item.GetItemId() == pointsItem[currentGetIndex].item.GetItemId()) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
        
    }
}
