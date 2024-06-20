using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_Grid : MonoBehaviour
{
        public int maxColumns = 3; // Maximum number of columns
    public int maxRows = 3;



    //public int maxColumns = 3; // Maximum number of columns
    float spacing_X = 0.5f; // Spacing between objects
    float spacing_Y = 0.5f;

    float min_Spacing = 0.1f;
    float max_spacing = 2.0f;

    public int Random_Min;
    public int Random_Max;

    public int minRowCount;
    public int maxRowCount;

    public int minColCount;
    public int maxColCount;

    public float minScale;
    public float maxScale;

    public float baseSpacing_X;
    public float baseSpacing_Y;

    public float spacingCoefficient; // Adjust as needed


    public List<Vector3> CalculateGridPositions(int numObjects)
    {
        List<Vector3> objectPositions = new List<Vector3>();
        int numRows = Mathf.CeilToInt((float)numObjects / maxColumns); // Calculate the number of rows


        // Calculate the starting position
        float startX = -(maxColumns - 1) * spacing_X / 2f;
        float startY = (numRows - 1) * spacing_Y / 2f;

        for (int row = 0; row < numRows; row++)
        {
            int rowObjectCount = Mathf.Min(maxColumns, numObjects - row * maxColumns);

            for (int col = 0; col < rowObjectCount; col++)
            {
                Vector3 objectPosition = new Vector3(startX + col * spacing_X, startY - row * spacing_Y, 0f);
                objectPositions.Add(objectPosition);
            }
        }

        return objectPositions;
    }

    public List<Vector3> CalculateGridPositions_V2(int numObjects)
    {
        List<Vector3> objectPositions = new List<Vector3>();

        int numRows = Mathf.Min(maxRows, Mathf.CeilToInt((float)numObjects / maxColumns));
        int numColumns = Mathf.Min(maxColumns, Mathf.CeilToInt((float)numObjects / numRows));

        float startX = -(numColumns - 1) * spacing_X / 2f;
        float startY = (numRows - 1) * spacing_X / 2f;

        float objectSize = 1f / Mathf.Sqrt(numObjects); // Calculate the object size inversely based on the number of objects

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                int objectIndex = row * numColumns + col;
                if (objectIndex < numObjects)
                {
                    float x = startX + col * spacing_X;
                    float y = startY - row * spacing_X ;
                    Vector3 objectPosition = new Vector3(x, y, 0f);

                    // Scale the object size
                    Vector3 objectScale = new Vector3(objectSize, objectSize, objectSize);

                    objectPositions.Add(objectPosition);
                }
            }
        }

        List<Vector3> worldPositions = new List<Vector3>();
        foreach (Vector3 localPosition in objectPositions)
        {
            Vector3 worldPosition = this.transform.TransformPoint(localPosition);
            worldPositions.Add(worldPosition);
        }

        return worldPositions;
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

        // Increase spacing_X and spacing_Y to increase the spacing between grid objects
        float spacing_X = 1.2f;
        float spacing_Y = 1.2f;

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


    public List<Vector3> GenerateGrid(int rows, int columns)
    {
        spacing_X = Mathf.Lerp(min_Spacing, max_spacing, Mathf.Clamp01(2.0f / columns));
        spacing_Y = Mathf.Lerp(min_Spacing, max_spacing, Mathf.Clamp01(2.0f / rows));


        List<Vector3> objectPositions = new List<Vector3>();
        // Calculate the half-width and half-height of the grid.
        float halfWidth = (columns - 1) * spacing_X / 2;
        float halfHeight = (rows - 1) * spacing_Y / 2;

        // Get the position of the GridGenerator game object.
        Vector3 gridGeneratorPosition = transform.position;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the local position based on row, column, spacing, and the center.
                Vector3 localPosition = new Vector3(col * spacing_X - halfWidth, row * spacing_Y - halfHeight, 0);

                // Calculate the world position by adding the grid generator position to the local position.
                Vector3 worldPosition = this.transform.TransformPoint(localPosition);

                objectPositions.Add(worldPosition);

                // Instantiate the gridObjectPrefab at the calculated world position.
               // Instantiate(gridObjectPrefab, worldPosition, Quaternion.identity);
            }
        }
        return objectPositions;
    }

    List<Vector3> CalculateGridPositions_V4(int numObjects, Vector3 targetPosition)
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
               // localPosition += targetPosition;

                // Calculate the world position by adding the grid generator position to the local position.
               // Vector3 worldPosition = this.transform.TransformPoint(localPosition);

                objectPositions.Add(localPosition);

                if (objectPositions.Count >= numObjects)
                {
                    // Stop adding positions once the desired number is reached
                    return objectPositions;
                }
            }
        }

        return objectPositions;
    }



    public List<Vector3> popluateElements_Grid(int numObjects, GameObject targetPosition , MathObj obj)
    {
        List<Vector3> objectPositions = CalculateGridPositions_V4(numObjects, targetPosition.transform.localPosition);

        /* for (int i = 0; i < objectPositions.Count; i++)
         {
             float objectSize = (maxScale / Mathf.Sqrt(numObjects));

             // Instantiate the grid item
             MathObj gridItem = Instantiate(obj, Vector3.zero, Quaternion.identity) as MathObj;
             gridItem.transform.parent = targetPosition.transform;
             gridItem.transform.localPosition = objectPositions[i];
             gridItem.transform.localScale = new Vector3(objectSize, objectSize, 1);
             gridItem.Anim.AnimateObject(gridItem.transform.localScale);
             gridItem.ActivateCanClick();
         }*/

        return objectPositions;
    }


    public List<Vector3> GenerateGridAroundTarget(int rows, int columns, Vector3 targetPosition)
    {
        spacing_X = Mathf.Lerp(min_Spacing, max_spacing, Mathf.Clamp01(3.5f / columns));
        spacing_Y = Mathf.Lerp(min_Spacing, max_spacing, Mathf.Clamp01(5.0f / rows));

        List<Vector3> objectPositions = new List<Vector3>();
        // Calculate the half-width and half-height of the grid.
        float halfWidth = (columns - 1) * spacing_X / 2;
        float halfHeight = (rows - 1) * spacing_Y / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the local position based on row, column, spacing, and the center.
                /*Vector3 localPosition = new Vector3(col * spacing_X - halfWidth + targetPosition.x,
                                                    row * spacing_Y - halfHeight + targetPosition.y,
                                                    targetPosition.z);
*/
                 Vector3 localPosition = new Vector3(row * spacing_Y - halfHeight, col * spacing_X - halfWidth, 0);

                // Calculate the world position by adding the target position to the local position.
                //Vector3 worldPosition = this.transform.TransformPoint(localPosition);

                objectPositions.Add(localPosition);

                // Instantiate the gridObjectPrefab at the calculated world position.
                // Instantiate(gridObjectPrefab, worldPosition, Quaternion.identity);
            }
        }
        return objectPositions;
    }
}
