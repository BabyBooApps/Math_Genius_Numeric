using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counting_Screen : MonoBehaviour
{
    public Button BackButton;

    public void On_BackButton_Pressed()
    {
        UI_Manager.instance.on_Counting_Back_Button_Pressed();
        this.gameObject.SetActive(false);
    }

}
