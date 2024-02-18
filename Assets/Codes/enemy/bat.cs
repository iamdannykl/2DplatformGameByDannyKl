using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    // Start is called before the first frame update
    public void toFly()
    {
        GetComponent<Animator>().Play("fly");
    }
}
