using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counting_Scene : MonoBehaviour
{
    public int Random_Min;
    public int Random_Max;
   
    public MathObj obj;
    public Math_Object_Scriptable Current_Scriptable_Object;

    public float itemSpacing;    // Spacing between grid items

    public int minRowCount ;
    public int maxRowCount;

    public int minColCount ;
    public int maxColCount ;

    public float minScale ;
    public float maxScale ;

    public  float baseSpacing_X ;
    public  float baseSpacing_Y ;

    public float spacingCoefficient; // Adjust as needed

    public GameObject GridTarget;
    public List<AnswerTile> AnswerTilesList;
    public List<MathObj> ObjList = new List<MathObj>();

    int Answer_Val;
    bool isLevelCompleted = false;
    int currentClickCount = 0;

    public List<Sprite> ImagesList = new List<Sprite>();
    public List<Math_Object_Scriptable> Math_Objects_Scriptables_List = new List<Math_Object_Scriptable>();
    public Sprite Item_Image;

    public Success_Animation Success_Anim;

  

    // Start is called before the first frame update
    void Start()
    {
       // GetImagesFromGameData();
    }

    public void GetImagesFromGameData()
    {
        ImagesList = GameData.instance.ImagesList;
        Math_Objects_Scriptables_List = GameData.instance.Obj_List;
    }

    public void ClearLevel()
    {
        if(ObjList.Count > 0)
        {
            for(int i = 0; i < ObjList.Count; i++)
            {
                Destroy(ObjList[i].gameObject);
            }
        }
        ObjList.Clear();
    }

    public void InitializeLevel()
    {
        GetImagesFromGameData();

        isLevelCompleted = false;
        GlobalClickCounter.ResetClickCounter();
        

        ClearLevel();

       
        Current_Scriptable_Object = Math_Objects_Scriptables_List.GetRandomElement();
        Item_Image = Current_Scriptable_Object.image;
        Answer_Val = GenerateRandomNumber();
        List<int> Answers = GenerateAnswers(Answer_Val);
        Debug.Log("Answers List count : " + Answers.Count);
        PopulateAnswers(Answers);
        Debug.Log("Random number : " + Answer_Val);
        List<Vector3> PosList = CalculateGridPositions_V3(Answer_Val , GridTarget.transform.position);
       
        for(int i = 0; i < PosList.Count; i++)
        {
            float objectSize = (maxScale / Mathf.Sqrt(Answer_Val));
            // Instantiate the grid item
            MathObj gridItem = Instantiate(obj, Vector3.zero, Quaternion.identity) as MathObj;
            gridItem.GetComponent<SpriteRenderer>().sprite = Item_Image;
            gridItem.transform.localPosition = PosList[i];
            gridItem.transform.localScale = new Vector3(objectSize, objectSize, 1);
            gridItem.Anim.AnimateObject(gridItem.transform.localScale);
            gridItem.ActivateCanClick();
            ObjList.Add(gridItem);
        }
    }

    public int GenerateRandomNumber()
    {
        int val = 0;
        val = Random.Range(Random_Min, Random_Max);
        return val;
           
    }

    public List<int> GenerateAnswers(int correctAnswer)
    {
        // Generate two different random numbers close to the correct answer
        int closeNumber1;
        int closeNumber2;

        do
        {
            int randomOffset1 = Random.Range(1, 6);
            int randomOffset2 = Random.Range(1, 6);

            closeNumber1 = correctAnswer + randomOffset1;
            closeNumber2 = correctAnswer + randomOffset2;
        } while (closeNumber1 == closeNumber2); // Ensure the two random numbers are different

        // Create a list with one correct answer and two close random numbers
        List<int> numberList = new List<int>
        {
            correctAnswer, closeNumber1, closeNumber2
        };

        return numberList;
    }

    public void PopulateAnswers(List<int> ansList)
    {
        AnswerTilesList.Shuffle();
        for(int i = 0; i< AnswerTilesList.Count; i++)
        {
            AnswerTilesList[i].SetValue(ansList[i]);
        }
    }

    public List<Vector3> CalculateGridPositions_V3(int numObjects, Vector3 targetPosition)
    {
        List<Vector3> objectPositions = new List<Vector3>();

        int numRows, numColumns;

        // If numObjects is less than or equal to 3, place them in a single row
        if (numObjects <= 3)
        {
            numColumns = 1;
            numRows = numObjects;
        }
        else
        {
            // Calculate the number of rows and columns to fit the desired number of objects while maintaining symmetry
            numRows = Mathf.CeilToInt(Mathf.Sqrt(numObjects));
            numColumns = Mathf.CeilToInt((float)numObjects / numRows);
        }

        float objectSize = 1f * 0.5f / Mathf.Sqrt(numObjects); // Calculate the object size inversely based on the number of objects
        float minObjectSize = 0.2f; // Set a minimum object size

        // Adjust objectSize based on the number of objects
        objectSize = Mathf.Max(objectSize, minObjectSize);

        // Calculate the spacing based on the number of objects, object size, and spacing coefficient
        float spacing_X = Mathf.Max(baseSpacing_X / (Mathf.Sqrt(numRows) * objectSize) - spacingCoefficient * numObjects, baseSpacing_X);
        float spacing_Y = Mathf.Max(baseSpacing_Y / (Mathf.Sqrt(numColumns) * objectSize) - spacingCoefficient * numObjects, baseSpacing_Y);

        // Calculate the half-width and half-height of the grid based on the new spacing
        float halfWidth = (numColumns - 1) * spacing_X / 2;
        float halfHeight = (numRows - 1) * spacing_Y / 2;

        for (int col = 0; col < numColumns; col++)
        {
            for (int row = 0; row < numRows; row++)
            {
                // Calculate the local position based on row, column, spacing, and the center.
                Vector3 localPosition = new Vector3(row * spacing_Y - halfHeight, col * spacing_X - halfWidth, 0);

                // Offset the local position by the target position
                localPosition += targetPosition;

                // Calculate the world position by adding the grid generator position to the local position.
                Vector3 worldPosition = this.transform.TransformPoint(localPosition);

                objectPositions.Add(worldPosition);

                if (objectPositions.Count >= numObjects)
                {
                    // Stop adding positions once the desired number is reached
                    return objectPositions;
                }
            }
        }

        return objectPositions;
    }

    public List<Vector3> CalculateGridPositions_V3(int numObjects)
    {
        List<Vector3> objectPositions = new List<Vector3>();

        int numRows, numColumns;

        // If numObjects is less than or equal to 3, place them in a single row
        if (numObjects <= 3)
        {
            numRows = 1;
            numColumns = numObjects;
        }
        else
        {
            // Calculate the number of rows and columns to fit the desired number of objects while maintaining symmetry
            numRows = Mathf.CeilToInt(Mathf.Sqrt(numObjects));
            numColumns = Mathf.CeilToInt((float)numObjects / numRows);
        }

        float objectSize = 1f * 0.5f / Mathf.Sqrt(numObjects); // Calculate the object size inversely based on the number of objects
        float minObjectSize = 0.2f; // Set a minimum object size

        // Adjust objectSize based on the number of objects
        objectSize = Mathf.Max(objectSize, minObjectSize);

      
        // Calculate the spacing based on the number of objects, object size, and spacing coefficient
        float spacing_X = Mathf.Max(baseSpacing_X / (Mathf.Sqrt(numRows) * objectSize) - spacingCoefficient * numObjects, baseSpacing_X);
        float spacing_Y = Mathf.Max(baseSpacing_Y / (Mathf.Sqrt(numColumns) * objectSize) - spacingCoefficient * numObjects, baseSpacing_Y);

        // Calculate the half-width and half-height of the grid based on the new spacing
        float halfWidth = (numColumns - 1) * spacing_X / 2;
        float halfHeight = (numRows - 1) * spacing_Y / 2;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                // Calculate the local position based on row, column, spacing, and the center.
                Vector3 localPosition = new Vector3(col * spacing_X - halfWidth, row * spacing_Y - halfHeight, 0);

                // Calculate the world position by adding the grid generator position to the local position.
                Vector3 worldPosition = this.transform.TransformPoint(localPosition);

                objectPositions.Add(worldPosition);

                if (objectPositions.Count >= numObjects)
                {
                    // Stop adding positions once the desired number is reached
                    return objectPositions;
                }
            }
        }

        return objectPositions;
    }

    public void IncrementCurrentClickCount()
    {
        currentClickCount++;
    }

    public int GetCurrentClickCount()
    {
        return currentClickCount;
    }

    
    public void OnAnswerTileClicked(int value , AnswerTile tile)
    {
        if(!isLevelCompleted)
        {
            Debug.Log("Answer tile clicked with value : " + value);
            ValidateAnswer(value, tile);
        }
       
    }

    private void ValidateAnswer(int Ans , AnswerTile tile)
    {
        if(Ans == Answer_Val)
        {
            Debug.Log("Correct Answer");
            AudioManager.instance.Play_Cheer_Clip();
            tile.Set_Tile_For_Correct_Answer();
            StartCoroutine(ResetLevel(Ans));
        }else
        {
            Debug.Log("Wrong Answer");
            AudioManager.instance.PlayFailClip();
            tile.Set_Tile_For_Wrong_Answer();
        }
    }

    IEnumerator ResetLevel(int Ans)
    {
       // yield return new WaitForSeconds(0.2f);
        Success_Anim.gameObject.SetActive(true);
        yield return Success_Anim.Animate_Success(Current_Scriptable_Object.image, Ans, Current_Scriptable_Object.Name);
        //yield return new WaitForSeconds(2.0f);
        InitializeLevel();
    }



}

