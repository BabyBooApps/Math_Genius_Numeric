using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Substraction_Screen : MonoBehaviour
{
    public Button Back_Button;

    public void On_BackButton_Pressed()
    {
        UI_Manager.instance.on_Substraction_Back_Button_Pressed();
        this.gameObject.SetActive(false);
    }
}
