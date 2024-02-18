using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire : MonoBehaviour
{
    private Animator anim;
    public AnimatorStateInfo asi;
    public float lightingSpd,waitingTime,zongWaitingTime,tgtLit;
    public GameObject lightSon;
    private bool isOnFire;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void light()
    {
        anim.SetBool("isFns",true);
    }

    IEnumerator lighting(float lightSpd,float waitTime,float zongWaitTime)
    {
        lightSon.SetActive(true);
        float ogLit = 0;
        float targetLit = (zongWaitTime/waitTime)*lightSpd;
        while (ogLit < targetLit)
        {
            ogLit += lightSpd;
            lightSon.GetComponent<Light2D>().intensity = ogLit;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public bool IsOnFire
    {
        get => isOnFire;
        set
        {
            if(isOnFire==value)return;
            if (value)
            {
                StartCoroutine(lighting(lightingSpd,waitingTime,zongWaitingTime));
                anim.SetBool("isFns",false);
                anim.SetBool("on",true);
                anim.SetBool("id",false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            isOnFire = value;
        } 
    }
    private void Update()
    {
        asi = anim.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("idle"))
        {
            lightSon.GetComponent<Light2D>().intensity = 0;
            anim.SetBool("isFns",false);
            anim.SetBool("id",true);
            anim.SetBool("on",false);
        }
        IsOnFire = asi.IsName("on");
        tgtLit = (zongWaitingTime / waitingTime) * lightingSpd;
    }
}
