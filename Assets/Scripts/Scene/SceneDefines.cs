using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName {
  Title, DeckEdit, Menu, Game
}
class SceneDefine {

  private static Dictionary<SceneName , string> sceneName = new Dictionary<SceneName , string>() {
    { SceneName.Title,"Prefabs/Scenes/TitleScene" } ,
    { SceneName.DeckEdit,"Prefabs/Scenes/DeckEdit" } ,
    { SceneName.Menu,"Prefabs/Scenes/MenuScene" } ,
    { SceneName.Game,"Prefabs/Scenes/GameScene" } ,
  };

  public static string ScenePath(SceneName name) {
    return sceneName[name];
  }
}