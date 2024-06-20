using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    public GameObject Question_Container;
    public GameObject Answer_Container;
    public Vector3 Question_COntainer_InitialPos;
    public GameObject Grid1;
    public GameObject Grid2;
    public GameObject plus_Symbol;

    private void Start()
    {
        Question_COntainer_InitialPos = Question_Container.transform.position;
    }

    public void AnimateSuccess()
    {
        if(Grid1 != null)
        {
            Grid1.gameObject.SetActive(false);
        }
        if (Grid2 != null)
            Grid2.gameObject.SetActive(false);
        if (Answer_Container != null)
            Answer_Container.SetActive(false);
        if (plus_Symbol != null)
            plus_Symbol.SetActive(false);
        if (Question_Container != null)
            iTween.MoveTo(Question_Container.gameObject, Vector3.zero, 1.0f);
    }

    public void Reset_LevelContainer()
    {
        if (Question_Container != null)
            iTween.MoveTo(Question_Container.gameObject, Question_COntainer_InitialPos, 1.0f);
        if (Grid1 != null)
            Grid1.gameObject.SetActive(true);
        if (Grid2 != null)
            Grid2.gameObject.SetActive(true);
        if (Answer_Container != null)
            Answer_Container.SetActive(true);
        if (plus_Symbol != null)
            plus_Symbol.SetActive(true);
       
    }
}
