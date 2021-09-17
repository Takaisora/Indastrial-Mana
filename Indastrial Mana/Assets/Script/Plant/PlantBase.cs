using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantBase : MonoBehaviour
{
    public float GrowSpeed = 1;// �A���̐������x

    //���Y���I����Ă����邩�ǂ���
    protected bool Grow = false;
    //�A����ꂽ��
    public bool GrowS = false;

    public float PlantsWater = 0;// ���~�ϒl
    public float PlantsFert = 0;// �엿�~�ϒl
    private const byte _MAXPLANTSWATER = 100;// ���~�Ϗ���l
    private const byte _MAXPLANTSFERT = 100;// �엿�~�Ϗ���l
    // �f�o�b�O�pSerialize
    [SerializeField]
    private float _GenerateTimeCount = 0;// �����܂ł̌o�ߎ��ԃJ�E���g
    [SerializeField]
    public float _WitherTimeCount = 0;// �͊����ԃJ�E���g
    [SerializeField]
    protected GrowthState MyGrowth = GrowthState.Seed;// �A���̐������
    protected GameObject Player = null;
    public byte _GeneratedCount = 0;// �}�i�������ɃJ�E���g
    protected bool _IsCompleted = false;// �}�i��S�Đ�������������
    private GameObject MyGarden = null;
    private Image _WaterGauge = null;
    private Image _FertGauge = null;
    private float _DecreaseRatio = 0;

    // ���x���f�U�C���p
    [SerializeField, Header("�A����")]
    private string _PlantsName = string.Empty;
    [SerializeField, Header("�}�i�����񐔁i��, �����j"), Space, Space, Space]
    byte _NumOfGenerate;
    [SerializeField, Header("���̏�����(���100, ������)")]
    byte _DefaultWater;
    [SerializeField, Header("�엿�̏�����(���100, ������)")]
    byte _DefaultFert;
    [SerializeField, Header("����{�����/�b(����)")]
    byte _WaterConsumption;
    [SerializeField, Header("�엿��{�����/�b(����)")]
    byte _FertConsumption;
    [SerializeField, Header("�}�i�����܂łɕK�v�Ȏ���(�b, ������)")]
    float _GenerateTime;
    [SerializeField, Header("�����𖞂��������Ɍ͂��܂ł̎���(�b, ������)")]
    public float _WitherTime;
    [SerializeField, Header("�����엿�ǂ��炩�����ł��͂��H")]
    bool _IsOR;

    //�����_���^2
    // �o�t���������Ă��邩�𔻕ʂ���bool�ϐ�
    public bool Buff = false;
    // �o�t���������Ă���Ȃ玞�Ԃ��J�E���g����float�ϐ�
    [SerializeField, Header("�o�t�|����܂�")]
    public float BuffTime;
    // �o�t�̌��ʎ��Ԃ��󂯎��float�ϐ�
    [SerializeField, Header("�o�t����")]
    public float GetBuff;

    //�����_���^3
    //�D��
    public bool Rob = false;
    //���Y
    public bool Create = false;
    //�������n�p
    bool Last = false;
    //���n��
    float M = 0;




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
        _DecreaseRatio = DayParameter.Instance.DecreaseRatios[(int)Day_1.day - 1];// �C���f�b�N�X��0�`6�Aday��1�`7�Ȃ̂�-1

        if (MyGrowth == GrowthState.Planted || MyGrowth == GrowthState.Generated)
        {
            // ���Ɣ엿�̏���
            PlantsWater -= _WaterConsumption * Time.deltaTime * _DecreaseRatio;
            if (PlantsWater < 0)
                PlantsWater = 0;
            PlantsFert -= _FertConsumption * Time.deltaTime * _DecreaseRatio;
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
        _GenerateTimeCount += Time.deltaTime * GrowSpeed;

        if (_GenerateTime <= _GenerateTimeCount)
        {
            _GeneratedCount++;
            _GenerateTimeCount = 0;
            MyGrowth = GrowthState.Generated;
            Tutorial_Text.Mana = true;
            Debug.Log("�A�����}�i�����v" + _GeneratedCount + "����");
            TextLog.Instance.Insert($"{_PlantsName}���}�i�𐶐�");
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
    public void Plant(GameObject Garden)
    {
        if (MyGrowth == GrowthState.Seed)
        {
            Garden Gardens = Garden.GetComponent<Garden>();
            // �Q�[�W�N��
            _WaterGauge = Gardens.WaterGauge.GetComponent<Image>();
            _WaterGauge.GetComponent<CanvasGroup>().alpha = 1;
            _FertGauge = Gardens.FertGauge.GetComponent<Image>();
            _FertGauge.GetComponent<CanvasGroup>().alpha = 1;

            // �t���O����
            Gardens.IsPlanted = true;
            PlayerController.Instance.CarryItem = null;
            PlayerController.Instance.Tool = PlayerController.ToolState.None;

            MyGarden = Garden;
            this.gameObject.tag = "Untagged";// Item���ƃv���C���[�����Ă�̂ŊO��
            this.name = "Plant";
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
            TextLog.Instance.Insert($"{_PlantsName}�̎��A����");
            //�A������
            GrowS = true;
        }
    }


    //�����_���^3�̃}�i����
    //public void Randomu3()
    //{
    //    if (Create)
    //    {
    //        _GeneratedCount = 1;
    //        Debug.Log("�d���̓}�i��" + _GeneratedCount + "�{���Y����");
    //    }
    //}


    protected void Watering()
    {
        Bucket.Instance.IsWaterFilled = false;
        PlayerController.Instance.Tool = PlayerController.ToolState.BucketEmpty;
        Debug.Log("����^����");
        TextLog.Instance.Insert($"{_PlantsName}�ɐ���^����");
        PlantsWater = _MAXPLANTSWATER;
        Tutorial_Text.Water = true;
        SoundManager.Instance.WaterSound();
    }

    protected void Fertilizing()
    {
        Shovel.Instance.IsFertFilled = false;
        PlayerController.Instance.Tool = PlayerController.ToolState.ShovelEmpty;
        Debug.Log("�엿��^����");
        TextLog.Instance.Insert($"{_PlantsName}�ɔ엿��^����");
        PlantsFert = _MAXPLANTSFERT;
        Tutorial_Text.Fert = true;
        SoundManager.Instance.fertilizerSound();
    }

    protected void Harvest()
    {
        if (MyGrowth == GrowthState.Generated)
        {
            Bottle MyBottle = PlayerController.Instance.CarryItem.GetComponent<Bottle>();
            //if (MyBottle.IsManaFilled == false)
            //    if (MyBottle.IsManaFilled == false)
            //        if (MyBottle.IsManaFilled == false)
            //            if (MyBottle.IsManaFilled == false)
            //                if (MyBottle.IsManaFilled == false)
            //                    M++;
            //if (M >= _GeneratedCount)
            //{
                //Last = true;

            MyGrowth = GrowthState.Planted;
            Tutorial_Text.Delivery = true;

            // �S�Đ����ςȂ�͂��
            //if (_IsCompleted && Last)
            //{
            //    MyGrowth = GrowthState.Withered;
            //    M = 0;
            //}

            // ���̐����ōŌ�ɂȂ�Ȃ�
            if (_NumOfGenerate <= _GeneratedCount)
            {
                _IsCompleted = true;
                MyGrowth = GrowthState.Withered;
            }

            //}

            MyBottle.IsManaFilled = true;
            PlayerController.Instance.Tool = PlayerController.ToolState.BottleFilled;
            Debug.Log("�}�i�����n����");
            TextLog.Instance.Insert($"{_PlantsName}�̃}�i�����n����");
            //MyGrowth = GrowthState.Planted;
        }
    }



    public void Withered()
    {
        SoundManager.Instance.WitherSound();
        MyGarden.GetComponent<Garden>().IsPlanted = false;
        _WaterGauge.GetComponent<CanvasGroup>().alpha = 0;
        _FertGauge.GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("�͂�Ă��܂���..");
        TextLog.Instance.Insert($"{_PlantsName}�͌͂�Ă��܂���...");
        Destroy(this.gameObject);
    }

    public void saiz()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(5, 5);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Withered();
    }
}