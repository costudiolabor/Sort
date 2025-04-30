using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateLevels))]
public class СustomButton : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CreateLevels myScript = (CreateLevels)target;
        if (GUILayout.Button("CREATE")) {
            myScript.Create();
        }
    }

}

public class CreateLevels : MonoBehaviour {
    [SerializeField] private SettingLevels currentSettingLevels;
    [SerializeField] private int maxFullBasket;
    [SerializeField] private int maxEmptyBasket;
    [SerializeField] private int maxItem;
    [SerializeField] private int currentFullBasket;
    [SerializeField] private int currentItem;
    [SerializeField] private int currentShuffleItem;

     public void Create() {
         bool proccess = true;
         if (currentSettingLevels == null) {
             Debug.Log("SettingLevels = null");
             return;
         }
         currentFullBasket = 3;
         List<Level> levels = currentSettingLevels.GetLevels().ToList();
         while (proccess) {
             Level level = new Level();
             level.fullBasket = CreateFullBaskets().ToArray();
             level.emptyBasket = CreateEmptyBasket().ToArray();
             levels.Add(level);
             currentFullBasket++;
             proccess = currentFullBasket <= maxFullBasket;
         }
         
        currentSettingLevels.SetLevels(levels.ToArray());
        Debug.Log("SettingLevels Created");
     }

     [SerializeField] int[] items;
     private List<FullBasket> CreateFullBaskets() {
         List<FullBasket> fullBaskets = new List<FullBasket>();

         currentShuffleItem = 0;
         items = GetShuffleItems();
         
         for (int j = 0; j < currentFullBasket; j++) {
             FullBasket fullBasket = new FullBasket();
             SetFullBasketItems(fullBasket);
             fullBaskets.Add(fullBasket);
         }
         return fullBaskets;
     }


     private void SetFullBasketItems(FullBasket fullBasket) {
         int[] items = new int[currentItem];
         for (int i = 0; i < items.Length; i++) {
             items[i] = GetNextShuffleItems();
         } 
         fullBasket.items = items;
     }

     private int GetNextShuffleItems() {
         int index = currentShuffleItem;
         currentShuffleItem++;
         return items[index];
     }

     private int[] GetShuffleItems() {
         List<int> items = new List<int>();

         for (int j = 0; j < currentFullBasket; j++) {
             for (int i = 0; i < currentItem; i++) {
                 int id = j;
                 items.Add(id);
             }
         }
         Shuffle(0, items);
         return items.ToArray();
     }

     private void Shuffle(int beginShuffle, List<int> arrays) {
         for (int i = beginShuffle; i < arrays.Count; i++) {
             int j = Random.Range(beginShuffle, arrays.Count);
             (arrays[j], arrays[i]) = (arrays[i], arrays[j]);
         }
     }

     private  List<EmptyBasket> CreateEmptyBasket() {
         List<EmptyBasket> emptyBaskets = new List<EmptyBasket>();
         for (int j = 0; j < maxEmptyBasket; j++) {
             EmptyBasket emptyBasket = new EmptyBasket();
             emptyBaskets.Add(emptyBasket);
         }

         return emptyBaskets;
     }
}
