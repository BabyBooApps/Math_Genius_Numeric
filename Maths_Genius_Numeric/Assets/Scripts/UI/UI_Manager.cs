using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public HomeScreen Home_Screen;
    public Counting_Screen counting_Screen;
    public AdditionScreen addition_Screen;
    public Substraction_Screen substraction_Screen;
    public Compare_Screen compare_Screen;
    public Multiply_Screen multiply_Screen;
    public Pattern_Screen pattern_Screen;
    public Init_Screen init_Screen;
    public SettingsScreen settings_Screen;

    private void Awake()
    {
        // Ensure there is only one instance of this class
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Activate_Home_Screen()
    {
        Home_Screen.gameObject.SetActive(true);
    }

    public void Activate_Couting_Scene()
    {
        counting_Screen.gameObject.SetActive(true);
        GamePlayManager.instance.Start_Counting_Scene();
    }

    public void Activate_Addition_Scene()
    {
        addition_Screen.gameObject.SetActive(true);
        GamePlayManager.instance.Start_Addition_Level();
    }

    public void Activate_Compare_Level()
    {
        compare_Screen.gameObject.SetActive(true);
        GamePlayManager.instance.Start_Compare_Level();
    }


    public void Activate_Substraction_Level()
    {
        substraction_Screen.gameObject.SetActive(true);
        GamePlayManager.instance.Start_Substraction_Level();
    }

    public void Activate_Mutilplication_Level_UI()
    {
        multiply_Screen.gameObject.SetActive(true);
        multiply_Screen.Tabel_Selection_Screen.gameObject.SetActive(true);
       
    }

    public void Start_Multiplication_Level(int tableVal)
    {
        GamePlayManager.instance.Start_Multiplication_Level(tableVal);
    }

    public void on_Counting_Back_Button_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();

        GamePlayManager.instance.Disable_Counting_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void on_Addition_Back_Button_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();

        GamePlayManager.instance.Disable_Addition_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void on_Substraction_Back_Button_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        GamePlayManager.instance.Disable_Substraction_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void on_Compare_BackButton_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        GamePlayManager.instance.Disable_Compare_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void on_Multiply_BackButton_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        GamePlayManager.instance.Disable_Multiply_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void Activate_Pattern_Level()
    {
        pattern_Screen.gameObject.SetActive(true);
        GamePlayManager.instance.Acitivate_Pattern_Level();
    }

    public void On_Pattern_BackButton_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        GamePlayManager.instance.Disable_Pattern_Level();
        Home_Screen.gameObject.SetActive(true);

        AdsManager.Instance.interstitial.ShowAd();
    }

    public void OnHomeScreen_Back_Btn_Pressed()
    {
        AudioManager.instance.Play_Btn_Click();
        init_Screen.gameObject.SetActive(true);
    }

    public void OnSettings_Button_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        settings_Screen.gameObject.SetActive(true);
    }

}
