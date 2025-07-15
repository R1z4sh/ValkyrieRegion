using System.Collections.Generic;
using UnityEngine;
public enum PopupName {
  PopupDebug,
  PopupMemuOption,
  PopupBattelOption,
  PopupGameClear,
  PopupGameOver,
}

public class PopupDefines {
  private static Dictionary<PopupName, string> sceneName = new Dictionary<PopupName, string>() {
    { PopupName.PopupDebug,"Prefabs/Debug/PopupDebug"},
    { PopupName.PopupMemuOption,"Prefabs/Menu/Popup/PopupMenuOption"},
    { PopupName.PopupBattelOption,"Prefabs/Battle/Popup/PopupOption"},
    { PopupName.PopupGameClear,"Prefabs/Battle/Popup/PopupGameClear"},
    { PopupName.PopupGameOver,"Prefabs/Battle/Popup/PopupGameOver"},
  };

  public static string PopupPath(PopupName name) {
    return sceneName[name];
  }
}
