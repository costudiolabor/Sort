using System;
using UnityEngine;

[Serializable]
public class UIHandler : IDisposable{
    [SerializeField] private Game game;
    [SerializeField] private WinView winView;
    [SerializeField] private StarsHandler starsHandler;
    
    public int countAdd, countHelp;
    public event Action ReplayEvent, NextEvent;
    public event Action<bool> AddEvent, HelpEvent;
    
    private int currentLevel;
    public void Initialize(AppData appData, int minSteps) {
        starsHandler.Initialize(minSteps); 
        winView.Initialize();
        Subscription();
        currentLevel = appData.level;
        SetCountAdd(appData.countAddBasket);
        SetCountHelp(appData.countHelp);
        SetLevel(currentLevel);
        Game();
    }

    public void InitializeGame() { game.Initialize(); }
    
    public void Game() {
        game.Open();
        winView.Close();
    }
    
    public void Win() { winView.Open(); }
    
    public void Lose() { winView.Close(); }
    
    public void SetLevel(int value) {
        value++;
        game.ShowLevel(value);
        winView.SetLevel(value);
    }

    public void SetCountAdd(int value) {
        countAdd = value;
        game.ShowCountAdd(countAdd);
    }

    public void SetCountHelp(int value) {
        countHelp = value;
        game.ShowCountHelp(countHelp);
    }

    public void AddCountBasket(int value = 0) {
        if (value > 0)
            countAdd = value;
        else
            countAdd++;
        game.ShowCountAdd(countAdd);
    }
    
    public void AddCountHelp(int value = 0) {
        if (value > 0)
            countHelp = value;
        else
            countHelp++;
        game.ShowCountHelp(countHelp);
    }

    public int GetActiveStar() => starsHandler.GetActiveStar();
    private void OnNext() { NextEvent?.Invoke(); }
    public void SetStep() { starsHandler.AddStep(); }
    public void RemoveStep() { starsHandler.RemoveStep(); }
    private void OnReplay() { ReplayEvent?.Invoke(); }

    private void OnAdd() {
        bool isCountAdd = countAdd > 0;
        if (isCountAdd) countAdd--;
        game.ShowCountAdd(countAdd);
        AddEvent?.Invoke(isCountAdd);
    }

    private void OnHelp() {
        bool isCountHelp = countHelp > 0;
        if (isCountHelp) countHelp--;
        game.ShowCountHelp(countHelp);
        HelpEvent?.Invoke(isCountHelp);
    }
    
    private void Subscription() {
        game.ReplayEvent += OnReplay;
        game.AddEvent += OnAdd;
        game.HelpEvent += OnHelp;
        winView.ReplayEvent += OnReplay;
        winView.NextEvent += OnNext;
    }

    private void Unsubscription() {
        game.ReplayEvent -= OnReplay;
        game.AddEvent -= OnAdd;
        game.HelpEvent -= OnHelp;
        winView.ReplayEvent -= OnReplay;
        winView.NextEvent -= OnNext;
    }

    public void Dispose() { Unsubscription(); }
}
