using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Success_Animation : MonoBehaviour
{
    public SpriteRenderer Item_Image;
    public TextMeshPro Count;
    public TextMeshPro ItemName;
    public GameObject Main_Obj;

    public void Set_Item(Sprite sp, int count , string name)
    {
        Item_Image.sprite = sp;
        Count.text = count.ToString();
        ItemName.text = name;

        Main_Obj.transform.localScale = new Vector3(1, 0, 0);

    }

    public void EnableAnim()
    {
      
        iTween.ScaleTo(Main_Obj, Vector3.one,0.5f);
    }

    public IEnumerator Animate_Success(Sprite sp, int count, string name)
    {
        Set_Item(sp, count, name);
        yield return new WaitForSeconds(0.5f);
        EnableAnim();
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(Count.gameObject, new Vector3(1.2f, 1.2f, 0), 1.0f);
        yield return new WaitForSeconds(0.5f);
        iTween.PunchScale(ItemName.gameObject, new Vector3(1.2f, 1.2f, 0), 1.0f);
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);


    }
}
