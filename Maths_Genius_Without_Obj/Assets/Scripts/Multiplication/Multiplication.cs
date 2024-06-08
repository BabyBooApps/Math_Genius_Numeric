using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multiplication : MonoBehaviour
{
    public List<int> Question_Elements = new List<int>();
    public int Answer;

    public TextMeshPro Question_Element1;
    public TextMeshPro Question_Element2;
    public TextMeshPro Answer_Elemenet;
    public TextMeshPro Minus;
    public TextMeshPro Equal;

    public List<AnswerTile> Choice_Answer_Tiles = new List<AnswerTile>();

    public MathObj Grid_Obj;
    public List<MathObj> Grid_Obj_List = new List<MathObj>();

    public Dynamic_Grid Grid1;
    public Target_Answer_Tile target_Answer;

    public LevelContainer leveContainer;

    public Math_Object_Scriptable current_scriptable;
    public List<Math_Object_Scriptable> Math_Objects_Scriptables_List = new List<Math_Object_Scriptable>();

    public int Table_Val = 1;
    public int Multiplier = 1;

    private void Start()
    {
        
    }

    public void Start_Level(int tableVal)
    {
        //GetImagesFromGameData();
        Multiplier = 1;
        Activate_Question(tableVal);
    }

    public void GetImagesFromGameData()
    {

        Math_Objects_Scriptables_List = GameData.instance.Obj_List;
    }

    public void Get_Random_Scriptable()
    {
        current_scriptable = Math_Objects_Scriptables_List.GetRandomElement();
    }

    public void Generate_Question(int TableVal)
    {
        Table_Val = TableVal;
        Question_Elements.Clear();
        // int num1 = Utilities.GetRandomNumber(4, 9);
        int num1 = TableVal;
        //int num2 = Utilities.GetRandomNumber(1, 9);
        int num2 = Multiplier;
        Question_Elements.Add(num1);
        Question_Elements.Add(num2);

        Answer = num1 * num2;
        target_Answer.Set_Id(Answer);
    }

    public void Set_Question(int TableVal)
    {
        Generate_Question(TableVal);
        Question_Element1.text = Question_Elements[0].ToString();
        Question_Element2.text = Question_Elements[1].ToString();
        Answer_Elemenet.text = Answer.ToString();
    }


    public void Generate_Answers()
    {
        int[] distractors = GenerateDistractors(Answer);

        Choice_Answer_Tiles.Shuffle();

        // Set the text for the choice answer tiles
        Choice_Answer_Tiles[0].SetValue(Answer); // Correct answer
        Choice_Answer_Tiles[1].SetValue(distractors[0]); // Distractor 1
        Choice_Answer_Tiles[2].SetValue(distractors[1]); // Distractor 2
    }


    int[] GenerateDistractors(int answer)
    {
        int[] distractors = new int[2];

        // Generate the first distractor using addition
        distractors[0] = answer + Utilities.GetRandomNumber(1, 5);

        // Generate the second distractor using subtraction
        distractors[1] = answer - Utilities.GetRandomNumber(1, answer - 1);

        // Ensure that distractors are different from each other and not equal to the correct answer
        while (distractors[0] == distractors[1] || distractors[0] == answer || distractors[1] == answer)
        {
            distractors[0] = answer + Utilities.GetRandomNumber(1, 5);
            distractors[1] = answer - Utilities.GetRandomNumber(1, answer - 1);
        }

        return distractors;
    }


    public void Populate_Grid()
    {


        List<Vector3> leftPos = Grid1.GenerateGridAroundTarget(Question_Elements[0] , Question_Elements[1] , Grid1.transform.position);


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
    }

    public void ValidateAnswer(AnswerTile tile)
    {
        if (tile.Id == target_Answer.Id)
        {
            Debug.Log("Correct Answer");
            AudioManager.instance.Play_Cheer_Clip();
            tile.transform.position = target_Answer.transform.position;

            StartCoroutine(AnimateSuccess());

        }
        else
        {
            Debug.Log("wrong answer");
            AudioManager.instance.PlayFailClip();
            iTween.MoveTo(tile.gameObject, tile.initial_Pos, 1.0f);
            // tile.transform.localPosition = tile.initial_Pos;
        }

    }

    public IEnumerator AnimateSuccess()
    {
        UI_Manager.instance.multiply_Screen.Back_Button.gameObject.SetActive(false);

        Clear_Grid_Objects();
        yield return new WaitForSeconds(0.5f);

        leveContainer.AnimateSuccess();
        Answer_Elemenet.gameObject.SetActive(true);
        target_Answer.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        iTween.PunchScale(Question_Element1.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Minus.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Question_Element2.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Equal.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Answer_Elemenet.gameObject, new Vector3(1.5f, 1.5f, 0), 0.5f);
        yield return new WaitForSeconds(1.0f);
        leveContainer.Reset_LevelContainer();
        yield return new WaitForSeconds(1.0f);
        ResetLevel();
        Answer_Elemenet.gameObject.SetActive(false);
        target_Answer.gameObject.SetActive(true);

        UI_Manager.instance.multiply_Screen.Back_Button.gameObject.SetActive(true);

    }

    public void ResetLevel()
    {
        GlobalClickCounter.ResetClickCounter();
        Multiplier++;
        if(Multiplier > 10)
        {
            Multiplier = 1;
        }

        Reset_Answer_Tiles();
        Activate_Question(Table_Val);
    }

    public void Activate_Question(int TableVal)
    {
       // Get_Random_Scriptable();
        Set_Question(TableVal);
        Generate_Answers();
       // Populate_Grid();
    }


    public void Clear_Grid_Objects()
    {
        for (int i = 0; i < Grid_Obj_List.Count; i++)
        {
            Destroy(Grid_Obj_List[i].gameObject);
        }
        Grid_Obj_List.Clear();
    }

    public void Reset_Answer_Tiles()
    {
        for (int i = 0; i < Choice_Answer_Tiles.Count; i++)
        {
            Choice_Answer_Tiles[i].Reset_Tile_Pos();
        }
    }


}
