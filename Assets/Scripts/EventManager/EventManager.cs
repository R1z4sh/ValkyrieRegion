using System;
using System.Collections.Generic;

public static class EventManager {
  private static Dictionary<string , Delegate> eventTable = new();

  public static void Subscribe<T>(string eventName , Action<T> listener) {
    if (eventTable.TryGetValue(eventName , out var existingDelegate)) {
      eventTable[eventName] = Delegate.Combine(existingDelegate , listener);
    } else {
      eventTable[eventName] = listener;
    }
  }

  public static void Unsubscribe<T>(string eventName , Action<T> listener) {
    if (eventTable.TryGetValue(eventName , out var existingDelegate)) {
      eventTable[eventName] = Delegate.Remove(existingDelegate , listener);
    }
  }

  public static void Trigger<T>(string eventName , T arg) {
    if (eventTable.TryGetValue(eventName , out var d)) {
      if (d is Action<T> callback) {
        callback.Invoke(arg);
      } else {
        throw new Exception($"�C�x���g {eventName} �͈قȂ�^�œo�^����Ă��܂�");
      }
    }
  }

  // �S�C�x���g����
  public static void Clear() {
    eventTable.Clear();
  }
}