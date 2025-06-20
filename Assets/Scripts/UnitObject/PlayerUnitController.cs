using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerUnitController
{
    static PlayerUnitController instance = null;
    public bool summonState = false;
    private int summonUnitId = 0;
    private List<PlayerUnit> alliveUnits = new();
    private Reader reader = null;
    private GameObject unitPrefab = null;
    private GameObject unitRoot = null;
   
    public static PlayerUnitController Instance(){
        if (instance == null)instance =  new PlayerUnitController();
        return instance;
    }

    public PlayerUnitController()
    {
        unitPrefab = Resources.Load<GameObject>("Prefabs/Battle/Unit/PlayerUnit");
        reader = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/ReaderRoot/Reader").GetComponent<Reader>();
        unitRoot = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/PlayerUnits");
    }
 
    public void Summon(GameObject summonUnit) {
        if (!summonState) return;
        PlayerUnit comp = summonUnit.GetComponent<PlayerUnit>();
        comp.Initialize(summonUnitId, 10);
        summonState = false;
        alliveUnits.Add(comp);
    }

    public void Summon(int id){
        Vector3 pos = reader.GetReaderPos();
        Vector3 summonPos = pos + reader.GetDirection().normalized * 50;
        PlayerUnit summonUnit = Object.Instantiate(unitPrefab, pos, Quaternion.identity).GetComponent<PlayerUnit>();
        summonUnit.transform.SetParent(unitRoot.transform);
        summonUnit.Initialize(id, 10);
        summonUnit.GetComponent<RectTransform>().anchoredPosition = summonPos;  
        alliveUnits.Add(summonUnit);
        Debug.Log(summonPos);  
        Debug.Log(reader.GetDirection());  
    }
}
