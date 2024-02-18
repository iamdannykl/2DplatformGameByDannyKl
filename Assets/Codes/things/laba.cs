using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class laba : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData evenData)
    {
        transform.localScale=new Vector2(1.2f,1.2f);
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        transform.localScale=new Vector2(1f,1f);
    }

    public void originIt()
    {
        transform.localScale=new Vector2(1f,1f);
    }
}
