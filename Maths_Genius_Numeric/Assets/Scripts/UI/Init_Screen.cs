using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_Screen : MonoBehaviour
{
    public Button NoAdsBtn;
    private void Start()
    {
        NoAdsBtn.gameObject.SetActive(!PlayerPrefsManager.Instance.GetNoAdsStatus());
    }
    public void On_Play_Btn_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        UI_Manager.instance.Activate_Home_Screen();
        this.gameObject.SetActive(false);
    }

    public void On_Our_Games_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Application.OpenURL("https://play.google.com/store/apps/dev?id=6487105028651572662");
    }

    public void On_Rating_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SuperWiz.Math_Genius_Normal");
    }

    public void OnSettings_ButtonClick()
    {
        UI_Manager.instance.OnSettings_Button_Click();
    }

    public void Set_No_Ads_Btn()
    {
        //NoAdsBtn.enabled = !PlayerPrefsManager.Instance.GetNoAdsStatus();
        NoAdsBtn.gameObject.SetActive(!PlayerPrefsManager.Instance.GetNoAdsStatus());
    }
}
