using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR

[CustomEditor(typeof(EditLevel))]
public class customButton : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        EditLevel myScript = (EditLevel)target;
        
        if (GUILayout.Button("CreateLevel ")) {
            myScript.CreateLevel();
        } 
        
        if (GUILayout.Button("SaveLevel ")) {
            myScript.SaveLevel();
        } 
        
        if (GUILayout.Button("ClearLevel ")) {
            myScript.ClearLevel();
        }
    }
}
#endif


public class EditLevel : MonoBehaviour {
    public Slot[] _slots;
    public SettingLevels settingLevels;

    private List<FullBasket> fullBaskets;
    private List<EmptyBasket> emptyBaskets;
    
    public int countWinSlot;
    public int countWinItem;
    public int minSteps;

    [SerializeField] private Level level = new Level();
    
    public void CreateLevel() {
        fullBaskets = new List<FullBasket>();
        emptyBaskets = new List<EmptyBasket>();

        for (int i = 0; i < _slots.Length; i++) {
            FullBasket fullBasket = new FullBasket();;
            EmptyBasket emptyBasket = new EmptyBasket();;
            Slot slot = _slots[i];
            if (slot.gameObject.activeInHierarchy == false) continue;
            //Debug.Log("activeInHierarchy ");
            
            PointItem[] pointItems = slot.pointsItem;
            bool isEmpty = true; 
            
            for (int j = 0; j < pointItems.Length; j++) {
                int childCount = pointItems[j].transform.childCount;
                if (childCount > 0) {
                    isEmpty = false;
                }
            }

            if (isEmpty == false)
            {
                List<int> items = new List<int>();
                for (int j = 0; j < pointItems.Length; j++) {
                    int childCount = pointItems[j].transform.childCount;
                    if (childCount > 0) {
                        Item item = pointItems[j].transform.GetChild(0).GetComponent<Item>();
                        int idItem = item.GetItemId();
                        items.Add(idItem);
                    }
                }

                fullBasket.items = items.ToArray();
                CreateFullBasket(fullBasket);
            }
            else {
                emptyBasket.checkCountItem = true;
                CreateEmptyBasket(emptyBasket);
            }
        }

        CreateLevel(level);
    }


    private void CreateFullBasket(FullBasket fullBasket) {
        fullBaskets.Add(fullBasket);
    }
    
    private void CreateEmptyBasket( EmptyBasket emptyBasket) {
        emptyBaskets.Add(emptyBasket);
    }


    private void CreateLevel(Level level) {
        level.fullBasket = fullBaskets.ToArray();
        level.emptyBasket = emptyBaskets.ToArray();
        level.countWinSlot = countWinSlot;
        level.countWinItem = countWinItem;
        level.minSteps = minSteps;
    }

    public void ClearLevel() {
        for (int i = 0; i < _slots.Length; i++) {
            
            Slot slot = _slots[i];
            PointItem[] pointItems = slot.pointsItem;
            
            for (int j = 0; j < pointItems.Length; j++) {
                int childCount = pointItems[j].transform.childCount;
                
                for (int k = 0; k < childCount; k++) {
                    Transform child = pointItems[j].transform.GetChild(k);
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        level = null;
        countWinSlot = 0;
        countWinItem = 0;
        minSteps = 0;
    }

    public void SaveLevel() {
        List<Level> tempLevels = settingLevels.GetLevels().ToList();
        tempLevels.Add(level);
        settingLevels.SetLevels(tempLevels.ToArray());
    }
}
