
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEnhanceView : PopupBase<List<PopupEnhanceItemModel>,bool>
{
  [SerializeField]
  private ScrollRect scroll;
  [SerializeField]
  private PopupEnhanceItemView BaseItem;
  [SerializeField]
  private float space;
  [SerializeField]
  private VerticalLayoutGroup scrollLayout;

  [SerializeField] private Button closeButton = null;
  public override void Initialize(List<PopupEnhanceItemModel> data, TaskCompletionSource<bool> res)
  {

    closeButton.onClick.AddListener(() => CloseWithResult(true));

    base.Initialize(data, res);

    var objectHide = 0.0f;
    for (int i = 0; i < data.Count; i++) 
    {
      var itemModel = data[i];
      var obj = Instantiate<GameObject>(BaseItem.gameObject);
      var objRect = obj.GetComponent<RectTransform>();
      obj.transform.SetParent(scroll.content);
      obj.transform.localPosition = Vector3.zero;
      obj.transform.localScale = Vector3.one;

      var item = obj.GetComponent<PopupEnhanceItemView>();
      item.Initialize();
      item.UpdateView(itemModel);
      objectHide = objRect.sizeDelta.y;
    }
    scrollLayout.spacing = space;
    var hight = (data.Count * space) + (data.Count * objectHide);
    scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x, hight);
  }
  public override void Hide()
  {
    base.Hide();
  }

  public override void Show()
  {
    base.Show();
  }
  public void UpdateView() 
  {
  
  }

}
