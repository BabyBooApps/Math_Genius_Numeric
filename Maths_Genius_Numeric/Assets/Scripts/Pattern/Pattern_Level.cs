using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pattern_Level : MonoBehaviour
{

    public List<List<int>> levels = new List<List<int>>();

    public List<Math_Object_Scriptable> Math_Objects_Scriptables_List = new List<Math_Object_Scriptable>();

    public List<int> CurrentPattern = new List<int>();
    public float spacing = 1.0f; // Adjust this value to set the spacing between items

    public MathObj Grid_Obj;
    public MathObj CorrectAnsObj;

    public List<MathObj> Grid_Obj_List = new List<MathObj>();

    public Target_Answer_Tile target_answer;

    public List<AnswerTile> Answer_Tiles = new List<AnswerTile>();

    public List<int> answers = new List<int>();
    public int rand;

    // Start is called before the first frame update
    void Start()
    {
       /* SetLevels();
        GetImagesFromGameData();
       
        SetPattern();
        GenerateQuestion();*/
    }

    public void GetImagesFromGameData()
    {

        Math_Objects_Scriptables_List = GameData.instance.Obj_List;
        Math_Objects_Scriptables_List.Shuffle();
    }

    public void SetLevels()
    {
        levels = new List<List<int>>
        {

            new List<int> { 1,2,3,1,2,3 },
            new List<int> { 1,1,2,2,3,3 },
            new List<int> { 1,2,3,4,1,2,3,4 },
            new List<int> { 1,1,2,2,3,3,4,4 },
           
        };

        levels.Shuffle();
    }

    public void SetPattern()
    {
        CurrentPattern = levels.GetRandomElement();
    }

    public void GenerateQuestion()
    {
        rand = UnityEngine.Random.Range(0, CurrentPattern.Count);

        Vector3[] pos = GeneratePositions(CurrentPattern.Count);

        Answer_Tiles.Shuffle();

        for (int i = 0; i < pos.Length; i++)
        {
            MathObj grid_Obj = Instantiate(Grid_Obj) as MathObj;
            grid_Obj.transform.position = pos[i];
            grid_Obj.GetComponent<SpriteRenderer>().sprite = Math_Objects_Scriptables_List[CurrentPattern[i]].image;
            grid_Obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            grid_Obj.SetId(CurrentPattern[i]);
            Grid_Obj_List.Add(grid_Obj);
        }

        CorrectAnsObj = Grid_Obj_List[rand];
        CorrectAnsObj.GetComponent<SpriteRenderer>().enabled = false;
        target_answer.transform.position = CorrectAnsObj.transform.position;
        target_answer.Set_Id(CurrentPattern[rand]);

        answers.Add(CurrentPattern[rand]);
        //target_answer.Set_Id(CurrentPattern[answers[0]]);
        Debug.Log("Setting Target Id : " + CurrentPattern[rand]);
        Debug.Log("Setting Target Id ANSWER : " + answers[0]);

        Answer_Tiles[0].Set_Ans_Sprite(Math_Objects_Scriptables_List[answers[0]].image);
        Answer_Tiles[0].Set_Id(answers[0]);
        Debug.Log("Setting Answer tile value : " + answers[0]);

        Tuple<int, int> randomNumbers = GetRandomIndices(CurrentPattern, CurrentPattern[rand]);
        answers.Add(CurrentPattern[randomNumbers.Item1]);
        answers.Add(CurrentPattern[randomNumbers.Item2]);

        Debug.Log("Ans 1 : " + answers[1] + "," + answers[2]);

        Answer_Tiles[1].Set_Ans_Sprite(Math_Objects_Scriptables_List[answers[1]].image);
        Answer_Tiles[1].Set_Id(answers[1]);
        //Answer_Tiles[1].gameObject.SetActive(false);

        Answer_Tiles[2].Set_Ans_Sprite(Math_Objects_Scriptables_List[answers[2]].image);
        Answer_Tiles[2].Set_Id(answers[2]);
       // Answer_Tiles[2].gameObject.SetActive(false);

        /* answers.Shuffle();

         for (int i = 0; i < answers.Count; i++)
         {
             Answer_Tiles[i].Set_Ans_Sprite(Math_Objects_Scriptables_List[CurrentPattern[answers[i]]].image);
             Answer_Tiles[i].Set_Id(answers[i]);
         }*/




    }

    public static Tuple<int, int> GetRandomIndices(List<int> numberList, int answer)
    {
        // Ensure the list is not empty
        if (numberList == null || numberList.Count == 0)
        {
            throw new ArgumentException("Number list cannot be empty");
        }

        // Ensure the answer is in the list
        if (!numberList.Contains(answer))
        {
            throw new ArgumentException("Answer must be in the number list");
        }

        // Create a random number generator
        System.Random random = new System.Random();

        // Filter the list to unique indices excluding the index of the answer
        List<int> uniqueIndices = Enumerable.Range(0, numberList.Count).Where(i => numberList[i] != answer).ToList();

        // Check if there are enough unique indices
        if (uniqueIndices.Count < 2)
        {
            throw new InvalidOperationException("Not enough unique indices in the list to generate random indices");
        }

        // Get two distinct random indices from the unique list
        int index1 = GetRandomIndex(random, uniqueIndices);
        int index2 = GetRandomIndex(random, uniqueIndices.Except(new[] { index1 }).ToList());

        return Tuple.Create(index1, index2);
    }

    private static int GetRandomIndex(System.Random random, List<int> indices)
    {
        if (indices.Count == 0)
        {
            throw new InvalidOperationException("Not enough unique indices in the list to generate random indices");
        }

        return indices[random.Next(0, indices.Count)];
    }

    public Vector3[] GeneratePositions(int itemCount)
    {
        Vector3[] positions = new Vector3[itemCount];

        // Calculate the total width of all items
        float totalWidth = (itemCount - 1) * spacing;

        // Calculate the starting position
        Vector3 startPosition = new Vector3(-totalWidth / 2f, 0f, 0f);

        // Generate positions
        for (int i = 0; i < itemCount; i++)
        {
            positions[i] = startPosition + new Vector3(i * spacing, 0f, 0f);
        }

        return positions;
    }

    public void ValidateAnswer(AnswerTile tile)
    {
        if (tile.Id == target_answer.Id)
        {
            Debug.Log("Correct Answer");
            AudioManager.instance.Play_Cheer_Clip();
            tile.transform.position = target_answer.transform.position;

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

    IEnumerator AnimateSuccess()
    {
        yield return new WaitForSeconds(3.0f);
        Reset_Level();
    }

    public void Reset_Answer_Tiles()
    {
        for (int i = 0; i < Answer_Tiles.Count; i++)
        {
            Answer_Tiles[i].Reset_Tile_Pos();
        }

        answers.Clear();
    }

    public void Clear_Grid_Objects()
    {
        for (int i = 0; i < Grid_Obj_List.Count; i++)
        {
            Destroy(Grid_Obj_List[i].gameObject);
        }

        Grid_Obj_List.Clear();
    }

    public void Reset_Level()
    {
        Reset_Answer_Tiles();
        Clear_Grid_Objects();
        SetLevels();
        GetImagesFromGameData();
        SetPattern();
        GenerateQuestion();
    }
}


