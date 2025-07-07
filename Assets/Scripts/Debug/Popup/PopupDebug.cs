using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu {
  public string debugName;
  public Action onClick;
}

public class PopupDebug :PopupBase<bool, bool> {
  [SerializeField] private GameObject debugRoot = null;
  [SerializeField] private GameObject debugItem = null;
  private List<DebugMenu> debugMenuList = new List<DebugMenu>();

  public override void Initialize(bool none, System.Threading.Tasks.TaskCompletionSource<bool> tcs) {
    base.Initialize(none, tcs);
    CraeteDebugMenu();
    CreateDebugButton();
  }

  private void AddDebugMenu(string name, Action callback) {
    debugMenuList.Add(new DebugMenu() {
      debugName = name,
      onClick = callback
    });
  }
  //ここにデバッグの昨日を記載する
  private void CraeteDebugMenu() {
    AddDebugMenu("close", () => {
      CloseWithResult(true);
    });
    AddDebugMenu("dataload", () => {
      MasterManager.LoadSeverData();
    });
  }

  private void CreateDebugButton() {
    foreach(DebugMenu menu in debugMenuList) {
      DebugButtonItem item = Instantiate(debugItem, debugRoot.transform).GetComponent<DebugButtonItem>();
      item.Initialize(menu.debugName, menu.onClick);
    }
  }
}
