using System.Collections.Generic;
using UnityEngine;


public class MasterManager {
  public static TMaster LoadMasterData<TMaster>(string path) {
    // Resources����JSON�e�L�X�g��ǂݍ���
    TextAsset jsonText = Resources.Load<TextAsset>(path);
    if (jsonText == null) {
      Debug.LogError("JSON�t�@�C����������܂���ł����I");
      return default;
    }
    TMaster masterData = JsonUtility.FromJson<TMaster>(jsonText.text);
    return masterData;
  }
}
