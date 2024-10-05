using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//using Input = UnityEngine.Windows.Input;
public enum stateMs
{
    left,
    stay,
    right,
    dash,
    dqt
};
public class move : MonoBehaviour
{
    [Header("组件")]
    public _2DPlatformGame inputSys;
    public AudioSource slashAs;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PhysicsCheck physicsCheck;
    public GameObject biaoji;
    public GameObject tip;
    private Animator anim;
    public GameObject leftIt, rightIt;
    private leftItc lftc, rtc;
    private playerAim playAim;
    public GameObject shineIng;
    [Header("监听")]
    public Vector2 movement;

    [Header("参数")]
    public float slashTimeWait;
    private float currentTime;
    public float speed;
    public float jumpForce;
    public int JUMPTIMES = 2;
    public int jumpLeft;
    private bool anxia, guanbi;
    public ParticleSystem ps;
    private stateMs stateMs;
    //public Text tx;
    private int scole;
    public GameObject qiZi;
    private bool isJp;
    public float dashForce;
    public float dashWaitTime;
    public int dashTimes = 1;
    private bool isDashing;
    public float crtDsTm;
    private float upOrDown;
    public float slsTime;
    [Header("超冲")]
    public float SuperDashPower;
    private bool isSuperDashing;
    private bool isEnterSuperDash;

