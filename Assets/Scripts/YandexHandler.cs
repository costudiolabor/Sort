using UnityEngine;
using System;
using YG;

[Serializable]
public class YandexHandler {
    [SerializeField] private YandexGame yandexGame;
    private TypeReward _currentTypeReward;
    
    enum TypeReward {
        AddBasket,
        Help
    }

    public event Action<int> AddBasketEvent, HelpEvent;
    public void Initialize() { yandexGame.RewardVideoAd.AddListener(OnReward); }
    private void OnNextLevelReward() { yandexGame._RewardedShow(0); }

    public void AddBasket() {
        _currentTypeReward = TypeReward.AddBasket;
        yandexGame._RewardedShow(0);
    }
    
    public void Help() {
        _currentTypeReward = TypeReward.Help;
        yandexGame._RewardedShow(0);
    }

    private void OnReward() {
        if (_currentTypeReward == TypeReward.AddBasket) { AddBasketEvent?.Invoke(0); }
        if (_currentTypeReward == TypeReward.Help) { HelpEvent?.Invoke(0); }
    }
    
}
