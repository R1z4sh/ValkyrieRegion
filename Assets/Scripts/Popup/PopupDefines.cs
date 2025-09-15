using System.Collections.Generic;
using UnityEngine;
public enum PopupName {
  PopupDebug,
  PopupMemuOption,
  PopupBattelOption,
  PopupGameClear,
  PopupGameOver,
  PopupShop,
}

public class PopupDefines {
  private static Dictionary<PopupName, string> sceneName = new Dictionary<PopupName, string>() {
    { PopupName.PopupDebug,"Prefabs/Debug/PopupDebug"},
    { PopupName.PopupMemuOption,"Prefabs/Menu/Popup/PopupMenuOption"},
    { PopupName.PopupBattelOption,"Prefabs/Battle/Popup/PopupOption"},
    { PopupName.PopupGameClear,"Prefabs/Battle/Popup/PopupGameClear"},
    { PopupName.PopupGameOver,"Prefabs/Battle/Popup/PopupGameOver"},
    { PopupName.PopupShop,"Prefabs/Menu/Popup/Shop/PopupShopView"},
  };

  public static string PopupPath(PopupName name) {
    return sceneName[name];
  }
}
