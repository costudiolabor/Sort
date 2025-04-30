using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable] 
public class SpawnHandler {
    [SerializeField] private Item[] items;
    [SerializeField] private SettingLevels settingLevels;
    [SerializeField] private Level level;
    private int countWinItem;
    
    private int indexBasket;
    private int maxIndex;
    private int countFullBasket;
    private MonoBehaviour mono;
    public event Action StepEvent, StopTaskEvent;
   
    public int Initialize(Slot[] slots, int currentLevel) {
        level = settingLevels.GetLevel(currentLevel);
        countFullBasket = level.fullBasket.Length;
        int countEmptyBasket = level.emptyBasket.Length;
        countWinItem = level.countWinItem;
        maxIndex = countFullBasket;
        for (indexBasket = 0; indexBasket < maxIndex; indexBasket++) {
            slots[indexBasket].isEnabled = true;
            slots[indexBasket].isEmpty = false;
            slots[indexBasket].countWinItem = countWinItem;
            slots[indexBasket].checkCountItem = level.fullBasket[indexBasket].checkCountItem;
        }
        
        // empty Slots
        // maxIndex = 9 - countFullBasket;
        maxIndex = countFullBasket + countEmptyBasket;
        for (;indexBasket < maxIndex; indexBasket++) {
            slots[indexBasket].isEnabled = true;
            slots[indexBasket].isEmpty = true;
            slots[indexBasket].countWinItem = countWinItem;
            slots[indexBasket].checkCountItem = true;
        }
        
        
        //full Slots
         maxIndex = countFullBasket;
         for (indexBasket = 0; indexBasket < maxIndex; indexBasket++) {
             int countItem = level.fullBasket[indexBasket].items.Length;
             
             for (int indexItem = 0; indexItem < countItem; indexItem++) {
                 int numberItem = level.fullBasket[indexBasket].items[indexItem];
                 Item item = GetItem(numberItem);
                 slots[indexBasket].SpawnItem(item);
             }
         }
        return currentLevel;
    }

    public IEnumerator ShowItems(Slot[] slots) {
        int slotsLength = slots.Length;
        for (int i = 0; i < slotsLength; i++) {
            int pointsItemLength = slots[i].pointsItem.Length;
            if (slots[i].isEnabled == false) continue;
            for (int j = 0; j < pointsItemLength; j++) {
                Item item = slots[i].pointsItem[j].item;
                if (item != null) {
                    item.Open();
                    StepEvent?.Invoke();
                    yield return null;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        StopTaskEvent?.Invoke();
    }
    
    
    private Item GetItem(int index) => Object.Instantiate(items[index]);
    
    public int GetLengthEmpty(int currentLevel) {
        //int result = settingLevels.GetLevel(currentLevel).countWinSlot;
        int result = settingLevels.GetCountWinSlot();
        return result;
    }

    public int GetMinCountSteps() { return level.minSteps; }

    public void AddSlot(Slot slot) {
        slot.isEnabled = true;
        slot.isEmpty = true;
        slot.countWinItem = countWinItem;
        slot.checkCountItem = true;
        slot.Open();
    }

   
}
