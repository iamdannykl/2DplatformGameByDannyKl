using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class qizi : MonoBehaviour
{
    private Animator anim;
    private bool isPengEd;
    private AudioSource tan;
    void Start()
    {
        anim=GetComponent<Animator>();
        tan=GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"&&!isPengEd){
            isPengEd=true;
            tan.Play();
            anim.Play("peng");
        }
    }
    // Update is called once per frame
    public void pengToPiao(){
        anim.Play("piao");
    }
}
