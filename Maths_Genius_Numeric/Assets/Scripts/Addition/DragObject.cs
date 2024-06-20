using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private RaycastHit hit;

    public bool CanMove = true;

    private void Update()
    {
        if (isDragging && CanMove)
        {

            // Calculate the new position based on mouse position and the offset
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        }
    }

    private void OnMouseDown()
    {
        // Perform a raycast to check if the mouse is over the object
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // Calculate the offset between the mouse click point and the object's position
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;


    }
}
