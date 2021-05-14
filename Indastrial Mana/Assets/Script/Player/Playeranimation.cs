using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranimation : MonoBehaviour
{

    int seikou = 0;//�f�o�b�O�p�ϐ�

    float Movex = 0;//���E�ړ��ׂ̈̕ϐ�
    float Movey = 0;//�O��ړ��ׂ̈̕ϐ�

    float Move = 10;//�ړ��ʂׂ̈̕ϐ�

    int Direction = 0;//���E�̌�������̂��߂̕ϐ�,0=���A1=�E

    int Movenowx = 0;//���ܓ����Ă��邩�̔��f�@�A�j���[�V�����̗L���Ɏg�p 0=�����G,1=�ړ��A�j���[�V����
    int Movenowy = 0;//���ܓ����Ă��邩�̔��f�@�A�j���[�V�����̗L���Ɏg�p 0=�����G,1=�ړ��A�j���[�V����

    int ToolState = 0;//�����������Ă��邩�̔��f 0=������,1=�o�P�c,2=�V���x��,3=�엿,4=��

    

    Animator animator;

    [SerializeField] private BacketCheck bCheck;
    [SerializeField] private ShovelCheck sCheck;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movex = 0;
        Movey = 0;

        Vector3 scale = transform.localScale;//�v���C���[���̎擾

        //���E�ړ�
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
        //while (ToolState <2)
        //{
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
           // break;
        //}

        //�ȉ��A�N�V����
        //����߂�(�e�X�g�p)
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ToolState == 1)
            {
                bCheck.ActiveItem(true);
                bCheck.BacketMove(transform.position);
                ToolState = 0;
            }else if(ToolState == 2)
            {
                sCheck.ActiveItem(true);
                sCheck.ShovelMove(transform.position);
                ToolState = 0;
            }
        }
        //����Ђ낤
        if (Input.GetKey(KeyCode.Space))
        {
            if (ToolState == 0)
            {
                if (bCheck.InPlayer == true)
                {
                    ToolState = 1;
                    bCheck.ActiveItem(false);
                }
                else if (sCheck.InPlayer2 == true)
                {
                    ToolState = 2;
                    sCheck.ActiveItem(false);
                }
            }
            else if (ToolState == 1)//����ɂ��邩�̔��f,�o�P�c�������Đ�������
            {

            }
            else if (ToolState == 2)//�엿�̏�ɋ��邩�̔��f,�엿��������
            {

            }
            else if (ToolState == 3)//���̏�ɋ��邩�̔��f,�엿�𔨂ɂ�����
            {
                
            }else if(ToolState == 4)//���̏�ɂ��邩�̔��f,���ɐ������
            {

            }
        }

        //�ȉ��X�V�p
        transform.localScale = scale;
        transform.Translate(Movex / 1000, Movey / 1000, 0);
        }
    }