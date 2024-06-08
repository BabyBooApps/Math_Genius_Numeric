using UnityEngine;
using UnityEngine.UI;

public class UIBubbleAnimation : MonoBehaviour
{
    public float minScale = 0.8f;  // Minimum scale value for both X and Y.
    public float maxScale = 1.2f;  // Maximum scale value for both X and Y.
    public float speed = 1.0f;    // Scaling speed.
    private RectTransform rectTransform;
    private bool expandX = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Calculate the new scale for both X and Y based on the expandX flag.
        float scaleX = expandX ? Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * speed, 1.0f)) : Mathf.Lerp(maxScale, minScale, Mathf.PingPong(Time.time * speed, 1.0f));
        float scaleY = expandX ? Mathf.Lerp(maxScale, minScale, Mathf.PingPong(Time.time * speed, 1.0f)) : Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * speed, 1.0f));

        // Update the scale of the UI element.
        rectTransform.localScale = new Vector3(scaleX, scaleY, 1.0f);

        // Toggle the expandX flag when the animation completes a cycle.
        if (Mathf.Approximately(scaleX, maxScale) && Mathf.Approximately(scaleY, minScale))
        {
            expandX = !expandX;
        }
    }
}