using System.Threading.Tasks;
using UnityEngine;

public class GameSceneManager {
  private static GameSceneManager instance = null;
  private GameObject root = null;

  public GameSceneManager() {
    this.root = GameObject.Find("SceneRoot");
  }

  public static GameSceneManager Instance() {
    if (instance == null)
      instance = new GameSceneManager();
    return instance;
  }

  public void  ChangeScene(SceneName scene) {
    DestroyScene(root.transform);
    string path = SceneDefine.ScenePath(scene);
    GameObject prefab = Resources.Load<GameObject>(path);
    if (prefab == null) {
      Debug.LogError("指定のパスにプレファブが見つかりません" + path);
      return;
    }


    GameObject sceneObj = Object.Instantiate(prefab , Vector3.zero , Quaternion.identity);
    sceneObj.transform.parent = root.transform;
    sceneObj.GetComponent<SceneBase>().Initialize();

  }

  private void DestroyScene(Transform root) {
    foreach (Transform child in root) {
      child.GetComponent<SceneBase>().Fainalize();
      Object.Destroy(child.gameObject);
    }
  }
}
