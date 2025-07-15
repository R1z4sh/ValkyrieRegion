using System.Threading.Tasks;
using UnityEngine;

public class PopupManager {
  private static PopupManager instance = null;

  public static PopupManager Instance() {
    if(instance == null) {
      instance = new PopupManager();
    }
    return instance;
  }

  public async Task<TResult> ShowPopup<TData, TResult, TPopup>(PopupName name, TData data)
    where TPopup : PopupBase<TData, TResult> {
    var tcs = new TaskCompletionSource<TResult>();
    GameObject obj = GameObject.Find("PopupRoot");
    GameObject prefab = Resources.Load<GameObject>(PopupDefines.PopupPath(name));
    var popupGO = GameObject.Instantiate(prefab, obj.transform);

    if(popupGO.TryGetComponent<TPopup>(out var popup)) {
      popup.Initialize(data, tcs);
      popup.Show();
    } else {
      Debug.LogError($"Prefab に {typeof(TPopup)} がアタッチされていません");
      tcs.SetException(new MissingComponentException());
    }

    return await tcs.Task;
  }
}
