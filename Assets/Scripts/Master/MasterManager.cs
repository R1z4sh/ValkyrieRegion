using System.Collections.Generic;
using UnityEngine;


public class MasterManager {
  public static TMaster LoadMasterData<TMaster>(string path) {
    // ResourcesからJSONテキストを読み込む
    TextAsset jsonText = Resources.Load<TextAsset>(path);
    if (jsonText == null) {
      Debug.LogError("JSONファイルが見つかりませんでした！");
      return default;
    }
    TMaster masterData = JsonUtility.FromJson<TMaster>(jsonText.text);
    return masterData;
  }
}
