using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Networking;

public class IAPManager :MonoBehaviour ,IStoreListener
{
  private IStoreController m_StoreController;

  /// <summary>
  /// 購入する商品の初期化
  /// </summary>
  /// <param name="_productId"></param>
  public void InitializePurchasing(string _productId)
  {
    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
    builder.AddProduct(_productId, ProductType.Consumable);
    UnityPurchasing.Initialize(this, builder);
  }
  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
    Debug.Log("初期化完了");
    m_StoreController = controller;

    var googleExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

  }

  /// <summary>
  /// 初期化失敗
  /// </summary>
  /// <param name="error"></param>
  public void OnInitializeFailed(InitializationFailureReason error)
  {
  }

  /// <summary>
  /// 初期化失敗
  /// </summary>
  /// <param name="error"></param>
  /// <param name="message"></param>
  public void OnInitializeFailed(InitializationFailureReason error, string message)
  {
  }

  /// <summary>
  /// 購入失敗
  /// </summary>
  /// <param name="product"></param>
  /// <param name="failureReason"></param>
  public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
  {
  }

  /// <summary>
  /// 購入結果
  /// </summary>
  /// <param name="purchaseEvent"></param>
  /// <returns></returns>
  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
  {
    var product = purchaseEvent.purchasedProduct;
    StartCoroutine(SendReceiptToServer(product));

    // ここではレシート未送信なのでPendingを返しトランザクションを張る
    return PurchaseProcessingResult.Pending;
  }

  private IEnumerator SendReceiptToServer(Product product)
  {
    // サーバーにレシート送信（ダミー）
    var request = UnityWebRequest.Get("https://example.com/receipt/");
    yield return request.SendWebRequest();

    if (request.responseCode < 300)
    {
      Debug.Log($"無事レシートを送れたのでトランザクション完了: {product.definition.id}");
      m_StoreController.ConfirmPendingPurchase(product);
    }

    // 別途ユーザーデータをもとにアイテム反映
  }

  /// <summary>
  /// 購入処理
  /// </summary>
  /// <param name="_productId"></param>
  public void Purchase(string _productId) 
  {
    m_StoreController.InitiatePurchase(_productId);
  }

  void OnPurchaseDeferred() 
  {
  
  }

}
