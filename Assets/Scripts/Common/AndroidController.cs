using UnityEngine;

/// <summary>
/// アンドロイドバッグキー制御
/// </summary>
public class AndroidController : Singleton<AndroidController>
{
#if UNITY_ANDROID || UNITY_EDITOR

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {


    }
  }
#endif   
}
