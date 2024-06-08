using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Answer_Tile : MonoBehaviour
{
    public int Id;
    public string CompareId;
    
    public void Set_Id(int id)
    {
        Id = id;
    }

    public void Set_Compare_Id(string val)
    {
        CompareId = val;
    }
}
