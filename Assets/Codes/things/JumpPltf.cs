using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPltf : MonoBehaviour
{
    public float jumpFc;
    private Animator anim;
    public AudioSource tan;
    private void Awake()
    {
        anim=GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tan.Play();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpFc),ForceMode2D.Impulse);
            anim.Play("Jump");
        }
    }

    
    public void backToIdle()
    {
        anim.Play("idle");
    }
}
