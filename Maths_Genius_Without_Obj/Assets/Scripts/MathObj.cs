using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathObj : MonoBehaviour
{
    public TextMeshPro Count_Text;
    bool CanClick = false;
    public BubbleAnimation Anim;
    public int Id;
    // Start is called before the first frame update
    void Start()
    {
        Count_Text.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        Anim = GetComponent<BubbleAnimation>();
    }

    private void OnMouseDown()
    {
        if (CanClick)
        {
            Debug.Log("Mouse Clicked on MathObj");
            Animate_Enabling_Text(GlobalClickCounter.incrementClickCounter());
            CanClick = false;
        }
        else
        {
            Debug.Log("Mouse Already Clicked");
        }
          
    }

    public void ActivateCanClick()
    {
        CanClick = true;
    }

    public void Deactivate_Object()
    {
        CanClick = false;
        Color col = GetComponent<SpriteRenderer>().color;
        col.a = 0.5f;
        GetComponent<SpriteRenderer>().color = col;
    }

    public void Animate_Enabling_Text(int count)
    {
        Count_Text.text = count.ToString();
        Count_Text.gameObject.SetActive(true);
    }

    public void SetId(int id)
    {
        Id = id;
    }


}
