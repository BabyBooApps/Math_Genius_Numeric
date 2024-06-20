using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Comapre_Level : MonoBehaviour
{
    public Dynamic_Grid Grid1;
    public Dynamic_Grid Grid2;

    public List<int> Question_Elements = new List<int>();
    public string Answer;

    public TextMeshPro Question_Element1;
    public TextMeshPro Question_Element2;
    public TextMeshPro Answer_Elemenet;

    public List<AnswerTile> Choice_Answer_Tiles = new List<AnswerTile>();
    public Target_Answer_Tile target_Answer;

    public GameObject Answer_Tiles_Obj;

    public List<Math_Object_Scriptable> Math_Objects_Scriptables_List = new List<Math_Object_Scriptable>();
    public Math_Object_Scriptable current_scriptable;
    public MathObj Grid_Obj;
    public List<MathObj> Grid_Obj_List = new List<MathObj>();

    private void Start()
    {
       
        //Reset_Level();
    }

    public void Reset_Level()
    {
        Math_Objects_Scriptables_List = GameData.instance.Obj_List;
        //Get_Random_Scriptable();
        Reset_Answers();
        Set_Question();
        Generate_Answers();
       // Populate_Grid();
    }

    public void Get_Random_Scriptable()
    {
        current_scriptable = Math_Objects_Scriptables_List.GetRandomElement();
    }
    public void Reset_Answers()
    {
       
        for (int i = 0; i < Choice_Answer_Tiles.Count; i++)
        {
            Choice_Answer_Tiles[i].Reset_Tile_Pos();
            Choice_Answer_Tiles[i].gameObject.SetActive(true);
        }
        Answer_Tiles_Obj.SetActive(true);
    }
    public void Generate_Question()
    {
        Question_Elements.Clear();
        int num1 = Utilities.GetRandomNumber(1, 9);
        int num2 = Utilities.GetRandomNumber(1, 9);
        Question_Elements.Add(num1);
        Question_Elements.Add(num2);
        Answer = Get_Compare_Symbol(Question_Elements);
        target_Answer.Set_Compare_Id(Answer);
    }

    public void Set_Question()
    {
        Generate_Question();
        Question_Element1.text = Question_Elements[0].ToString();
        Question_Element2.text = Question_Elements[1].ToString();
        Answer_Elemenet.text = Answer;
        Answer_Elemenet.gameObject.SetActive(false);
       // Question_Element1.gameObject.SetActive(false);
       // Question_Element2.gameObject.SetActive(false);
    }

    public string Get_Compare_Symbol(List<int> QuestionList)
    {
       
        if(QuestionList[0] < QuestionList[1])
        {
            return "<";
        }else if (QuestionList[0] > QuestionList[1])
        {
            return ">";
        }else
        {
            return "=";
        }
           
    }


    public void Generate_Answers()
    {
       

        Choice_Answer_Tiles.Shuffle();

        // Set the text for the choice answer tiles
        Choice_Answer_Tiles[0].SetValue("<"); // Correct answer
        Choice_Answer_Tiles[1].SetValue(">"); // Distractor 1
        Choice_Answer_Tiles[2].SetValue("="); // Distractor 2
    }

    public void Validate_Answer(AnswerTile tile)
    {
        if(tile.Compare_Id == Answer)
        {
            Debug.Log("Coerrect Answer");
            AudioManager.instance.Play_Cheer_Clip();
            tile.transform.position = target_Answer.transform.position;
            tile.gameObject.SetActive(false);
           
            StartCoroutine(AnimateSuccess());
            
        }else
        {
            Debug.Log("Wrong Answer");
            AudioManager.instance.PlayFailClip();
            tile.Reset_Tile_Pos();
        }
    }

    public IEnumerator AnimateSuccess()
    {
        UI_Manager.instance.compare_Screen.BackButton.gameObject.SetActive(false);

        Clear_Grid_Objects();
        Answer_Tiles_Obj.SetActive(false);
        Answer_Elemenet.gameObject.SetActive(true);
        Question_Element1.gameObject.SetActive(true);
        Question_Element2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        iTween.PunchScale(Question_Element1.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(target_Answer.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Question_Element2.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(1.0f);
        Reset_Level();

        UI_Manager.instance.compare_Screen.BackButton.gameObject.SetActive(true);
    }

    public void Populate_Grid()
    {


        List<Vector3> leftPos = Grid1.popluateElements_Grid(Question_Elements[0], Grid1.gameObject, Grid_Obj);
        List<Vector3> RightPos = Grid2.popluateElements_Grid(Question_Elements[1], Grid2.gameObject, Grid_Obj);

        for (int i = 0; i < leftPos.Count; i++)
        {
            float objectSize = (Grid1.maxScale / Mathf.Sqrt(leftPos.Count));

            // Instantiate the grid item
            MathObj gridItem = Instantiate(Grid_Obj, Vector3.zero, Quaternion.identity) as MathObj;
            gridItem.GetComponent<SpriteRenderer>().sprite = current_scriptable.image;
            gridItem.transform.parent = Grid1.transform;
            gridItem.transform.localPosition = leftPos[i];
            gridItem.transform.localScale = new Vector3(objectSize, objectSize, 1);
            gridItem.Anim.AnimateObject(gridItem.transform.localScale);
            gridItem.ActivateCanClick();

            Grid_Obj_List.Add(gridItem);
        }

        for (int i = 0; i < RightPos.Count; i++)
        {
            float objectSize = (Grid2.maxScale / Mathf.Sqrt(RightPos.Count));

            // Instantiate the grid item
            MathObj gridItem = Instantiate(Grid_Obj, Vector3.zero, Quaternion.identity) as MathObj;
            gridItem.GetComponent<SpriteRenderer>().sprite = current_scriptable.image;
            gridItem.transform.parent = Grid2.transform;
            gridItem.transform.localPosition = RightPos[i];
            gridItem.transform.localScale = new Vector3(objectSize, objectSize, 1);
            gridItem.Anim.AnimateObject(gridItem.transform.localScale);
            gridItem.ActivateCanClick();

            Grid_Obj_List.Add(gridItem);
        }


    }


    public void Clear_Grid_Objects()
    {
        for (int i = 0; i < Grid_Obj_List.Count; i++)
        {
            Destroy(Grid_Obj_List[i].gameObject);
        }
        Grid_Obj_List.Clear();
    }
}
