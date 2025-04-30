using System;
using UnityEngine;

[Serializable]
public class SlotsHandler {
    [SerializeField] private Slot[] _slots;
    private Slot slot;
    private PointItem pointItem;
    
    public event Action HideHelpEvent;
    public event Action CountWinEvent;
    public event Action<bool> LockEvent;
    
    public void Initialize() {
        foreach (var slot in _slots) {  slot.Initialize();  }
        Subscription();
    }

    public Slot[] GetSlots() => _slots;
    
    public Slot GetNextSlot() {
        Slot result = null;
        for (int i = 0; i < _slots.Length; i++) {
            if (_slots[i].isEnabled == false && _slots[i].checkCountItem == false) {
                result = _slots[i];
                break;
            }            
        }
        return result;
    }

    private void OnCountWin() { CountWinEvent?.Invoke(); }
    private void OnLock(bool state) { LockEvent?.Invoke(state); }
    
    public void Help() {
        
        for (int i = 0; i < _slots.Length; i++) {
            if (_slots[i].isEnabled == true) {
                pointItem = _slots[i].GetLastItem();
                Item item = pointItem.item;
                bool isSetItem = CheckSlots(item, i);
                if (isSetItem) {
                    ShowHelp();
                    break;
                }
            }
        }
        
    }

    private bool CheckSlots(Item item, int indexSlot)
    {
        bool isEmptySlot = false;
        bool result = false;
        if (item == null) return result;
        
        for (int i = 0; i < _slots.Length; i++) {
            if (indexSlot == i) continue;
            if (_slots[i].isEnabled == true) {
                //Debug.Log("_slots[i] " + i + " _slots[i].enabled " + _slots[i].enabled);
                bool isSetItem = _slots[i].CheckSetItem(item, ref isEmptySlot);
                if (isSetItem) {
                    slot = _slots[i];
                    result = true;
                    if (isEmptySlot == false) break;
                }
            }
        }
        
        return result;
    }

    private void ShowHelp() {
        slot.ShowHelp();
        pointItem.ShowHelp();
        HideHelpEvent += HideHelp;
    }
    
    private void HideHelp() {
        slot.HideHelp();
        pointItem.HideHelp();
        HideHelpEvent -= HideHelp;
    }

    public void Click() { HideHelpEvent?.Invoke();}
    
    private void Subscription() {
        foreach (var slot in _slots) {
            slot.LockEvent += OnLock;
            slot.CountWinEvent += OnCountWin;
        }
    }
    
    public void Unsubscription() {
        foreach (var slot in _slots) {
            slot.LockEvent -= OnLock;
            slot.CountWinEvent -= OnCountWin;
        }
    }

}
