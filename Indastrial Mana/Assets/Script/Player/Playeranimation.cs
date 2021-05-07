using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{

    float Movex = 0;//左右移動の為の変数
    float Movey = 0;//前後移動の為の変数

    float Move = 10;//移動量の為の変数

    int direction = 0;//左右の向き判定のための変数,0=左、1=右

    int Movenowx = 0;//いま動いているかの判断　アニメーションの有無に使用 0=立ち絵,1=移動アニメーション
    int Movenowy = 0;//いま動いているかの判断　アニメーションの有無に使用 0=立ち絵,1=移動アニメーション

    Animator animator;

    [SerializeField] Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movex = 0;
        Movey = 0;

        Vector3 scale = transform.localScale;

        //左右移動
        if (Input.GetKey(KeyCode.A))
        {
            Movex = -Move;
            // animator.SetInteger("PlayerState", 1);
            Movenowx = 1;
            if (direction == 1)
            {
                scale.x *= -1;
                direction = 0;
            }
        } else if (Input.GetKey(KeyCode.D))
        {
            Movex = Move;
            // animator.SetInteger("PlayerState", 1);
            Movenowx = 1;
            if (direction == 0)
            {
                scale.x *= -1;
                direction = 1;
            }
        }
        else
        {
            //animator.SetInteger("PlayerState", 2);
            Movenowx = 0;
        }


        //上下移動
        if (Input.GetKey(KeyCode.W))
        {
            Movey = Move;
            Movenowy = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            Movey = -Move;
            Movenowy = 1;
        }
        else
        {
            Movenowy = 0;
        }


        //アニメーション差し替え判定
        if(Movenowx == 1)
        {
            animator.SetInteger("PlayerState", 1);
        }
        else if(Movenowy == 1)
        {
            animator.SetInteger("PlayerState", 1);
        }
        else
        {
            animator.SetInteger("PlayerState", 2);
        }
            //以下更新用
            transform.localScale = scale;
            transform.Translate(Movex / 1000, Movey / 1000, 0);
        }
    }