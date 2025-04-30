using System;
using UnityEngine;

[Serializable]
public class RewardHandler {
    [SerializeField] private int countStarsRewardAddBasket;
    
    public event Action AddBasketEvent, AddHelpEvent;
    
    public int CheckStarsForBasket(int value) {
        if (countStarsRewardAddBasket > value) return value;
        AddBasketEvent?.Invoke();
        AddHelpEvent?.Invoke();
        return value - countStarsRewardAddBasket;
    }    
    
    public void CheckStarsForHelp(int value) {
        // Debug.Log("CheckStarsForHelp " + value);
        // //if (countStarsRewardAddHelp > value) return;
        // if (value < 3) return;
        // currentStarsRewardAddHelp++;
        // Debug.Log("currentStarsRewardAddHelp " + currentStarsRewardAddHelp);
        // if (countStepsRewardAddHelp > currentStarsRewardAddHelp) return;
        // currentStarsRewardAddHelp = 0;
        // AddHelpEvent?.Invoke();
    }    
}
