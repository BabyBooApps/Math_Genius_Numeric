using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppRewards : MonoBehaviour
{
    public static InAppRewards Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        // PlayerPrefs.DeleteAll();
    }
    public void On_No_Ads_Purchase_Success()
    {
        PlayerPrefsManager.Instance.Set_No_Ads_Status(true);
        UI_Manager.instance.init_Screen.Set_No_Ads_Btn();
        AdsManager.Instance.banner.DestroyAd();
     //   UI_Manager.Instance.Animate_Toast("No Ads Bought");
    }

    public void On_Coins_Bought(int count)
    {
      //  GamePlay.Instance.AddCoins(count);
       // UI_Manager.Instance.Animate_Toast("Owned " + count.ToString() + "Coins");

    }
}
