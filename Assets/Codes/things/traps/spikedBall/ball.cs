using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    leftYD,
    rightYD
};
public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Vector2 ForcePush;
    private State state;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public State State
    {
        get => state;
        set
        {
            if(state==value)return;
            if (value == State.leftYD)
            {
                if (rb.velocity.x > 0.1)
                {
                    rb.AddForce(new Vector2(ForcePush.x,ForcePush.y),ForceMode2D.Impulse);
                }

                if (rb.velocity.x < -0.1)
                {
                    rb.AddForce(new Vector2(-ForcePush.x,ForcePush.y),ForceMode2D.Impulse);
                }
            }
            state = value;
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > 0.1f)
        {
            State = State.leftYD;
        }
        else
        {
            State = State.rightYD;
        }
    }
}
