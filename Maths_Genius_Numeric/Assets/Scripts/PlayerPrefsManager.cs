using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager Instance;

    public const string NoAds_String = "NoAds";
    public bool DeleteAllPlayerPrefs;

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
        if (DeleteAllPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }

    }

    public void Set_No_Ads_Status(bool adsStatus)
    {
        int adStatus = (adsStatus == true ? 1 : 0);
        PlayerPrefs.SetInt(NoAds_String, adStatus);

    }

    public bool GetNoAdsStatus()
    {
        return PlayerPrefs.GetInt(NoAds_String) == 1 ? true : false;
    }
}
