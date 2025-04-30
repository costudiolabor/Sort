using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR

[CustomEditor(typeof(MergeLevels))]
public class MergeButton : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        MergeLevels myScript = (MergeLevels)target;
        
        if (GUILayout.Button(" MergeLevels ")) {
            myScript.Merge();
        } 
    }
}
#endif


public class MergeLevels : MonoBehaviour {
    [SerializeField] private SettingLevels newSettingLevels;
    [SerializeField] private List<SettingLevels> _settingLevelsList;

    public void Merge() {
        List<Level> mergeLevels = new List<Level>(); 
        for (int i = 0; i < _settingLevelsList.Count; i++) {
            Level[] levels = _settingLevelsList[i].GetLevels();
            for (int j = 0; j < levels.Length; j++) {
                Level level = levels[j];
                mergeLevels.Add(level);
            }
        }
        newSettingLevels.SetLevels(mergeLevels.ToArray());
    }
}
