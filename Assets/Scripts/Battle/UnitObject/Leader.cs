using System;
using UnityEngine;

public class Leader :MonoBehaviour {
  private bool isStop = false;
  private Action<Vector3, Vector3> onChangeDirection = null;
  private FloatingJoystick joystick;
  private Vector3 direction = Vector3.zero;
  private Rigidbody2D rb = null;
  private float moveSpeed = 2f;
  public void Initialize(FloatingJoystick joystick, Action<Vector3, Vector3> onChangeDirection) {
    this.joystick = joystick;
    this.onChangeDirection = onChangeDirection;
    direction.y = 1f;
    this.joystick.Initialize(SetDirection);
    EventManager.Subscribe<bool>("gameStop", Stop);
    rb = GetComponent<Rigidbody2D>();
    rb.freezeRotation = true;
  }


  private void Stop(bool flag) {
    isStop = flag;
  }

  private void Update() {
    joystick.enabled = !isStop;
    if(isStop) return;
    this.GetComponent<SpriteRenderer>().flipX = joystick.Horizontal > 0;
    //左スティックでの縦移動
    this.transform.position += this.transform.up * joystick.Vertical * moveSpeed * Time.deltaTime;
    //左スティックでの横移動
    this.transform.position += this.transform.right * joystick.Horizontal * moveSpeed * Time.deltaTime;
  }

  private void SetDirection() {
    Vector3 dir = Vector3.up * joystick.Vertical + Vector3.right * joystick.Horizontal;
    if(dir == Vector3.zero)
      return;
    direction = dir;
    onChangeDirection(transform.position, direction);
  }

  private void OnDestroy() {
    EventManager.Unsubscribe<bool>("gameStop", Stop);
  }
}
