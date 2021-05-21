using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{
    int seikou = 0;//デバッグ用変数

    float Movex = 0;//左右移動の為の変数
    float Movey = 0;//前後移動の為の変数

    float Move = 10;//移動量の為の変数

    int Direction = 0;//左右の向き判定のための変数,0=左、1=右

    int Movenowx = 0;//いま動いているかの判断　アニメーションの有無に使用 0=立ち絵,1=移動アニメーション
    int Movenowy = 0;//いま動いているかの判断　アニメーションの有無に使用 0=立ち絵,1=移動アニメーション

    // アイテム所持状態
    public enum ToolState : byte
    {
        None,
        Shovel,
        Bucket
    }


   // [HideInInspector] public bool inPlayer;

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

        Vector3 scale = transform.localScale;//座標取得

        //左右移動
        if (Input.GetKey(KeyCode.A))
        {
            Movex = -Move;
            Movenowx = 1;
            if (Direction == 1)
            {
                scale.x *= -1;
                Direction = 0;
            }
        } else if (Input.GetKey(KeyCode.D))
        {
            Movex = Move;           
            Movenowx = 1;
            if (Direction == 0)
            {
                scale.x *= -1;
                Direction = 1;
            }
        }
        else
        {
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
        while (ToolState <2)
        {
            if (Movenowx == 1)
            {
                animator.SetInteger("PlayerState", 1);
            }
            else if (Movenowy == 1)
            {
                animator.SetInteger("PlayerState", 1);
            }
            else
            {
                animator.SetInteger("PlayerState", 2);
            }
            break;
        }

        //以下アクション
        //道具ひろう
       /* if (Input.GetKey(KeyCode.Space))
        {
            if (ToolState == 0)
            {
               if(inPlayer = true)
                {
                    ToolState = 1;
                    Debug.Log("ToolState");
                } 
            }
            else if (ToolState == 1)//肥料の上に居るかの判断,肥料をすくう
            {

            }
            else if (ToolState == 2)//水場にいるかの判断,バケツをもって水を汲む
            {

            }
            else if (ToolState == 3)//畑の上に居るかの判断,肥料を畑にかける
            {
                
            }else if(ToolState == 4)//畑の上にいるかの判断,畑に水をやる
            {

            }
        }*/

        //以下更新用
        transform.localScale = scale;
            transform.Translate(Movex / 1000, Movey / 1000, 0);
        }
    }