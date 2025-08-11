using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{

  public enum SaveDataKey 
  {//セーブしたいデータのKey
  
  }

  public void SaveData(int _data, SaveDataKey _key)
  {
    PlayerPrefs.SetInt(_key.ToString(), _data);
  }
  public void SaveData(float _data, SaveDataKey _key)
  {
    PlayerPrefs.SetFloat(_key.ToString(), _data);
  }
  public void SaveData(string _data, SaveDataKey _key)
  {
    PlayerPrefs.SetString(_key.ToString(), _data);
  }
  public void SaveData<T>(T _data,SaveDataKey _key) 
  {
    var data = Serialize(_data);
    SaveData(data,_key);
  }

  public int GetInt(SaveDataKey _key,int _defaltValue) 
  {
    return PlayerPrefs.GetInt(_key.ToString(),_defaltValue);
  }

  public float GetFloat(SaveDataKey _key, float _defaltValue)
  {
    return PlayerPrefs.GetFloat(_key.ToString(), _defaltValue);
  }

  public string GetString(SaveDataKey _key, string _defaltValue)
  {
    return PlayerPrefs.GetString(_key.ToString(), _defaltValue);
  }

  public T GetData<T>(SaveDataKey _key, T _defaltValue) 
  {
    var data = PlayerPrefs.GetString(_key.ToString());

    var saveData = _defaltValue;
    if(data != null && data != "") 
    {
      saveData = Deserialize<T>(data);
    }

    return saveData;
  }

  public void Save() 
  {
    PlayerPrefs.Save();
  }


  //=================================================================================
  //シリアライズ、デシリアライズ
  //=================================================================================

  private static string Serialize<T>(T obj)
  {
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    MemoryStream memoryStream = new MemoryStream();
    binaryFormatter.Serialize(memoryStream, obj);
    return Convert.ToBase64String(memoryStream.GetBuffer());
  }

  private static T Deserialize<T>(string str)
  {
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(str));
    return (T)binaryFormatter.Deserialize(memoryStream);
  }
}
