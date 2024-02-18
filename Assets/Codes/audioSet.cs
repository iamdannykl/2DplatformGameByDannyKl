using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioSet : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterS, BGMS, effectS;
    [SerializeField]private bool isJingYin=false;
    private int num;
    public GameObject laba;
    

    private void Update()
    {
        if (isJingYin||masterS.value <= -40f)
        {
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer.SetFloat("Master",masterS.value);
        }
    }

    public void SetMaster()
    {
        
    }

    public void SetBGM()
    {
        audioMixer.SetFloat("BGM",BGMS.value);
    }

    public void SetEffect()
    {
        audioMixer.SetFloat("Effects",effectS.value);
    }

    public void JingYin()
    {
        isJingYin = !isJingYin;
        if (!isJingYin) laba.GetComponent<Animator>().Play("kai");
        else
        {
            laba.GetComponent<Animator>().Play("guan");
        }
    }
}
