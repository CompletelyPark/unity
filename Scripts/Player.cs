using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private float previousX;
    Vector3 dirvec;
    bool isHorizonMove;

    public GameObject scanObject;
    Rigidbody2D rigid;

    public GameObject scanobject;

    float moveX=0f;
    float moveY=0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        previousX = transform.position.x;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        float currentX = transform.position.x;

        Vector3 moveVector = new Vector3(moveX, moveY, 0);

        //transform.Translate(moveVector * Time.deltaTime * 5f);

        bool hdown = Input.GetButtonDown("Horizontal");
        bool vdown = Input.GetButtonDown("Vertical");
        bool hup = Input.GetButtonUp("Horizontal");
        bool vup = Input.GetButtonUp("Vertical");

        if (hdown) isHorizonMove = true;
        else if (vdown) isHorizonMove = false;
        else if (hup || vup) isHorizonMove = moveX != 0;

        if (vdown && moveY == 1)
        {
            dirvec = Vector3.up;
        }
        if (vdown && moveY == -1)
        {
            dirvec = Vector3.down;
        }
        if (hdown && moveX == -1)
        {
            dirvec = Vector3.left;
        }
        if (hdown && moveX == 1)
        {
            dirvec = Vector3.right;
        }

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("RunState", 0.5f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }

        if (currentX > previousX)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (currentX < previousX)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        previousX = currentX;

        if(Input.GetButtonDown("Jump")&& scanObject != null)
        {
            Debug.Log(scanObject.name);
        }
        attack();
    }
    private void FixedUpdate()
    {

        Vector2 moveVec = isHorizonMove ? new Vector2(moveX,0) : new Vector2(0,moveY);
        rigid.velocity = moveVec * 5f;

        Debug.DrawRay(rigid.position, dirvec * 1.5f, Color.red);
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirvec, 1.5f,LayerMask.GetMask("object"));

        if(rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        }
        else { scanObject = null; }
        //Debug.Log(scanObject.name);
    }

    //void Action(GameObject scanObj)
    //{
    //    scanobject = scanObj;
    //    ObjectData objdata = scanobject.GetComponent<ObjectData>();
    //}

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetFloat("NormalState", 0f);
            animator.SetFloat("AttackState", 0f);
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetFloat("NormalState", 0.5f);
            animator.SetFloat("AttackState", 0f);
            animator.SetTrigger("Attack");

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetFloat("NormalState", 1f);
            animator.SetFloat("AttackState", 0f);
            animator.SetTrigger("Attack");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetFloat("SkillState", 0f);
                animator.SetFloat("AttackState", 1f);
                animator.SetTrigger("Attack");
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetFloat("SkillState", 0.5f);
                animator.SetFloat("AttackState", 1f);
                animator.SetTrigger("Attack");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetFloat("SkillState", 1f);
                animator.SetFloat("AttackState", 1f);
                animator.SetTrigger("Attack");
            }
        }

    }
}
