using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New SettingLevels", menuName = "SettingLevels", order = 0)]
public class SettingLevels : ScriptableObject {
    [SerializeField] private Level[] levels = new Level[1];

    [SerializeField] private int startRandomLevel = 40;

    private int level;
    public Level[] GetLevels() => levels;
    
    public void SetLevels(Level[] levels) => this.levels = levels;

    public Level GetLevel(int level)
    {
        if (level < levels.Length) this.level = level;
        else

            //level = levels.Length - 1;
        
            this.level = Random.Range(startRandomLevel ,levels.Length);
        Debug.Log("level " + this.level);         
        //levels[level].numberLevel = level;
        return levels[this.level];
    }
    
    public int GetCountWinSlot() => levels[this.level].countWinSlot;
}

[Serializable]
public class Level {
    public int numberLevel;
    public FullBasket[] fullBasket;
    public EmptyBasket[] emptyBasket;
    public int countWinSlot;
    public int countWinItem;
    public int minSteps;
}

[Serializable]
public class FullBasket {
    public int[] items;
    public bool checkCountItem;
}

[Serializable]
public class EmptyBasket { public bool checkCountItem; }

