using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController
{
    static PlayerUnitController instance = null;
    public bool summonState = false;
    private int summonUnitId = 0;
    private List<PlayerUnit> alliveUnits = new();
    private Reader reader = null;
    private GameObject unitPrefab = null;
    private GameObject unitRoot = null;
    private SummonPoint point = null;
    public static PlayerUnitController Instance(){
        if (instance == null)instance =  new PlayerUnitController();
        return instance;
    }

    public PlayerUnitController()
    {
        unitPrefab = Resources.Load<GameObject>("Prefabs/Battle/Unit/PlayerUnit");
        reader = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/ReaderRoot/Reader").GetComponent<Reader>();
        unitRoot = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/PlayerUnits");
        point = GameObject.Find("SceneRoot/GameScene(Clone)/Canvas/SummonPoint").GetComponent<SummonPoint>();
        point.PointReset();
    }
 
    public void Summon(GameObject summonUnit) {
        if (!summonState) return;
        PlayerUnit comp = summonUnit.GetComponent<PlayerUnit>();
        comp.Initialize(summonUnitId, 10);
        summonState = false;
        alliveUnits.Add(comp);
    }

    public void Summon(int id,int cost){
        if (cost > point.GetPoint()) return;
        Vector3 pos = reader.GetReaderPos();
        Vector3 summonPos = pos + reader.GetDirection().normalized * 50;
        PlayerUnit summonUnit = Object.Instantiate(unitPrefab, pos, Quaternion.identity).GetComponent<PlayerUnit>();
        summonUnit.transform.SetParent(unitRoot.transform);
        summonUnit.Initialize(id, 10);
        summonUnit.GetComponent<RectTransform>().anchoredPosition = summonPos;  
        alliveUnits.Add(summonUnit);
        point.setPoint(cost);
    }
}
