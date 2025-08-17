using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCont2D : MonoBehaviour
{
    private const float MOVE_SPEED = 60f;

    public static CharCont2D Instance { get; private set; }

    //private Character_Base characterBase;
    private Rigidbody2D rigidbody2D;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;

    private void Awake()
    {
        Instance = this;
       // characterBase = GetComponent<Character_base>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;
        if (moveX != 0 || moveY != 0)
        {
            //Not Idle
            lastMoveDir = moveDir;
        }
       // characterBase.PlayMoveAnim(moveDir);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * MOVE_SPEED;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
