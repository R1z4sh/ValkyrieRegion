using System;
using UnityEngine;

public class Leader : MonoBehaviour
{
    private Action<Vector3> onChangeDirection = null;
    private FloatingJoystick joystick;
    private Vector3 direction = Vector3.zero;

    public void Initialize(FloatingJoystick joystick,Action<Vector3> onChangeDirection) 
    {
        this.joystick = joystick;
        this.onChangeDirection = onChangeDirection;
        direction.y = 1f;
        this.joystick.Initialize(SetDirection);
    }


    private void SetDirection()
    {   
        Vector3 dir = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
        if (dir == Vector3.zero) return;
        direction = dir;
        onChangeDirection(direction) ;
    }

    public Vector3 GetReaderPos() {
        return this.GetComponent<RectTransform>().anchoredPosition;
    }
}
