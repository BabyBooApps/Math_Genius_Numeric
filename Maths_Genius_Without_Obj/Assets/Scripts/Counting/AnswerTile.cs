using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerTile : MonoBehaviour
{
    Counting_Scene counting_Level;
    public TextMeshPro Value_Text;
    public SpriteRenderer Answer_Sprite;
    public int Id = 0;
    public string Compare_Id = "";
    bool IsClicked = false;
    SpriteRenderer sp;
    public Target_Answer_Tile targetObj;

    public Vector3 initial_Pos;

    public int CurrentId;


    private void Awake()
    {
        initial_Pos = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        counting_Level = FindAnyObjectByType(typeof(Counting_Scene)) as Counting_Scene;
        sp = GetComponent<SpriteRenderer>();
       
    }

    public void Set_Id(int id)
    {
        Id = id;
    }

    public void Set_Ans_Sprite(Sprite sp)
    {
        Answer_Sprite.sprite = sp;
    }
    public void Reset_Tile_Pos()
    {
        this.transform.position = initial_Pos;
    }

    private void OnMouseDown()
    {
        if(!IsClicked)
        {
            Debug.Log("Mouse clicked on Answer Tile");
            if(GamePlayManager.instance.level_State == Level_State.Counting)
            {
                counting_Level.OnAnswerTileClicked(Id, this);
            }else if(GamePlayManager.instance.level_State == Level_State.Addition)
            {
                GetComponent<DragObject>().CanMove = true;
            }
            else if (GamePlayManager.instance.level_State == Level_State.Compare)
            {
                GetComponent<DragObject>().CanMove = true;
            }
            else if (GamePlayManager.instance.level_State == Level_State.Substraction)
            {
                GetComponent<DragObject>().CanMove = true;
            }
            else if (GamePlayManager.instance.level_State == Level_State.Multiplication)
            {
                GetComponent<DragObject>().CanMove = true;
            }
            else if (GamePlayManager.instance.level_State == Level_State.Pattern)
            {
                GetComponent<DragObject>().CanMove = true;
            }

        }
        
    }

    private void OnMouseUp()
    {
        if (GamePlayManager.instance.level_State == Level_State.Addition)
        {
            if (targetObj != null)
            {
                GamePlayManager.instance.addition_Level.ValidateAnswer(this);
            }else
            {
                Reset_Tile_Pos();
            }
        }
        if (GamePlayManager.instance.level_State == Level_State.Compare)
        {
            if(targetObj != null)
            {
                GamePlayManager.instance.Compare_Level.Validate_Answer(this);
            }
            else
            {
                Reset_Tile_Pos();
            }

        }
        if (GamePlayManager.instance.level_State == Level_State.Substraction)
        {
            if (targetObj != null)
            {
                GamePlayManager.instance.substraction_Level.ValidateAnswer(this);
            }
            else
            {
                Reset_Tile_Pos();
            }

        }

        if (GamePlayManager.instance.level_State == Level_State.Multiplication)
        {
            if (targetObj != null)
            {
                GamePlayManager.instance.multiplication_Level.ValidateAnswer(this);
            }
            else
            {
                Reset_Tile_Pos();
            }

        }


        if (GamePlayManager.instance.level_State == Level_State.Pattern)
        {
            if (targetObj != null)
            {
                GamePlayManager.instance.pattern_Level.ValidateAnswer(this);
            }
            else
            {
                Reset_Tile_Pos();
            }

        }
    }

    public void SetValue(int value)
    {
       
        Set_Id(value);
        Value_Text.text = Id.ToString();
        IsClicked = false;
        if(sp!= null)
        {

        }
        
        else
        {
            sp = GetComponent<SpriteRenderer>();
        }

        sp.color = Color.white;
    }

    public void SetValue(string Val)
    {
        Compare_Id = Val;
        Value_Text.text = Val.ToString();
        IsClicked = false;
        if (sp != null)
        {

        }

        else
        {
            sp = GetComponent<SpriteRenderer>();
        }

        sp.color = Color.white;
    }

    public void Set_Tile_For_Correct_Answer()
    {
        sp.color = Color.green;
        IsClicked = true;
    }
    public void Set_Tile_For_Wrong_Answer()
    {
        sp.color = Color.red;
        IsClicked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Target_Answer_Tile>() != null)
        {
            targetObj = collision.GetComponent<Target_Answer_Tile>();
            CurrentId = (collision.GetComponent<Target_Answer_Tile>().Id);
           // Compare_Id = (collision.GetComponent<Target_Answer_Tile>().CompareId);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //CurrentId = -100;
        targetObj = null;
    }

   
}
