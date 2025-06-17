using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface SceneData { }

public class SceneBase : MonoBehaviour {
  public virtual void Initialize(SceneData data = null) { }
  public virtual void Fainalize() { }
}
