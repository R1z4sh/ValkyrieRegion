using System.Collections.Generic;
using UnityEngine;
public enum PopupName {
  PopupDebug,
  PopupMemuOption,
  PopupBattelOption,
  PopupGameClear,
  PopupGameOver,
  PopupShop,
  PopupMission,
  PopupEnhance,
}

public class PopupDefines {
  private static Dictionary<PopupName, string> sceneName = new Dictionary<PopupName, string>() {
    { PopupName.PopupDebug,"Prefabs/Debug/PopupDebug"},
    { PopupName.PopupMemuOption,"Prefabs/Menu/Popup/PopupMenuOption"},
    { PopupName.PopupBattelOption,"Prefabs/Battle/Popup/PopupOption"},
    { PopupName.PopupGameClear,"Prefabs/Battle/Popup/PopupGameClear"},
    { PopupName.PopupGameOver,"Prefabs/Battle/Popup/PopupGameOver"},
    { PopupName.PopupShop,"Prefabs/Menu/Popup/Shop/PopupShopView"},
    { PopupName.PopupMission,"Prefabs/Menu/Popup/Mission/PopupMissionView"},
    { PopupName.PopupEnhance,"Prefabs/Menu/Popup/Enhance/PopupEnhanceView"},
  };

  public static string PopupPath(PopupName name) {
    return sceneName[name];
  }
}
