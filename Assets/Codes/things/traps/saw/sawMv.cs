using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class sawMv : MonoBehaviour
{
    public Transform left,right;
    private Vector2 lft,rt;
    public bool isHengXiang;
    public bool isJingZhi;
    public GameObject upB, midB, downB;
    public int num;
    public float speed;
    public Vector3 pianYi;
    public Vector3 zongXiangPianYi;
    public Vector3 changDu;
    private Vector2 shang,xia;
    public bool isGun;
    public Transform shangP, xiaP;
    public float gizR;
    //private Rigidbody2D rb;
    public int dirc;
    private int upDown=0;
    
    private void Awake()
    {
        if (!isJingZhi)
        {
            if (isHengXiang)
            {
                lft=left.position;
                rt=right.position;
            }
            else
            {
                if (isGun)
                {
                    Vector3 hengX = transform.position + pianYi;
                    if (num % 2 == 1)//奇数
                    {
                        int midNum = (num - 1) / 2 - 1;
                        Instantiate(midB, hengX,midB.transform.rotation);
                        for (int i = 0; i < midNum; i++)
                        {
                            Instantiate(midB, hengX + zongXiangPianYi * (i + 1), midB.transform.rotation);
                            Instantiate(midB, hengX - zongXiangPianYi * (i + 1), midB.transform.rotation);
                        }
                        Instantiate(upB, hengX + zongXiangPianYi * (midNum + 1), midB.transform.rotation);
                        Instantiate(downB, hengX - zongXiangPianYi * (midNum + 1), midB.transform.rotation);
                        shang = hengX + zongXiangPianYi * (midNum + 1);
                        xia = hengX - zongXiangPianYi * (midNum + 1); 
                    }
                else
                {
                    int midNum = num / 2 - 1;
                    //Instantiate(midB, hengX,midB.transform.rotation);
                    for (int i = 0; i < midNum; i++)
                    {
                        Instantiate(midB, hengX + zongXiangPianYi * (i + 1)-changDu, midB.transform.rotation);
                        Instantiate(midB, hengX - zongXiangPianYi * (i + 1)+changDu, midB.transform.rotation);
                    }
                    Instantiate(upB, hengX + zongXiangPianYi * (midNum + 1)-changDu, midB.transform.rotation);
                    Instantiate(downB, hengX - zongXiangPianYi * (midNum + 1)+changDu, midB.transform.rotation);
                    shang = hengX + zongXiangPianYi * (midNum + 1) - changDu;
                    xia = hengX - zongXiangPianYi * (midNum + 1) + changDu;
                }
            }
                else
                {
                    shang = shangP.position;
                    xia = xiaP.position; 
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJingZhi)
        {
            if (isHengXiang)
            {
                if(transform.position.x>=rt.x)
                {
                    upDown=1;
                }
                if(transform.position.x <=lft.x)
                {
                    upDown=-1;
                }
                if(upDown==0)
                {
                    transform.Translate(Vector2.right*Time.deltaTime*speed);
                }
                else if(upDown==-1)
                {
                    transform.Translate(Vector2.right*Time.deltaTime*speed);
                }
                else if(upDown==1)
                {
                    transform.Translate(-Vector2.right*Time.deltaTime*speed);
                }
            }
            else
            {
                if(transform.position.y>=shang.y)
                {
                    upDown=1;
                }
                if(transform.position.y <=xia.y)
                {
                    upDown=-1;
                }
                if(upDown==0)
                {
                    transform.Translate(Vector2.down*Time.deltaTime*speed);
                }
                else if(upDown==-1)
                {
                    transform.Translate(-Vector2.down*Time.deltaTime*speed);
                }
                else if(upDown==1)
                {
                    transform.Translate(Vector2.down*Time.deltaTime*speed);
                }
            }
        }
    }

    
}
