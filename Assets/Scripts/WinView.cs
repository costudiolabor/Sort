using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WinView : View {
    [SerializeField] private Button buttonReplay;
    [SerializeField] private Button buttonNext;
    [SerializeField] private TMP_Text levelValue;
    [SerializeField] private float delay = 0.5f;
    
    public event Action ReplayEvent, NextEvent; 
    public void Initialize() {
        buttonReplay.onClick.AddListener(OnReplay);
        buttonNext.onClick.AddListener(OnNext);
    }
    public override void Open() { Invoke(nameof(TimerWin), delay); }
    private void TimerWin() { base.Open(); }
    private void OnReplay() { ReplayEvent?.Invoke(); }
    private void OnNext() { NextEvent?.Invoke(); }
    public void SetLevel(int value) { levelValue.text = value.ToString(); }
}
