using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    int Money = 1000;       //�����錾
    int MoneyCost = 100;    //�R�X�g�錾
    int TimeRequired = 3;   //���v����
    int SelectedSeed = 0;   //���I���ꂽ��
    int Madness = 0;        //���C�x
    int AddMadness = 5;     //1�x�̌����ŉ��Z����鋶�C�x
    int Craziness = 0;      //�������
    int Light = 50;         //�y�x���ʗp�i���j
    int Medium = 70;        //���x�A�d�x���ʗp�i���j
    int BaseMadness = 10;   //�������I��{�m���i���j
    int MadnessLv = 0;      //0���y�x�A1�����x�A2���d�x


    float TimeCount = 0;        //����
    bool IsStudying = false;    //����������

    [SerializeField]
    GameObject[] PlantsType = new GameObject[3];// �Ƃ肠������{�^3��

    void Start()
    {
        
    }

    void Update()
    {
        //�������Ȃ�
        if (IsStudying)
        {
            TimeCount += Time.deltaTime;                 //���ԉ��Z
            //3�b�o������퐶�Y
            if (TimeCount >= TimeRequired)
            {
                IsStudying = false;
                TimeCount = 0;
                //�풊�I
                SelectedSeed = Random.Range(0, PlantsType.Length);     //0����z��̃T�C�Y�܂ł̗���
                GameObject MySeed = PlantsType[SelectedSeed];// �I�΂ꂽ�ԍ��̎�𐶐�
                MySeed.name = "Seed";
                // �v���C���[�Ɏ��n��
                Debug.Log("�^�C�v" + SelectedSeed + "�̎����肵��");

                //Madness += AddMadness;                  //���C�x���Z
                ////���C�x��100���𒴂��Ȃ�
                //if (Madness > 100)
                //{
                //    Madness = 100;
                //}

                //�����̒��I(���b�N�ł͎g��Ȃ��̂ŃR�����g�A�E�g)
                //if (Random.Range(1,101) <= Madness + BaseMadness )
                //{
                //    //���C�x�̔���
                //    if (Madness < Light)
                //    {
                //        MadnessLv = 0;  //�y�x
                //    }
                //    else if (Madness <= Medium)
                //    {
                //        MadnessLv = 1;  //���x
                //    }
                //    else
                //    {
                //        MadnessLv = 2;  //�d�x
                //    }
                //    //���C�x�ɂ��������f�o�t�̒��I
                //    switch (MadnessLv)
                //    {
                //        case 0:
                //            Craziness = Random.Range(1, 4);
                //            break;
                //        case 1:
                //            Craziness = Random.Range(1, 4);
                //            break;
                //        case 2:
                //            Craziness = Random.Range(1, 6);
                //            break;
                //        default:
                //        break;
                //    }
                //    Debug.Log("�󂯂��f�o�t��" + Craziness + "�ł��B");

                    //��X�ǉ��\��
                    //���C�x�ɉ����ăf�o�t������
                    //switch (Craziness)
                    //{
                    //    case 0:
                    //        break;
                    //    default:
                    //        break;
                    //}
                //}
            }
        }
        //�������łȂ����
        else if (!IsStudying)    
        {
            //���N���b�N�Ŏ���������Ă���Ό����J�n
            if (Input.GetMouseButtonDown(0) && Money >= MoneyCost)    //0=���N���b�N�A1���E�N���b�N
            {
                Money -= MoneyCost;     //����
                Debug.Log("�����c��" + Money);
                IsStudying = true;
            }      
        }
    }
}
