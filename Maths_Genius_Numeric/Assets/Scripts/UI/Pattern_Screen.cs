using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Screen : MonoBehaviour
{
   public void On_BackButton_Pressed()
    {
        UI_Manager.instance.On_Pattern_BackButton_Pressed();
        this.gameObject.SetActive(false);
    }
}
