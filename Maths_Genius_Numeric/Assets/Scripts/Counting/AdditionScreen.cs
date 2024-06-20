using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionScreen : MonoBehaviour
{
    public Button BackButton;
    

    public void On_Back_ButtonPressed()
    {
        UI_Manager.instance.on_Addition_Back_Button_Pressed();
        this.gameObject.SetActive(false);
    }
}
