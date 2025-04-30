using System;
using UnityEngine;

[Serializable]
public class StorageService {
    [SerializeField] private string keyAppData = "AppData";
    AppData appData = new AppData();
    public void SaveAppData(AppData appData) {
        string dataSave = JsonUtility.ToJson(appData); 
        PlayerPrefs.SetString(keyAppData, dataSave);
        PlayerPrefs.Save();
    }

    public AppData LoadAppData() {
        if (PlayerPrefs.HasKey(keyAppData) == true) {
            string dataSave = PlayerPrefs.GetString(keyAppData);
            appData = JsonUtility.FromJson<AppData>(dataSave);
        }
        return appData;
    }
}

[Serializable]
public class AppData {
    public int level;
    public int stars;
    public int countAddBasket;
    public int countHelp;
}
