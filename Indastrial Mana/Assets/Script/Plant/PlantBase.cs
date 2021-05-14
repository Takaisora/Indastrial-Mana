using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlantBase : MonoBehaviour
{
    public float PlantsWater = 0;// ���~�ϒl
    public float PlantsFert = 0;// �엿�~�ϒl
    private const byte _MAXPLANTSWATER = 100;// ���~�Ϗ���l
    private const byte _MAXPLANTSFERT = 100;// �엿�~�Ϗ���l
    // �f�o�b�O�pSerialize
    [SerializeField]
    private float _GenerateTimeCount = 0;// �����܂ł̌o�ߎ��ԃJ�E���g
    [SerializeField]
    private float _WitherTimeCount = 0;// �͊����ԃJ�E���g
    [SerializeField]
    protected GrowthState MyGrowth;// �A���̐������
    private byte _GeneratedCount = 0;// �}�i�������ɃJ�E���g
    private bool _IsCompleted = false;// �}�i��S�Đ�������������
    // UI
    [SerializeField]
    Image WaterGauge;// ���Q�[�WUI
    [SerializeField]
    Image FertGauge;// �엿�Q�[�WUI


    // ���x���f�U�C���p
    [SerializeField, Header("1�x�̃}�i������(�{, ����)"), Space, Space, Space]// 1�{ = 100�}�i
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
        if(MyGrowth == GrowthState.Planted)
        {
            // ���Ɣ엿�̏���
            PlantsWater -= _WaterConsumption * Time.deltaTime;
            if (PlantsWater < 0)
                PlantsWater = 0;
            PlantsFert -= _FertConsumption * Time.deltaTime;
            if (PlantsFert < 0)
                PlantsFert = 0;

            // �͊��o�ߎ��Ԃ��J�E���g
            _WitherTimeCount += Time.deltaTime;

            // ���엿�Е��ł��Ȃ��ꍇ�͂�n�߂�
            if (_IsOR)
            {
                // �����Ƃ�����Ȃ�J�E���g���Z�b�g
                if (PlantsWater > 0 && PlantsFert > 0)
                    _WitherTimeCount = 0;
            }
            // ���엿�����Ȃ��ꍇ�͂�n�߂�
            else
            {
                // �ǂ��炩����Ȃ�J�E���g���Z�b�g
                if (PlantsWater > 0 || PlantsFert > 0)
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
                _IsCompleted = true;
        }
    }

    protected void DrawGauge()
    {
        // �Q�[�WUI�ɕS�����ő��
        WaterGauge.fillAmount = PlantsWater / 100;
        FertGauge.fillAmount = PlantsFert / 100;
    }

    protected void Plant()
    {
        if(MyGrowth == GrowthState.Seed)
        {
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
        }
    }

    protected void Watering()
    {
        if(!_IsCompleted)
            PlantsWater = _MAXPLANTSWATER;
    }
    
    protected void fertilizing()
    {
        if (!_IsCompleted)
            PlantsFert = _MAXPLANTSFERT;
    }
    
    protected void Harvest()
    {
        if(MyGrowth == GrowthState.Generated)
        {
            Debug.Log("�}�i" + _NumOfGenerateMana +"�{���n�I");
            MyGrowth = GrowthState.Planted;
            // �S�Đ����ςȂ�͂��
            if(_IsCompleted)
                MyGrowth = GrowthState.Withered;
        }
    }
}   