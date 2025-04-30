using System;
using UnityEngine;

public class Entry : MonoBehaviour {
    [SerializeField] private StorageService storageService;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private TouchHandler touchHandler;
    [SerializeField] private SlotsHandler slotsHandler;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private SpawnHandler spawnHandler;
    [SerializeField] private LearningService learningService;
    [SerializeField] private YandexHandler yandexHandler;
    [SerializeField] private RewardHandler rewardHandler;
    [SerializeField] private SoundHandler soundHandler;
    
    private int currentLevel;
    private int currentStars;
    private Item _tempItem;
    private Slot lastSlot;
    private int countWinSlot;
    private int currentWinSlot;
    private AppData appData;
    private int idSlot;
    
    private event Action<Slot> ClickSlotEvent;
    private event Action SetLastSlotEvent;

    void Awake() { Initialize(); }
    private void Initialize() {
        learningService.Hide();
        appData = storageService.LoadAppData();
        currentLevel = appData.level;
        if (currentLevel < 0) {
            currentLevel = 0;
            appData.level = currentLevel;
        }
       
        Slot[] slots = slotsHandler.GetSlots();
        currentLevel = spawnHandler.Initialize(slots, currentLevel);
        int minSteps = spawnHandler.GetMinCountSteps();
        uiHandler.Initialize(appData, minSteps);
        slotsHandler.Initialize();
        spawnHandler.StopTaskEvent += ContinueInitialize;
        spawnHandler.StepEvent += soundHandler.PlayStep;
        StartCoroutine(spawnHandler.ShowItems(slots));
    }

    private void ContinueInitialize() {
        spawnHandler.StopTaskEvent -= ContinueInitialize;
        uiHandler.InitializeGame();
        if (currentLevel == 0) {
            RewardAddBasket(1);
            RewardHelp(1);
            learningService.Initialize();
            learningService.Step(1);
        }
        countWinSlot = spawnHandler.GetLengthEmpty(currentLevel);
        yandexHandler.Initialize();
        Subscription();
    }
    
    private void OnLock(bool state) {
        if (state) touchHandler.ClickSlotEvent -= OnClickSlot;
        else
            touchHandler.ClickSlotEvent += OnClickSlot;
    }
    void Update() { touchHandler.Update(); }
    private void OnClick() { slotsHandler.Click(); } 
    private void OnClickSlot(Slot slot) { ClickSlotEvent?.Invoke(slot); }
    private void OnGetItem(Slot slot) {
        _tempItem = slot.GetItem();
        if (_tempItem == null) return;
        soundHandler.PlayStep();
        ClickSlotEvent = null;
        ClickSlotEvent += SetItem;
        lastSlot = slot;
        idSlot = slot.GetInstanceID();
    }
    
    private void SetItem(Slot slot) {
        soundHandler.PlayStep();
        if (idSlot != slot.GetInstanceID()) {
            uiHandler.SetStep();
            learningService.Step(2);
        }
        slot.SetItem(_tempItem, SetLastSlotEvent);
        ClickSlotEvent = null;
        ClickSlotEvent += OnGetItem;
    }

    private void OnSetLastSlot() {
        lastSlot.SetItem(_tempItem, SetLastSlotEvent);
        uiHandler.RemoveStep();
    }
    
    private void OnCountWin() {
        currentWinSlot++;
        soundHandler.PlayFullBasket();
        if (currentWinSlot == countWinSlot) Win();
    }

    private void Win() {
        uiHandler.Win();
        int saveLevel = currentLevel + 1;
        currentStars = uiHandler.GetActiveStar();
        SaveData(saveLevel, currentStars);
        soundHandler.PlayWin();
    }

    private void OnReplay() {
        SaveData(currentLevel, -currentStars);
        sceneHandler.LoadCurrentScene();
    }

    private void SaveData(int level, int stars) {
        appData.level = level;
        appData.stars += stars;
        appData.countAddBasket = uiHandler.countAdd;
        appData.countHelp = uiHandler.countHelp;
        storageService.SaveAppData(appData);
    }
    
    private void OnNext() {
        HandleReward();
        sceneHandler.LoadCurrentScene();
    }

    private void HandleReward() {
        int stars = uiHandler.GetActiveStar();
        rewardHandler.CheckStarsForHelp(stars);

        stars = appData.stars;
        appData.stars = rewardHandler.CheckStarsForBasket(stars);
        storageService.SaveAppData(appData);
    }
    

    private void OnAdd(bool toDo) {
        if (toDo == true) {
            Slot slot = slotsHandler.GetNextSlot();
            if (slot == null) return;
            spawnHandler.AddSlot(slot);
            SaveData(currentLevel, 0);
           learningService.Step(3);
        }
        else {
            yandexHandler.AddBasket();
        }
    }

    private void OnHelp(bool toDo) {
        if (toDo == true) {
            slotsHandler.Help();
            SaveData(currentLevel, 0);
            learningService.Step(4);
        }
        else {
            yandexHandler.Help();
        }
    }

    private void RewardAddBasket(int value = 0) {
        uiHandler.AddCountBasket(value);
        SaveLevel();
    }

    private void RewardHelp(int value = 0) {
        uiHandler.AddCountHelp(value);
        SaveLevel();
    }

    private void SaveLevel() {
        int saveLevel = appData.level;
        SaveData(saveLevel, 0);
    }

    private void OnAddBasket() {
        RewardAddBasket();
    }

    private void OnAddHelpEvent() {
        RewardHelp();
    }
    
    private void Subscription() {
        slotsHandler.LockEvent += OnLock;
        slotsHandler.CountWinEvent += OnCountWin;
        touchHandler.ClickEvent += OnClick;
        touchHandler.ClickSlotEvent += OnClickSlot;
        uiHandler.ReplayEvent += OnReplay;
        uiHandler.NextEvent += OnNext;
        uiHandler.AddEvent += OnAdd;
        uiHandler.HelpEvent += OnHelp;
        yandexHandler.AddBasketEvent += RewardAddBasket;
        yandexHandler.HelpEvent += RewardHelp;
        rewardHandler.AddBasketEvent += OnAddBasket;
        rewardHandler.AddHelpEvent += OnAddHelpEvent;
        ClickSlotEvent += OnGetItem;
        SetLastSlotEvent += OnSetLastSlot;
    }

    private void UnSubscription() {
        spawnHandler.StepEvent -= soundHandler.PlayStep;
        slotsHandler.LockEvent -= OnLock;
        slotsHandler.CountWinEvent -= OnCountWin;
        slotsHandler.Unsubscription();
        touchHandler.ClickEvent -= OnClick;
        touchHandler.ClickSlotEvent -= OnClickSlot;
        uiHandler.ReplayEvent -= OnReplay;
        uiHandler.NextEvent -= OnNext;
        uiHandler.AddEvent -= OnAdd;
        uiHandler.HelpEvent -= OnHelp;
        yandexHandler.AddBasketEvent -= RewardAddBasket;
        yandexHandler.HelpEvent -= RewardHelp;
        rewardHandler.AddBasketEvent -= OnAddBasket;
        rewardHandler.AddHelpEvent -= OnAddHelpEvent;
        ClickSlotEvent -= OnGetItem;
        SetLastSlotEvent -= OnSetLastSlot;
    }

    private void OnDestroy() { UnSubscription(); }
}