using UnityEngine;

public class Reader : MonoBehaviour
{
    private FloatingJoystick joystick;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        direction.y = 1f;
        joystick = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/Floating Joystick").GetComponent<FloatingJoystick>();
        joystick.Initialize(SetDirection);
    }


    private void SetDirection()
    { Vector3 dir = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
     if(dir != Vector3.zero)   direction = dir; 
    }

    public Vector3 GetDirection() { return direction; }

    public Vector3 GetReaderPos() {
        return this.GetComponent<RectTransform>().anchoredPosition;
    }
}
