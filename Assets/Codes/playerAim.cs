using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAim : MonoBehaviour
{
    private Animator anim;
    private PhysicsCheck phyche;
    private move mv;
    private bool isBegin;
    public AudioSource hurt;
    public int hp;
    public int currentHp;
    public int TopHeart;
    public List<GameObject> hearts;
    Rigidbody2D rb;
    public GameObject dead;
    
    private void Awake()
    {
        mv = GetComponent<move>();//获取组件
        anim=GetComponent<Animator>();//动画Animator组件
        rb=GetComponent<Rigidbody2D>();//2d刚体组件
        phyche=GetComponent<PhysicsCheck>();//地面检测组件，这个是我们自己用C#写的
        currentHp = hp;
        TopHeart = 4;
    }
    private void Update() {
        SetAnim();//调用SetAnim方法
    }
    public void SetAnim(){
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));//把角色的X速度的绝对值赋值给velocityX
        anim.SetBool("isOnGrd",phyche.isOnGround);//是否在地面赋值给isOnGrd
        anim.SetBool("isbg",isBegin);//是否播放完成入场动画
        anim.SetInteger("jumpLeftNum",GetComponent<move>().jumpLeft);
        anim.SetBool("isWalling",phyche.isWall);
        anim.SetBool("isSld",mv.isSliding);
        anim.SetInteger("hpLft",currentHp);
    }
    public void appearToIdle(){//动画结束后从入场动画切到idle站立
        isBegin=true;//isBegin为true时便不再播放入场动画
        anim.Play("idleFg");//切到idle站立动画
    }

    private void hpLess()
    {
        if (currentHp > 0)
        { 
            currentHp--;
            TopHeart--;
        }
    }

    public void addHp()
    {
        if (currentHp < 5)
        {
            currentHp++;
            TopHeart++;
        }
    }
    public void hpJian()
    {
        if (currentHp > 0&&currentHp<=hp)
        {
            for(int i=TopHeart+1;i<hp;i++)
            {
                hearts[i].SetActive(false);
            }

            for (int j = 0; j <= TopHeart; j++)
            {
                hearts[j].SetActive(true);
            }
        }
        else if (currentHp <= 0)
        {
            dead.SetActive(true);
            //anim.SetBool("isDead",true);
            anim.Play("death");
        }
    }

    public void gameOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    void siLe()//死了
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="jian")
        {
            hpLess();
            hpJian();
            hurt.Play();
            anim.SetBool("isHurt",true);
            rb.velocity = new Vector2(rb.velocity.x,15);
            //rb.AddForce(new Vector2(0,75),ForceMode2D.Impulse);
        }
        if (other.tag == "enemy")
        {
            hpLess();
            hpJian();
            hurt.Play();
            anim.SetBool("isHurt",true);
            rb.velocity = new Vector2(rb.velocity.x,15);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "jian")
        {
            hpLess();
            hpJian();
            hurt.Play();
            anim.SetBool("isHurt",true);
            rb.velocity = new Vector2(rb.velocity.x,15);
            //rb.AddForce(new Vector2(0,75),ForceMode2D.Impulse);
        }
        if (other.gameObject.tag == "pingtai")
        {
            transform.SetParent(other.gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "pingtai")
        {
            transform.SetParent(null);
        }
    }
    public void backFromHurt()
    {
        anim.SetBool("isHurt",false);
    }
}
