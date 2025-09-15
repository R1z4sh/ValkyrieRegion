using UnityEditor;
using UnityEngine;


public class PopupShopItemModel
{

  private int id;
  public int Id { get => id; }

  private string name;
  public string Name { get => name; }

  public void InjectData(int _id, string _name) 
  {
    id = _id;
    name = _name;
  }

}
