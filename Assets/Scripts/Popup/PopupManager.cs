using System.Threading.Tasks;
using UnityEngine;

public class PopupManager {
  private static PopupManager instance = null;

  public static PopupManager Instance() {
    if (instance == null) {
      instance = new PopupManager();
    }
    return instance;
  }



  public async Task<TResult> ShowPopup<TData, TResult, TPopup>(TData data , GameObject popupPrefab)
      where TPopup : PopupBase<TData , TResult> {
    var tcs = new TaskCompletionSource<TResult>();
    GameObject obj = GameObject.Find("PopupRoot");

    var popupGO = GameObject.Instantiate(popupPrefab , obj.transform);

    if (popupGO.TryGetComponent<TPopup>(out var popup)) {
      popup.Initialize(data , tcs);
      popup.Show();
    } else {
      Debug.LogError($"Prefab に {typeof(TPopup)} がアタッチされていません");
      tcs.SetException(new MissingComponentException());
    }

    return await tcs.Task;
  }
}
