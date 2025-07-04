using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public interface SceneData { }

public class SceneBase :MonoBehaviour {
  public virtual async Task Initialize(SceneData data = null) { }
  public virtual void Fainalize() { }
}
