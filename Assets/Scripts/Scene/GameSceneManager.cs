using System.Threading.Tasks;
using UnityEngine;

public class GameSceneManager {
  private static GameSceneManager instance = null;
  private GameObject root = null;
  private Animation loadingAnimation = null;
  public GameSceneManager() {
    this.root = GameObject.Find("SceneRoot");
    this.loadingAnimation = GameObject.Find("Loading").GetComponent<Animation>();
  }

  public static GameSceneManager Instance() {
    if(instance == null)
      instance = new GameSceneManager();
    return instance;
  }

  public async void ChangeScene(SceneName scene, SceneData data = null) {
    DestroyScene(root.transform);
    string path = SceneDefine.ScenePath(scene);

    //読み込み中の画面を表示
    loadingAnimation.Play();

    Load(scene, data);

    //読み込み中の画面を非表示
    // loadingAnimation.Play("LoadOff");
  }


  private async Task Load(SceneName scene, SceneData data) {
    string path = SceneDefine.ScenePath(scene);
    GameObject prefab = Resources.Load<GameObject>(path);
    if(prefab == null) {
      Debug.LogError("指定のパスにプレファブが見つかりません" + path);
      return;
    }
    GameObject sceneObj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    sceneObj.transform.parent = root.transform;
    await sceneObj.GetComponent<SceneBase>().Initialize(data);
  }


  private void DestroyScene(Transform root) {
    foreach(Transform child in root) {
      child.GetComponent<SceneBase>().Fainalize();
      Object.Destroy(child.gameObject);
    }
  }
}
