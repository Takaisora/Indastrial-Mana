using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{

    float Movex = 0;//���E�ړ��ׂ̈̕ϐ�
    float Movey = 0;//�O��ړ��ׂ̈̕ϐ�

    float Move = 10;//�ړ��ʂׂ̈̕ϐ�

    int direction = 0;//���E�̌�������̂��߂̕ϐ�,0=���A1=�E

    int Movenowx = 0;//���ܓ����Ă��邩�̔��f�@�A�j���[�V�����̗L���Ɏg�p 0=�����G,1=�ړ��A�j���[�V����
    int Movenowy = 0;//���ܓ����Ă��邩�̔��f�@�A�j���[�V�����̗L���Ɏg�p 0=�����G,1=�ړ��A�j���[�V����

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

        //���E�ړ�
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


        //�㉺�ړ�
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


        //�A�j���[�V���������ւ�����
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
            //�ȉ��X�V�p
            transform.localScale = scale;
            transform.Translate(Movex / 1000, Movey / 1000, 0);
        }
    }