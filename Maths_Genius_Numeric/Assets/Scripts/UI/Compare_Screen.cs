using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compare_Screen : MonoBehaviour
{
    public Button BackButton;

    public void on_BackButton_Pressed()
    {
        UI_Manager.instance.on_Compare_BackButton_Pressed();
        this.gameObject.SetActive(false);

    }
}
