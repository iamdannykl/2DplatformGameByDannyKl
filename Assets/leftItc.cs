using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftItc : MonoBehaviour
{
    public bool isWall;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ground")
        {
            isWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ground")
        {
            isWall = false;
        }
    }
}
