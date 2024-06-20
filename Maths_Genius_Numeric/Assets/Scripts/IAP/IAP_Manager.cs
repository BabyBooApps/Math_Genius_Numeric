using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAP_Manager : MonoBehaviour
{
    public const string NoAds_ID = "noads_math_genius_numeric";
    

    public void OnPurchaseComplete(Product product)
    {
        switch(product.definition.id)
        {
            case NoAds_ID:
                Debug.Log("Bought No Ads Successfully!!!");
                InAppRewards.Instance.On_No_Ads_Purchase_Success();
                break;
            

        }
    }

    public void OnPurchaseFailed(Product product , PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + "Failed Because : " + failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailedEventArgs failureReason)
    {
        Debug.Log(product.definition.id + "Failed Because : " + failureReason);
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
    {
        if (p.reason == PurchaseFailureReason.PurchasingUnavailable)
        {
            // IAP may be disabled in device settings.
            Debug.Log(i.definition.id + "Failed Because : " + p.message);
        }
    }
}
