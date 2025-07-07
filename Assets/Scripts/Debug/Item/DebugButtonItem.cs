using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugButtonItem :MonoBehaviour {
  [SerializeField] private Text debugMenuName;
  [SerializeField] private Button debugButton;

  private Action onClick = null;


  public void Initialize(string name, Action callBack) {
    debugMenuName.text = name;
    onClick = callBack;
    debugButton.onClick.AddListener(OnClickEvent);
  }

  private void OnClickEvent() {
    if(onClick == null) {
      Debug.LogError("コールバックが設定されていません");
      return;
    }
    onClick();
  }
}
