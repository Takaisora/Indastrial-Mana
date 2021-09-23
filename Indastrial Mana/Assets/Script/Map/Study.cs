using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Study : SingletonMonoBehaviour<Study>
{
    [SerializeField, Header("�����ɕK�v�Ȏ���")]
    int _MoneyCost = 0;
    [SerializeField, Header("�����ɂ����鎞��(�b)")]
    int _TimeRequired = 0;
    [SerializeField, Header("���Y�����A��")]
    GameObject[] PlantsType = new GameObject[3];// �Ƃ肠������{�^3��
    [SerializeField, Header("���Y���ꂽ�킪�u�����ʒu")]
    GameObject SetPosition = null;
    [SerializeField, Header("�킪������e�I�u�W�F�N�g")]
    GameObject MapCanvas = null;
    private float _TimeCount = 0;        //����
    public bool IsStudying = false;    //����������
    public static int Madness = 0;        //���C�x
    public static int AddMadness = 5;     //1�x�̌����ŉ��Z����鋶�C�x
    public static int Craziness = 0;      //�������
    int Light = 50;         //�y�x���ʗp�i���j
    int Medium = 70;        //���x�A�d�x���ʗp�i���j
    int BaseMadness = 10;   //�������I��{�m���i���j
    int MadnessLv = 0;      //0���y�x�A1�����x�A2���d�x 
    int MadnessTime = 3;   //��{�̔������ԁ@MadnessLv*2��ǉ����Ďg�p�B
    float CrazyTime = 0;    //�������Ԍv��

    private int positionX = 0;
    private int positionY = 0;
    private float MadTime = 0;
    private float MadDelayTime = 0;
    private int Cra5Count = 0;

    public static bool CrazinessLv4 = false;//���C4�Ԗڃt���O

    [SerializeField]
    GameObject CrazinessLv5;

    [SerializeField]
    GameObject CrazinessLv6;

    //�ˑR�ψٗp
    int B = 0;
    int Y = 0;
    //Rey�p
    //X
    float X_ = 0;
    //Y
    float Y_ = 0;
    //�Ԓd�̍��W�i�[�p
    GameObject hit;

    [SerializeField]
    public Image crazygauge = null;

    private Animator animator;

    private const string _Study_Anim = "Study_Anim";


    void Start()
    {
        CrazinessLv4 = false;
        Madness = 80;
        AddMadness = 5;
        animator = GetComponent<Animator>();
        crazygauge.fillAmount = (float)Madness / (float)100;
        Craziness = 0;
    }

    void Update()
    {
        //�������Ȃ�
        if (IsStudying)
        {
            _TimeCount += Time.deltaTime;                 //���ԉ��Z

            animator.SetBool(_Study_Anim, true);

            //3�b�o������퐶�Y
            if (_TimeCount >= _TimeRequired)
            {
                animator.SetBool(_Study_Anim, false);
                IsStudying = false;
                _TimeCount = 0;
                PlayerController.Instance.MoveRatio = 1;// ���ɖ߂�

                //�풊�I
                int MaxRange = 0;

                switch(Day_1.day)
                {
                    case Day_1.Days.Day1:
                        MaxRange = 3;
                        break;
                    case Day_1.Days.Day2:
                        MaxRange = 4;
                        break;
                    case Day_1.Days.Day3:
                        MaxRange = 5;
                        break;
                    case Day_1.Days.Day4:
                        MaxRange = 6;
                        // ��������ˑR�ψ�
                        break;
                    case Day_1.Days.Day5:
                        MaxRange = 7;
                        break;
                    case Day_1.Days.Day6:
                        MaxRange = 8;
                        break;
                    case Day_1.Days.Day7:
                        MaxRange = 9;
                        break;
                    default:
                        break;
                }

                if (MaxRange > PlantsType.Length)
                    MaxRange = PlantsType.Length;// ��O����

                int SelectedNum = Random.Range(0, MaxRange);     // 0����z��̃T�C�Y�܂ł̗���


                // �I�΂ꂽ�ԍ��̎�𐶐�
                GameObject MySeed = Instantiate(PlantsType[SelectedNum]);
                if (SelectedNum < 3)
                    MySeed.name = "Seed";
                else if (SelectedNum < 6)
                    MySeed.name = "ObsSeed";
                MySeed.transform.position = SetPosition.transform.position;
                MySeed.transform.parent = MapCanvas.transform;
                Debug.Log("�^�C�v" + SelectedNum + "�̎킪���Y���ꂽ");
                TextLog.Instance.Insert($"�^�C�v{SelectedNum + 1}�̎킪���Y���ꂽ");
                AddMad();

                


 /*               //�ˑR�ψ�
                //if (MadnessLv >= 0)
                //{
                    B = Random.Range(1, 6);
                    switch (B)
                    {
                        case 1:
                            //�Ԓd�̍��W
                            X_ = -6;
                            Y_ = 3;
                            //�Ăяo��
                            GetPlants();
                            break;

                        case 2:
                            //�Ԓd�̍��W
                            X_ = -3;
                            Y_ = 3;
                            //�Ăяo��
                            GetPlants();
                        break;

                        case 3:
                            //�Ԓd�̍��W
                            X_ = 0;
                            Y_ = 3;
                            //�Ăяo��
                            GetPlants();
                        break;

                        case 4:
                            //�Ԓd�̍��W
                            X_ = 3;
                            Y_ = 3;
                            //�Ăяo��
                            GetPlants();
                        break;

                        case 5:
                            //�Ԓd�̍��W
                            X_ = 6;
                            Y_ = 3;
                            //�Ăяo��
                            GetPlants();
                        break;
                    }
                //}*/   


                //�����̒��I(���b�N�ł͎g��Ȃ��̂ŃR�����g�A�E�g)
                if (Random.Range(1, 101) <= Madness + BaseMadness)
                {
                    //���C�x�̔���
                    if (Madness < Light)
                    {
                        MadnessLv = 0;  //�y�x
                    }
                    else if (Madness <= Medium)
                    {
                        MadnessLv = 1;  //���x
                    }
                    else
                    {
                        MadnessLv = 2;  //�d�x
                    }
                    
                    //���C�x�ɉ������f�o�t�̒��I
                    switch (MadnessLv)
                    {
                        case 0:
                            Craziness = Random.Range(1, 5);
                            break;
                        case 1:
                            Craziness = Random.Range(1, 5);
                            break;
                        case 2:
                            Craziness = Random.Range(1, 6);
                            break;
                        default:
                            break;
                    }
                    switch (Craziness)
                    {
                        case 1:
                            PlayerController.Instance.MoveRatio = 0.5f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            PlayerController.Crazy1Buff = true;
                            break;
                        case 2:
                            PlayerController.Instance.MoveRatio = 0.0f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            PlayerController.Crazy2Buff = true;
                            break;
                        case 3:
                            Day_1.Crazy_Flag = true;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            PlayerController.Crazy3Buff = true;
                            break;
                        case 4:
                            CrazinessLv4 = true;//���ꂼ���̃X�N���v�g�Ńt���O�������Ƀ}�i�����^�C�������Z�b�g����B
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            PlayerController.Crazy4Buff = true;
                            break;
                        case 5:
                            positionX = Random.Range(1, 12);
                            positionY = Random.Range(1, 9);
                            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
                            var temp = Instantiate(CrazinessLv5,SetPostion, Quaternion.identity);
                            MapCanvas = GameObject.Find("MapCanvas");
                            temp.transform.parent = MapCanvas.transform;
                            //CrazinessLv5.transform.Translate(SetPostion);
                            MadTime += Time.deltaTime;
                            if (MadTime > 14)
                            {
                                Cra5Count++;
                                MadTime = 0;
                            }
                            break;
                        case 6:
                            CrazinessLv6.SetActive(true);
                            break;
                        default:
                            break;
                    }
                    Debug.Log("�󂯂��f�o�t��" + Craziness + "�ł��B");
                    TextLog.Instance.Insert($"�f�o�t{ Craziness}���󂯂�");
                    //��X�ǉ��\��
                    //���C�x�ɉ����ăf�o�t������
                    //switch (Craziness)
                    //{
                    //    case 0:
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
        }
    }

    public void Studying()
    {
        //�������łȂ����
        if (!IsStudying)
        {
            //���N���b�N�Ŏ���������Ă���Ό����J�n
            if (PlayerController.Money >= _MoneyCost)
            {
                IsStudying = true;
                PlayerController.Money -= (ushort)_MoneyCost;     //����
                PlayerController.Instance.MoveRatio = 0;// �v���C���[�̈ړ��𐧌�
                Debug.Log("�����J�n!\n�����c��" + PlayerController.Money);
                TextLog.Instance.Insert($"�����J�n!(�����c��{PlayerController.Money})");
                SoundManager.Instance.MoneySound();
            }
            else
            {
                Debug.Log("����������܂���");
                TextLog.Instance.Insert("����������܂���");
            }
        }
    }

    public void AddMad()
    {
        Madness += AddMadness;                  //���C�x���Z
                                                //���C�x��100���𒴂��Ȃ�
        crazygauge.fillAmount = (float)Madness / (float)100;
        if (Madness > 100)
        {
            Madness = 100;
        }
    }

    //�Ԓd�̑I��
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        //���g�����O
        Physics2D.queriesStartInColliders = false;

        hit = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(X_, Y_, 0), 100);

        if (Hit)
        {
            hit = Hit.transform.gameObject;
        }
        Debug.Log(Hit.collider.gameObject.transform.position);
        return Hit;
    }

    /*void GetPlants()
    {
        RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);
        //�A�����m
        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Untagged"))
        {
            Hit.collider.gameObject.GetComponent<PlantBase>(). saiz();
        }
    }*/
}