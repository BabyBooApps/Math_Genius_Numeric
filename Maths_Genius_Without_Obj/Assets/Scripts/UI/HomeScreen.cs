using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{

    public Button Counting_Button;
    public Button Addition_Button;
    public Button Substraction_Button;
    public Button Multiplication_Button;
    public Button Compare_Button;
    public Button Pattern_Button;
    public Button Back_Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void On_Back_Button_Pressed()
    {

        UI_Manager.instance.OnHomeScreen_Back_Btn_Pressed();
        this.gameObject.SetActive(false);
    }

    public void On_Counting_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Counting Button click");
        UI_Manager.instance.Activate_Couting_Scene();
        this.gameObject.SetActive(false);
    }

    public void On_Addition_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Addition Button click");
        UI_Manager.instance.Activate_Addition_Scene();
        this.gameObject.SetActive(false);
    }

    public void On_Substraction_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Substraction Button click");
        UI_Manager.instance.Activate_Substraction_Level();
        this.gameObject.SetActive(false);
        
    }

    public void On_Multiply_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Multiply Button click");
        UI_Manager.instance.Activate_Mutilplication_Level_UI();
        this.gameObject.SetActive(false);

    }
    public void On_Compare_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Compare Button click");
        UI_Manager.instance.Activate_Compare_Level();
        this.gameObject.SetActive(false);
    }

    public void On_Pattern_Btn_Click()
    {
        AudioManager.instance.Play_Btn_Click();
        Debug.Log("Pattern Button click");
        UI_Manager.instance.Activate_Pattern_Level();
        this.gameObject.SetActive(false);
    }




}
