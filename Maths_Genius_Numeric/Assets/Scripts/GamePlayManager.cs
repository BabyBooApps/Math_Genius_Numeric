using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;

    public Counting_Scene counting_Scene;
    public Addition_Level addition_Level;
    public Comapre_Level Compare_Level;
    public Sunbtraction_Level substraction_Level;
    public Multiplication multiplication_Level;
    public Pattern_Level pattern_Level;

    public Level_State level_State;

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
        //GamePlayManager.instance.level_State = Level_State.Pattern;
         //counting_Scene.InitializeLevel();
         //level_State = Level_State.Counting;
    }

    public void Start_Counting_Scene()
    {
        counting_Scene.gameObject.SetActive(true);
        level_State = Level_State.Counting;
        counting_Scene.InitializeLevel();
       
    }

    public void Disable_Counting_Level()
    {
        counting_Scene.ClearLevel();
        level_State = Level_State.None;
        counting_Scene.gameObject.SetActive(false);

    }

    public void Start_Addition_Level()
    {
        addition_Level.gameObject.SetActive(true);
        level_State = Level_State.Addition;
        addition_Level.Start_Level();
       
    }

    public void Disable_Addition_Level()
    {
        addition_Level.Clear_Grid_Objects();
        level_State = Level_State.None;
        addition_Level.gameObject.SetActive(false);
        GlobalClickCounter.ResetClickCounter();

    }

    public void Start_Compare_Level()
    {
        Compare_Level.gameObject.SetActive(true);
        level_State = Level_State.Compare;
        Compare_Level.Reset_Level();
    }

    public void Disable_Compare_Level()
    {
        Compare_Level.Clear_Grid_Objects();
        level_State = Level_State.None;
        GlobalClickCounter.ResetClickCounter();
        Compare_Level.gameObject.SetActive(false);
    }

    public void Start_Substraction_Level()
    {
        level_State = Level_State.Substraction;
        substraction_Level.gameObject.SetActive(true);
        substraction_Level.Start_Level();
    }

    public void Disable_Substraction_Level()
    {
        level_State = Level_State.None;
        substraction_Level.Clear_Grid_Objects();
        substraction_Level.gameObject.SetActive(false);
        GlobalClickCounter.ResetClickCounter();

    }

    public void Start_Multiplication_Level(int tableVal)
    {

        level_State = Level_State.Multiplication;
        multiplication_Level.gameObject.SetActive(true);
        multiplication_Level.Start_Level(tableVal);
    }

    public void Disable_Multiply_Level()
    {
        level_State = Level_State.None;
        GlobalClickCounter.ResetClickCounter();
        multiplication_Level.Clear_Grid_Objects();
        multiplication_Level.gameObject.SetActive(false);
    }

    public void Acitivate_Pattern_Level()
    {
        level_State = Level_State.Pattern;
        GlobalClickCounter.ResetClickCounter();
        pattern_Level.gameObject.SetActive(true);
        pattern_Level.Reset_Level();
    }

    public void Disable_Pattern_Level()
    {
        level_State = Level_State.None;
        GlobalClickCounter.ResetClickCounter();
        pattern_Level.Clear_Grid_Objects();
        pattern_Level.gameObject.SetActive(false);
    }

    
}

public enum Level_State
{
    None,
    Counting,
    Addition,
    Compare,
    Substraction,
    Multiplication,
    Pattern
}
