using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsBase : MonoBehaviour
{
    // �S�A�����ʂ��ۂ��ϐ�
    // Serialize�̓e�X�g�p
    private const byte _MAXPLANTSWATER = 100;// ���~�Ϗ���l
    private const byte _MAXPLANTSFERT = 100;// �엿�~�Ϗ���l
    [SerializeField]
    public float _PlantsWater = 0;// ���~�ϒl
    [SerializeField]
    public float _PlantsFert = 0;// �엿�~�ϒl
    [SerializeField]
    private float _GenerateTimeCount = 0;// �����܂ł̌o�ߎ��ԃJ�E���g
    [SerializeField]
    private float _WitherTimeCount = 0;// �͊����ԃJ�E���g
    [SerializeField]
    protected GrowthState MyGrowth;// �A���̐������
    private byte _GeneratedCount = 0;// �}�i�������ɃJ�E���g
    private bool IsCompleted = false;// �}�i��S�Đ�������������


    // �A���ɂ���ĕς�肻���ȕϐ�
    [SerializeField, Header("1�x�̃}�i������(�{, ����)")]// 1�{ = 100�}�i
    private byte _NumOfGenerateMana;
    [SerializeField, Header("�}�i�����񐔁i��, �����j")]
    byte _NumOfGenerate;
    [SerializeField, Header("���̏�����(���100, ������)")]
    byte _DefaultWater;
    [SerializeField, Header("�엿�̏�����(���100, ������)")]
    byte _DefaultFert;
    [SerializeField, Header("�������/�b(����)")]
    byte _WaterConsumption;
    [SerializeField, Header("�엿�����/�b(����)")]
    byte _FertConsumption;
    [SerializeField, Header("�}�i�����܂łɕK�v�Ȏ���(�b, ������)")]
    float _GenerateTime;
    [SerializeField, Header("�����𖞂��������Ɍ͂��܂ł̎���(�b, ������)")]
    float _WitherTime;
    [SerializeField, Header("�����엿�ǂ��炩�����ł��͂��H")]
    bool _IsOR;

    protected enum GrowthState : byte
    {
        Seed,     // ��
        Planted,  // �A����
        Generated,// �}�i����
        Withered  // �͂ꂽ
    }

    /// <summary>
    /// ���Ɣ엿�����Ղ��A�͊����Ă���Ȃ�����ɉ����Č͂��
    /// </summary>
    protected void DepletionCheck()
    {
        if(!IsCompleted)
        {
            // ���Ɣ엿�̏���
            _PlantsWater -= _WaterConsumption * Time.deltaTime;
            if (_PlantsWater < 0)
                _PlantsWater = 0;
            _PlantsFert -= _FertConsumption * Time.deltaTime;
            if (_PlantsFert < 0)
                _PlantsFert = 0;

            // �͊����Ă���Ȃ�o�ߎ��Ԃ��J�E���g
            _WitherTimeCount += Time.deltaTime;

            // ���엿�Е��ł��Ȃ��ꍇ�͂�n�߂�
            if (_IsOR)
            {
                // �����Ƃ�����Ȃ�J�E���g���Z�b�g
                if (_PlantsWater > 0 && _PlantsFert > 0)
                    _WitherTimeCount = 0;
            }
            // ���엿�����Ȃ��ꍇ�͂�n�߂�
            else
            {
                // �ǂ��炩����Ȃ�J�E���g���Z�b�g
                if (_PlantsWater > 0 || _PlantsFert > 0)
                    _WitherTimeCount = 0;
            }

            // �K��̎��ԂɒB������͂��
            if (_WitherTime <= _WitherTimeCount)
                MyGrowth = GrowthState.Withered;
        }
    }

    /// <summary>
    /// ���Ԃ��J�E���g�A��莞�ԂŐ���������
    /// </summary>
    protected void Growing()
    {
        _GenerateTimeCount += Time.deltaTime;

        if (_GenerateTime <= _GenerateTimeCount)
        {
            _GeneratedCount++;
            _GenerateTimeCount = 0;
            MyGrowth = GrowthState.Generated;
            // ���̐����ōŌ�ɂȂ�Ȃ�
            if (_NumOfGenerate <= _GeneratedCount)
                IsCompleted = true;
        }
    }

    protected void Plant()
    {
        if(MyGrowth == GrowthState.Seed)
        {
            _PlantsWater = _DefaultWater;
            _PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
        }
    }

    protected void Watering()
    {
        if(!IsCompleted)
            _PlantsWater = _MAXPLANTSWATER;
    }
    
    protected void fertilizing()
    {
        if (!IsCompleted)
            _PlantsFert = _MAXPLANTSFERT;
    }
    
    protected void Harvest()
    {
        if(MyGrowth == GrowthState.Generated)
        {
            Debug.Log("�}�i" + _NumOfGenerateMana +"�{���n�I");
            MyGrowth = GrowthState.Planted;
            // �S�Đ����ςȂ�͂��
            if(IsCompleted)
                MyGrowth = GrowthState.Withered;
        }
    }
}   