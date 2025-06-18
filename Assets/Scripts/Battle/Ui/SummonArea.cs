using UnityEngine;

public class SummonArea : MonoBehaviour
{
    public GameObject prefabToSpawn; 
    public LayerMask raycastLayers;

    void Update(){
        if (!PlayerUnitController.Instance().summonState) return;
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero, Mathf.Infinity, raycastLayers);
            if (hit.collider != null){
                GameObject obj = Instantiate(prefabToSpawn, mouseWorldPos2D, Quaternion.identity);
                obj.transform.SetParent(this.transform,false);
            }
            else Debug.Log("‚ ‚½‚Á‚Ä‚¢‚Ü‚¹‚ñ");
        }
    }
}
