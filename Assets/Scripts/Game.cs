using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : View {
   [SerializeField] private Button buttonReplay;
   [SerializeField] private Button buttonAdd;
   [SerializeField] private Button buttonHelp;
   [SerializeField] private TMP_Text levelCount;
   [SerializeField] private TMP_Text countAdd;
   [SerializeField] private GameObject imageAdd;
   [SerializeField] private TMP_Text countHelp;
   [SerializeField] private GameObject imageHelp;
   
   public event Action ReplayEvent, /*PauseEvent,*/ AddEvent, HelpEvent; 
   public void Initialize() {
       buttonReplay.onClick.AddListener(OnReplay);
       buttonAdd.onClick.AddListener(OnAdd);
       buttonHelp.onClick.AddListener(OnHelp);
   }
   private void OnReplay() { ReplayEvent?.Invoke(); }
   private void OnAdd() {  AddEvent?.Invoke(); }
   private void OnHelp() { HelpEvent?.Invoke(); }
   public void ShowLevel(int value) { levelCount.text = value.ToString(); }

   public void ShowCountAdd(int value) {
       bool state = value > 0;
       countAdd.gameObject.SetActive(state);
       imageAdd.SetActive(!state);
       countAdd.text = value.ToString();
   }

   public void ShowCountHelp(int value) {
       bool state = value > 0;
       countHelp.gameObject.SetActive(state);
       imageHelp.SetActive(!state);
       countHelp.text = value.ToString();
   }

   //private void OnDestroy() { Unsubscription(); }

   private void Unsubscription() {
       buttonReplay.onClick.RemoveAllListeners();
       buttonAdd.onClick.RemoveAllListeners();
       buttonHelp.onClick.RemoveAllListeners();
   }
       
}
