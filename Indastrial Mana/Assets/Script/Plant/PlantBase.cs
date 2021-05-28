using System.Collections;
using System.Collections.Generic;
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
    protected GameObject Player = null;
    private byte _GeneratedCount = 0;// �}�i�������ɃJ�E���g
    private bool _IsCompleted = false;// �}�i��S�Đ�������������
    private GameObject MyGarden = null;
    private Image _WaterGauge = null;
    private Image _FertGauge = null;

    // ���x���f�U�C���p
    [SerializeField, Header("�}�i�����񐔁i��, �����j"), Space, Space, Space]
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
            Debug.Log("�A�����}�i�𐶐�");
            // ���̐����ōŌ�ɂȂ�Ȃ�
            if (_NumOfGenerate <= _GeneratedCount)
                _IsCompleted = true;
        }
    }

    protected void DrawGauge()
    {
        // �Q�[�WUI�ɕS�����ő��
        _WaterGauge.fillAmount = PlantsWater / 100;
        _FertGauge.fillAmount = PlantsFert / 100;
    }

    /// <summary>
    /// �A����A���鏈��
    /// </summary>
    /// <param name="WGauge">�A�����Ԓd�̐��Q�[�WUI</param>
    /// <param name="FGauge">�A�����Ԓd�̔엿�Q�[�WUI</param>
    public void Plant(GameObject WGauge, GameObject FGauge)
    {
        if(MyGrowth == GrowthState.Seed)
        {
            MyGarden = WGauge.transform.parent.gameObject;
            MyGarden.GetComponent<Garden>().IsPlanted = true;
            _WaterGauge = WGauge.GetComponent<Image>();
            _WaterGauge.GetComponent<CanvasGroup>().alpha = 1;
            _FertGauge = FGauge.GetComponent<Image>();
            _FertGauge.GetComponent<CanvasGroup>().alpha = 1;

            this.gameObject.tag = "Untagged";// Item���ƃv���C���[�����Ă�̂ŊO��
            this.name = "Plant";
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
            Debug.Log("���A����");
        }
    }

    protected void Watering()
    {
        if(!_IsCompleted)
        {
            Bucket.IsWaterFilled = false;
            Debug.Log("����^����");
            PlantsWater = _MAXPLANTSWATER;
        }
    }
    
    protected void Fertilizing()
    {
        if (!_IsCompleted)
        {
            Shovel.IsFertFilled = false;
            Debug.Log("�엿��^����");
            PlantsFert = _MAXPLANTSFERT;
        }
    }
    
    protected void Harvest()
    {
        if(MyGrowth == GrowthState.Generated)
        {
            GameObject MyPlayer = GameObject.Find("Player");
            Bottle MyBottle = MyPlayer.GetComponent<PlayerController>().CarryItem.GetComponent<Bottle>();
            if(MyBottle.IsManaFilled == false)
            {
                MyBottle.IsManaFilled = true;
                Debug.Log("�}�i�����n����");
                MyGrowth = GrowthState.Planted;
                // �S�Đ����ςȂ�͂��
                if (_IsCompleted)
                    MyGrowth = GrowthState.Withered;
            }
        }
    }

    protected void Withered()
    {
        MyGarden.GetComponent<Garden>().IsPlanted = false;
        _WaterGauge.GetComponent<CanvasGroup>().alpha = 0;
        _FertGauge.GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("�͂�Ă��܂���..");
        Destroy(this.gameObject);
    }
}   