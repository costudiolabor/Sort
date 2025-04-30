using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : View {
    [SerializeField] private Button buttonReplay;
    [SerializeField] private TMP_Text levelValue;
    public event Action ReplayEvent; 
    public void Initialize() { buttonReplay.onClick.AddListener(OnReplay); }
    private void OnReplay() { ReplayEvent?.Invoke(); }
    public void SetLevel(int value) { levelValue.text = value.ToString(); }
}
