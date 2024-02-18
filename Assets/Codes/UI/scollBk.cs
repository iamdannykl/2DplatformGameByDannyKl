using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scollBk : MonoBehaviour
{
    public RawImage ri;

    public float xp, yp;
    // Update is called once per frame
    void Update()
    {
        ri.uvRect = new Rect(ri.uvRect.position+new Vector2(xp,yp),ri.uvRect.size);
    }
}
