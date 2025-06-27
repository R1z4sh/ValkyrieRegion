using System.Threading.Tasks;
using UnityEngine;

public abstract class PopupBase<TData, TResult> :MonoBehaviour {
  private TaskCompletionSource<TResult> result;

  public virtual async void Initialize(TData data, TaskCompletionSource<TResult> res) {
    result = res;
  }

  public virtual void Show() {
    gameObject.SetActive(true);
  }

  public virtual void Hide() {
    gameObject.SetActive(false);
    Destroy(gameObject);
  }

  protected void CloseWithResult(TResult res) {
    result?.SetResult(res);
    Hide();
  }
}