    [Header("滑墙")]
    public bool isSliding;
    public float slidingSpd;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("蹬墙跳")]
    private bool isWallJping;
    private float wallJpDrction;
    public float wallJpTime;
    private float wallJpCounter;
    public float wallJpDuration;
    public Vector2 wallJpPower;
    [Header("上下左右劈")]
    public GameObject upSls, downSls, leftSls, rightSls;

    private void Awake()
    {
        jumpLeft = JUMPTIMES;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputSys = new _2DPlatformGame();
        anim = GetComponent<Animator>();
        lftc = leftIt.GetComponent<leftItc>();
        rtc = rightIt.GetComponent<leftItc>();
        playAim = GetComponent<playerAim>();
        inputSys.Player.Jump.started += JumpCtrl;
        inputSys.Player.Jump.canceled += stpJp;
        inputSys.Player.Slash.started += SlsIt;
        inputSys.Player.Dash.started += dashIt;
        //inputSys.Player.SuperDash.started += SuperDashEnter;
        //inputSys.Player.SuperDash.canceled += SuperDash;
        //pyw = physicsCheck.pianYiWall.x;
    }

    /*private void SuperDashEnter(InputAction.CallbackContext obj)
    {
        if (isEnterSuperDash)//结束超冲
        {
            isEnterSuperDash = false;
            isSuperDashing = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isDashing==false && (physicsCheck.isOnGround||(lftc.isWall||rtc.isWall)))
        {
            isEnterSuperDash = true;
            shineIng.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if(isDashing||(!physicsCheck.isOnGround&&!(lftc.isWall||rtc.isWall)))
        {
            isEnterSuperDash = false;
        }
    }
    private void SuperDash(InputAction.CallbackContext obj)
    {Debug.Log(isDashing==false && (physicsCheck.isOnGround||(lftc.isWall||rtc.isWall)));
        if (isEnterSuperDash)
        {Debug.Log("sssssssssssssss");
            shineIng.SetActive(false);
            isSuperDashing = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (!sr.flipX)
            {
                rb.velocity = new Vector2(SuperDashPower,0f);
            }
            else
            {
                rb.velocity = new Vector2(-SuperDashPower,0f);
            }
        }
    }*/
    //冲刺
    private void dashIt(InputAction.CallbackContext obj)
    {
        if (crtDsTm < 0 && dashTimes == 1 && !isSuperDashing)
        {
            dashTimes--;
            crtDsTm = dashWaitTime;
            isDashing = true;
            Invoke(nameof(stopDash), 0.25f);
            if (rtc.isWall)
            {
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
            }
            else if (lftc.isWall)
            {
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
            }
            else if (sr.flipX)
            {
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
            }
            else if (!sr.flipX)
            {
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
            }
        }
    }

    private void stopDash()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDashing = false;
    }
    private void Start()
    {
        //rb.velocity = new Vector2( wallJpPower.x, wallJpPower.y);
        rb.AddForce(new Vector2(wallJpPower.x, wallJpPower.y), ForceMode2D.Impulse);
    }

    void stpJp(InputAction.CallbackContext obj)
    {
        stopWjp();
        isJp = false;
        if (rb.velocity.y >= 0)
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }
    private void JumpCtrl(InputAction.CallbackContext obj)
    {
        if (wallJpCounter > 0)
        {
            rb.AddForce(new Vector2(wallJpDrction * wallJpPower.x, 0), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, wallJpPower.y);
            isWallJping = true;
            wallJpCounter = 0f;
            if (boolToFlt(sr.flipX) != wallJpDrction)
            {
                sr.flipX = !sr.flipX;
            }
            Invoke("stopWjp", wallJpDuration);
        }
        else
        {
            isJp = true;
            jumpLeft--;
            if (jumpLeft > 0)
            {
                if (rb.velocity.y > jumpForce)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce + rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                pss();
            }
        }
    }

    private void OnEnable()
    {
        inputSys.Enable();
    }
    private void OnDisable()
    {
        inputSys.Disable();
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stateMs);
        moveIt();
        slideWall();
        wallJump();

        upOrDown = inputSys.Player.Move.ReadValue<Vector2>().y;
        currentTime -= Time.deltaTime;
        crtDsTm -= Time.deltaTime;

        if (physicsCheck.isWall)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
        //tx.text = scole.ToString();
    }

    public stateMs StateMs
    {
        get => stateMs;
        set
        {
            if (stateMs == value) return;

            if (value == stateMs.dqt)
            {
                if (movement.x != 0)
                {
                    rb.AddForce(new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y), ForceMode2D.Impulse);
                }

                //rb.velocity=new Vector2(movement.x*speed*Time.deltaTime,rb.velocity.y);
            }
            if (value != stateMs.stay)
            {
                pss();
            }

            if (value == stateMs.left)
            {

                //physicsCheck.pianYiWall = new Vector2(-pyw, physicsCheck.pianYiWall.y);
            }

            if (value == stateMs.right)
            {

                //physicsCheck.pianYiWall = new Vector2(pyw, physicsCheck.pianYiWall.y);
            }
            stateMs = value;
        }
    }

    void flipIt()
    {
        if (movement.x > 0)
        {//----------------转身
            sr.flipX = false;
            StateMs = stateMs.right;
            ps.transform.localScale = new Vector3(1, 1, 1);
            //transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            sr.flipX = true;
            StateMs = stateMs.left;
            ps.transform.localScale = new Vector3(-1, 1, 1);
            //transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            StateMs = stateMs.stay;
        }
    }
    void moveIt()
    {
        if (rb.velocity.y < -0.01f)
        {
            rb.gravityScale = 4.5f * 1.15f;
        }
        if (physicsCheck.isOnGround || physicsCheck.isOnTrap)
        {
            dashTimes = 1;
            jumpLeft = JUMPTIMES;
            isWallJping = false;
        }
        movement = inputSys.Player.Move.ReadValue<Vector2>();//获取（上下）左右移动的二维向量
        if (!isWallJping)
            flipIt();
    }
    void pss()
    {
        ps.Play();
    }
    private void FixedUpdate() //----------物理更新
    {
        if (!isWallJping && !isDashing && !isSuperDashing && !isEnterSuperDash)
        {//Debug.Log("huifu");

            rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, rb.velocity.y);
        }
        else if (isWallJping)
        {
            if (movement.x != 0)
            {
                if (movement.x * wallJpDrction < 0)
                    rb.AddForce(new Vector2(movement.x * wallJpPower.x / 7f, 0), ForceMode2D.Impulse);
                //rb.velocity=new Vector2(movement.x*speed*Time.deltaTime,rb.velocity.y);
            }
            //StateMs = stateMs.dqt;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "clct")
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playAim.addHp();
            playAim.hpJian();
            //scole++;
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Animator>().Play("clct");
        }


        if (other.tag == "rebirth")
        {
            anim.SetBool("isHurt", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Transform tsf = other.transform.GetChild(0).transform;
            StartCoroutine(jieDong(tsf));
        }

        if (other.tag == "tongGuan")
        {
            qiZi.SetActive(true);
            Invoke("xiaoshi", 1.2f);
        }
    }

    void xiaoshi()
    {
        qiZi.SetActive(false);
    }
    IEnumerator jieDong(Transform tr)
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = tr.position;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        StopCoroutine(jieDong(tr));
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "hudong")
        {
            if (physicsCheck.isOnGround)
            {
                biaoji.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    if (!other.GetComponent<Fire>().asi.IsName("Hit"))
                        other.gameObject.GetComponent<Animator>().Play("Hit");
                }
                biaoji.transform.position = new Vector2(other.transform.position.x, biaoji.transform.position.y);
            }
            else
            {
                biaoji.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "hudong")
        {
            biaoji.gameObject.GetComponent<Animator>().SetBool("anxia", false);
            biaoji.gameObject.SetActive(false);
        }
    }

    private void slideWall()
    {
        if ((lftc.isWall || rtc.isWall) && !physicsCheck.isOnGround && movement.x != 0f)
        {
            crtDsTm = -1;
            dashTimes = 1;
            jumpLeft = JUMPTIMES;
            isSliding = true;

            wallJpCounter = wallJpTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpd, float.MaxValue));
        }
        else if ((lftc.isWall || rtc.isWall) && !physicsCheck.isOnGround)
        {
            crtDsTm = -1;
            dashTimes = 1;
        }

        else if ((!lftc.isWall && !rtc.isWall) || physicsCheck.isOnGround)
        {
            isSliding = false;
        }
    }

    private float boolToFlt(bool boolFRm)
    {
        if (boolFRm == false)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }
    private void wallJump()
    {
        if (isSliding)
        {
            if (lftc.isWall)
            {
                wallJpDrction = 1;
            }
            else if (rtc.isWall)
            {
                wallJpDrction = -1;
            }
        }
        else
        {
            wallJpCounter -= Time.deltaTime;
        }
    }
    private void SlsIt(InputAction.CallbackContext obj)//劈砍事件
    {
        if (currentTime < 0)
        {
            currentTime = slashTimeWait;
            StartCoroutine(slashIt());
        }
    }
    IEnumerator slashIt()
    {
        slashAs.Play();
        if (upOrDown >= 0.1f)//上劈
        {
            upSls.SetActive(true);
            yield return new WaitForSeconds(slsTime);
            upSls.GetComponent<BoxCollider2D>().enabled = true;
            upSls.SetActive(false);
            StopCoroutine(slashIt());
        }
        else if (upOrDown <= -0.1f && !physicsCheck.isOnGround)//下劈
        {
            downSls.SetActive(true);
            yield return new WaitForSeconds(slsTime);
            downSls.GetComponent<BoxCollider2D>().enabled = true;
            downSls.SetActive(false);
            StopCoroutine(slashIt());
        }
        else if (sr.flipX && Mathf.Abs(upOrDown) < 0.1f)
        {//左劈
            leftSls.SetActive(true);
            yield return new WaitForSeconds(slsTime);
            leftSls.GetComponent<BoxCollider2D>().enabled = true;
            leftSls.SetActive(false);
            StopCoroutine(slashIt());
            //StopCoroutine(slashIt());
        }
        else if (!sr.flipX && Mathf.Abs(upOrDown) < 0.1f)
        {//右劈
            rightSls.SetActive(true);
            yield return new WaitForSeconds(slsTime);
            rightSls.GetComponent<BoxCollider2D>().enabled = true;
            rightSls.SetActive(false);
            StopCoroutine(slashIt());
            //StopCoroutine(slashIt());
        }
    }
    private void stopWjp()//停止蹬墙跳
    {
        isWallJping = false;
        StateMs = stateMs.stay;
    }
}

