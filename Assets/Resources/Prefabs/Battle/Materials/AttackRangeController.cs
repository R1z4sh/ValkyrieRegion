using UnityEngine;

public class AttackRangeController :MonoBehaviour {
  [SerializeField] private Material rangeMaterial = null;
  private Material privateMaterial = null;


  public void Initialize(float min, float max) {
    privateMaterial = Instantiate(rangeMaterial);
    this.GetComponent<SpriteRenderer>().material = privateMaterial;
    privateMaterial.SetFloat("_MinRadius", min);
    privateMaterial.SetFloat("_MaxRadius", max);
    SetActive(false);
  }

  public void SetActive(bool flag) {
    this.gameObject.SetActive(flag);
  }
}
