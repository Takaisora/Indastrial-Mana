using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlantsBase : MonoBehaviour
{
    // �S�A�����ʂ��ۂ��ϐ�
    //private Vector2 _PlantsPosition = new Vector2(0,0);// ���A���̃}�X���W(1�P��)
    private const byte _DEFAULTWATER = 50;// ��������
    private const byte _DEFAULTFERT = 50;// �엿������
    private const byte _MAXPLANTSWATER = 100;// ���~�Ϗ���l
    private const byte _MAXPLANTFERT = 100;// �엿�~�Ϗ���l
    private byte _PlantsWater = 0;// ���~�ϒl
    private byte _PlantsFert = 0;// �엿�~�ϒl
    private float _GrowthTimeCount = 0;// �����p�o�ߎ��ԃJ�E���g
    private float _ConsumptionTimeCount = 0;// ����p�o�ߎ��ԃJ�E���g
    protected GrowthState myGrowth;// �A���̐������

    // �A���ɂ���ĕς�肻���ȕϐ�
    private byte _NumOfGenerateMana;// �}�i������(�{)
    private float _GrowthTime;//�����ɕK�v�Ȏ��� 
    private float _GenerateTime;// �}�i�����ɕK�v�Ȏ���
    private byte _WaterConsumption = 10;// �������/�b
    private byte _FertConsumption = 10;// �엿�����/�b

    protected enum GrowthState : byte
    {
        Seed,     // ��
        Grown,    // ������
        Generated,// �}�i����
        Harvested,// ���n��
        Dead      // �͂�
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void Timecount()
    {
        _ConsumptionTimeCount += Time.deltaTime;
        if(_ConsumptionTimeCount > 1)
        {
            Consumption();
            _ConsumptionTimeCount = 0;
        }
    }
    private void Consumption()
    {
        _PlantsWater -= _WaterConsumption;
        _PlantsFert -= _FertConsumption;
    }


    // �ȉ��v���C���[�Ăяo���p�֐�

    //�A����(�v��Ȃ�����)
    //public void Plant(Vector2 PlayerPosition)
    //{
    //    myGrowth = GrowthState.Seed;
    //    _PlantsPosition = PlayerPosition;
    //}
    // �����
    public void Watering()
    {
        _PlantsWater = _MAXPLANTSWATER;
    }
    // �{��
    public void fertilizing()
    {
        _PlantsFert = _MAXPLANTFERT;
    }
    // ���n
    public byte Harvest()
    {
        myGrowth = GrowthState.Harvested;
        return _NumOfGenerateMana;
    }
}
