using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Study : MonoBehaviour
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
    private bool _IsStudying = false;    //����������
    private int SelectedSeed = 0;   //���I���ꂽ��
    public static int Madness = 0;        //���C�x
    public static int AddMadness = 5;     //1�x�̌����ŉ��Z����鋶�C�x
    int Craziness = 0;      //�������
    int Light = 50;         //�y�x���ʗp�i���j
    int Medium = 70;        //���x�A�d�x���ʗp�i���j
    int BaseMadness = 10;   //�������I��{�m���i���j
    int MadnessLv = 0;      //0���y�x�A1�����x�A2���d�x 
    int MadnessTime = 10;   //��{�̔������ԁ@MadnessLv*2��ǉ����Ďg�p�B
    float CrazyTime = 0;    //�������Ԍv��
   //���C�t���O
    public static bool Madnesslv4 = false;
    public bool Madnesslv5 = false;
    public bool Madnesslv6 = false;
    private void Start()
    {
        Madnesslv4 = false;
        Madness = 90;
        AddMadness = 5;
    }
    void Update()
    {
        //�������Ȃ�
        if (_IsStudying)
        {
            _TimeCount += Time.deltaTime;                 //���ԉ��Z

            //3�b�o������퐶�Y
            if (_TimeCount >= _TimeRequired)
            {
                _IsStudying = false;
                _TimeCount = 0;
                PlayerController.MoveRatio = 1;// ���ɖ߂�

                //�풊�I
                SelectedSeed = Random.Range(0, PlantsType.Length);     // 0����z��̃T�C�Y�܂ł̗���
                // �I�΂ꂽ�ԍ��̎�𐶐�
                GameObject MySeed = Instantiate(PlantsType[SelectedSeed]);
                MySeed.name = "Seed";
                MySeed.transform.position = SetPosition.transform.position;
                MySeed.transform.parent = MapCanvas.transform;
                Debug.Log("�^�C�v" + SelectedSeed + "�̎킪���Y���ꂽ");

                Madness += AddMadness;                  //���C�x���Z
                //���C�x��100���𒴂��Ȃ�
                if (Madness > 100)
                {
                    Madness = 100;
                }

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
                            Craziness = Random.Range(1, 4);
                            break;
                        case 1:
                            Craziness = Random.Range(1, 4);
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
                            PlayerController.MoveRatio = 0.5f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            break;
                        case 2:
                            PlayerController.MoveRatio = 0.0f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            break;
                        case 3:
                            Day_1.Crazy_Flag = true;
                            break;
                        case 4:
                            Madnesslv4 = true;//���ꂼ���̃X�N���v�g�Ńt���O�������Ƀ}�i�����^�C�������Z�b�g����B
                            break;
                        case 5:
                            Madnesslv5 = true;
                            break;
                        case 6:
                            Madnesslv6 = true;
                            break;
                        default:
                            break;
                    }
                    Debug.Log("�󂯂��f�o�t��" + Craziness + "�ł��B");

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
        if (!_IsStudying)
        {
            //���N���b�N�Ŏ���������Ă���Ό����J�n
            if (PlayerController.Money >= _MoneyCost)
            {
                _IsStudying = true;
                PlayerController.Money -= (ushort)_MoneyCost;     //����
                PlayerController.MoveRatio = 0;// �v���C���[�̈ړ��𐧌�
                Debug.Log("�����J�n!\n�����c��" + PlayerController.Money);
            }
            else
                Debug.Log("����������܂���");
        }
    }
}
