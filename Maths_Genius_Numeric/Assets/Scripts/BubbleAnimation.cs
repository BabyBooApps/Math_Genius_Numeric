using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);  // The target scale to animate around.
    float scaleRange = 0.025f;                                 // The range to animate around the target scale.
    float speed = 1.5f;                                      // Scaling speed.

    private bool expand = true;
    bool canAnimate = false;

    public void AnimateObject(Vector3 scale)
    {
        canAnimate = true;
        targetScale = scale;
    }

    private void Update()
    {
        if(canAnimate)
        {
            // Calculate the new scale based on the expand flag.
            float scaleX = expand ? Mathf.Lerp(targetScale.x - scaleRange, targetScale.x + scaleRange, Mathf.PingPong(Time.time * speed, 1.0f)) : Mathf.Lerp(targetScale.x + scaleRange, targetScale.x - scaleRange, Mathf.PingPong(Time.time * speed, 1.0f));
            float scaleY = expand ? Mathf.Lerp(targetScale.y - scaleRange, targetScale.y + scaleRange, Mathf.PingPong(Time.time * speed, 1.0f)) : Mathf.Lerp(targetScale.y + scaleRange, targetScale.y - scaleRange, Mathf.PingPong(Time.time * speed, 1.0f));
            float scaleZ = targetScale.z;  // Assuming you don't want to animate the Z scale.

            // Update the local scale of the game object.
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

            // Toggle the expand flag when the animation completes a cycle.
            if (Mathf.Approximately(scaleX, targetScale.x + scaleRange) && Mathf.Approximately(scaleY, targetScale.y - scaleRange))
            {
                expand = !expand;
            }
        }
       
    }
}
