using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downSlash : MonoBehaviour
{
    public GameObject playerPrt;
    private Rigidbody2D rb;
    public float tanTiaoForce;
    public AudioSource dang;
    private move movePlayer;
    void Awake()
    {
        movePlayer = playerPrt.GetComponent<move>();
        rb=playerPrt.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="jian")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            movePlayer.jumpLeft = movePlayer.JUMPTIMES;
            movePlayer.dashTimes = 1;
            movePlayer.crtDsTm = -1;
            dang.Play();
            rb.velocity=new Vector2(rb.velocity.x,tanTiaoForce);
            playerPrt.GetComponent<move>().jumpLeft = playerPrt.GetComponent<move>().JUMPTIMES;
        }
        if(other.tag=="enemy")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            movePlayer.dashTimes = 1;
            movePlayer.crtDsTm = -1;
            movePlayer.jumpLeft = movePlayer.JUMPTIMES;
            other.gameObject.GetComponent<Animator>().Play("hit");
            rb.velocity=new Vector2(rb.velocity.x,tanTiaoForce);
            playerPrt.GetComponent<move>().jumpLeft = playerPrt.GetComponent<move>().JUMPTIMES;
        }
        if(other.tag=="jianTrigger")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            movePlayer.dashTimes = 1;
            movePlayer.crtDsTm = -1;
            movePlayer.jumpLeft = movePlayer.JUMPTIMES;
            rb.velocity=new Vector2(rb.velocity.x,tanTiaoForce);
            playerPrt.GetComponent<move>().jumpLeft = playerPrt.GetComponent<move>().JUMPTIMES; 
        }
    }
}
