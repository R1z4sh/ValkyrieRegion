using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController:MonoBehaviour
{
    [SerializeField] GameObject unitPrefab = null;
    private List<PlayerUnit> alliveUnits = new();
    private BattleController battleController = null;
    private Vector3 direction = new Vector3(0f, -1f, 0);

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Initialize(BattleController controller)
    {
        battleController = controller;
    }


    public PlayerUnitController()
    {
    }
 

    public void Summon(int id,int cost){
        if (cost >battleController.GetSummonPoint()) return;
        Vector3 summonPos = Vector3.zero + direction.normalized * 50;
        PlayerUnit summonUnit = Object.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerUnit>();
        summonUnit.transform.SetParent(this.transform);
        summonUnit.Initialize(id, 10);
        summonUnit.GetComponent<RectTransform>().anchoredPosition = summonPos;  
        alliveUnits.Add(summonUnit);
        EventManager.Trigger<int>("UseCost", cost);
    }
}
