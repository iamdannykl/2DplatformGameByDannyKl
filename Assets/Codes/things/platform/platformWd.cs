using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformWd : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform left,right;
    private Vector2 lft,rt;
    //private Rigidbody2D rb;
    public int dirc;
    private int lftOrRight = 0;
    private void Awake() {
        lft=left.position;
        rt=right.position;
        //rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>=rt.x)
        {
            lftOrRight=1;
        }
        if(transform.position.x <=lft.x)
        {
            lftOrRight=-1;
        }
        if(lftOrRight==0)
        {
            transform.Translate(Vector2.right*Time.deltaTime);
        }
        else if(lftOrRight==-1)
        {
            transform.Translate(Vector2.right*Time.deltaTime);
        }
        else if(lftOrRight==1)
        {
            transform.Translate(-Vector2.right*Time.deltaTime);
        }
    }
}
