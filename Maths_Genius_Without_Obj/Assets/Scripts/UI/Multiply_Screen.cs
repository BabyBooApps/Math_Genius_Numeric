using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiply_Screen : MonoBehaviour
{
    public Button Back_Button;
    public GameObject Tabel_Selection_Screen;
    public int Tabel_Val = 1;

    public void Activate_Table_Selection_Screen()
    {
        Tabel_Selection_Screen.gameObject.SetActive(true);
    }

    public void On_Start_Level_Btn_Click(int tabelVal)
    {
        AudioManager.instance.Play_Btn_Click();
        Tabel_Val = tabelVal;
        Tabel_Selection_Screen.gameObject.SetActive(false);
        UI_Manager.instance.Start_Multiplication_Level(Tabel_Val);
    }

    public void On_BackButton_Pressed()
    {
        UI_Manager.instance.on_Multiply_BackButton_Pressed();
        this.gameObject.SetActive(false);

    }
}
