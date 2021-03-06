using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantBase : MonoBehaviour
{
    public float GrowSpeed = 1;// ?A??????????x

    //???Y???I???????????????
    protected bool Grow = false;
    //?A???????
    public bool GrowS = false;

    public float PlantsWater = 0;// ???~??l
    public float PlantsFert = 0;// ???~??l
    private const byte _MAXPLANTSWATER = 100;// ???~?????l
    private const byte _MAXPLANTSFERT = 100;// ???~?????l
    // ?f?o?b?O?pSerialize
    [SerializeField]
    private float _GenerateTimeCount = 0;// ????????o?????J?E???g
    [SerializeField]
    public float _WitherTimeCount = 0;// ???????J?E???g
    [SerializeField]
    protected GrowthState MyGrowth = GrowthState.Seed;// ?A??????????
    protected GameObject Player = null;
    public byte _GeneratedCount = 0;// ?}?i????????J?E???g
    protected bool _IsCompleted = false;// ?}?i??S???????????????
    private GameObject MyGarden = null;
    private Image _WaterGauge = null;
    private Image _FertGauge = null;
    private float _DecreaseRatio = 0;

    // ???x???f?U?C???p
    [SerializeField, Header("?A????")]
    private string _PlantsName = string.Empty;
    [SerializeField, Header("?}?i??????i??, ?????j"), Space, Space, Space]
    byte _NumOfGenerate;
    [SerializeField, Header("?????????(???100, ??????)")]
    byte _DefaultWater;
    [SerializeField, Header("?????????(???100, ??????)")]
    byte _DefaultFert;
    [SerializeField, Header("????{?????/?b(????)")]
    byte _WaterConsumption;
    [SerializeField, Header("????{?????/?b(????)")]
    byte _FertConsumption;
    [SerializeField, Header("?}?i????????K?v?????(?b, ??????)")]
    float _GenerateTime;
    [SerializeField, Header("????????????????????????(?b, ??????)")]
    public float _WitherTime;
    [SerializeField, Header("??????????????????????H")]
    bool _IsOR;

    //?????_???^2
    // ?o?t???????????????????bool???
    public bool Buff = false;
    // ?o?t???????????????????J?E???g????float???
    [SerializeField, Header("?o?t?|??????")]
    public float BuffTime;
    // ?o?t?????????????float???
    [SerializeField, Header("?o?t????")]
    public float GetBuff;

    //?????_???^3
    //?D??
    public bool Rob = false;
    //???Y
    public bool Create = false;
    //???????n?p
    bool Last = false;
    //???n??
    float M = 0;




    protected enum GrowthState : byte
    {
        Seed,     // ??
        Planted,  // ?A????
        Generated,// ?}?i????
        Withered  // ???
    }

    /// <summary>
    /// ?????????????A?????????????????????????
    /// </summary>
    protected void DepletionCheck()
    {
        _DecreaseRatio = DayParameter.Instance.DecreaseRatios[(int)Day_1.day - 1];// ?C???f?b?N?X??0?`6?Aday??1?`7????-1

        if (MyGrowth == GrowthState.Planted || MyGrowth == GrowthState.Generated)
        {
            // ??????????
            PlantsWater -= _WaterConsumption * Time.deltaTime * _DecreaseRatio;
            if (PlantsWater < 0)
                PlantsWater = 0;
            PlantsFert -= _FertConsumption * Time.deltaTime * _DecreaseRatio;
            if (PlantsFert < 0)
                PlantsFert = 0;

            // ????o???????J?E???g
            _WitherTimeCount += Time.deltaTime;

            // ?????Е????????????n???
            if (_IsOR)
            {
                // ?????????????J?E???g???Z?b?g
                if (PlantsWater > 0 && PlantsFert > 0)
                    _WitherTimeCount = 0;
            }
            // ????????????????n???
            else
            {
                // ???????????J?E???g???Z?b?g
                if (PlantsWater > 0 || PlantsFert > 0)
                    _WitherTimeCount = 0;
            }

            // ?K???????B?????????
            if (_WitherTime <= _WitherTimeCount)
                MyGrowth = GrowthState.Withered;
        }
    }

    /// <summary>
    /// ??????J?E???g?A??莞????????????
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
            Debug.Log("?A?????}?i?????v" + _GeneratedCount + "????");
            TextLog.Instance.Insert("{_PlantsName}???}?i???");
        }
    }

    protected void DrawGauge()
    {
        // ?Q?[?WUI??S????????
        _WaterGauge.fillAmount = PlantsWater / 100;
        _FertGauge.fillAmount = PlantsFert / 100;
    }

    /// <summary>
    /// ?A????A???鏈??
    /// </summary>
    /// <param name="WGauge">?A??????d????Q?[?WUI</param>
    /// <param name="FGauge">?A??????d????Q?[?WUI</param>
    public void Plant(GameObject Garden)
    {
        if (MyGrowth == GrowthState.Seed)
        {
            Garden Gardens = Garden.GetComponent<Garden>();
            // ?Q?[?W?N??
            _WaterGauge = Gardens.WaterGauge.GetComponent<Image>();
            _WaterGauge.GetComponent<CanvasGroup>().alpha = 1;
            _FertGauge = Gardens.FertGauge.GetComponent<Image>();
            _FertGauge.GetComponent<CanvasGroup>().alpha = 1;

            // ?t???O????
            Gardens.IsPlanted = true;
            PlayerController.Instance.CarryItem = null;
            PlayerController.Instance.Tool = PlayerController.ToolState.None;

            MyGarden = Garden;
            this.gameObject.tag = "Untagged";// Item????v???C???[?????????O??
            this.name = "Plant";
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
            TextLog.Instance.Insert($"{_PlantsName}????A????");
            //?A??????
            GrowS = true;
        }
    }


    //?????_???^3??}?i????
    //public void Randomu3()
    //{
    //    if (Create)
    //    {
    //        _GeneratedCount = 1;
    //        Debug.Log("?d????}?i??" + _GeneratedCount + "?{???Y????");
    //    }
    //}


    protected void Watering()
    {
        Bucket.Instance.IsWaterFilled = false;
        PlayerController.Instance.Tool = PlayerController.ToolState.BucketEmpty;
        Debug.Log("????^????");
        TextLog.Instance.Insert($"{_PlantsName}?????^????");
        PlantsWater = _MAXPLANTSWATER;
        Tutorial_Text.Water = true;
        SoundManager.Instance.WaterSound();
    }

    protected void Fertilizing()
    {
        Shovel.Instance.IsFertFilled = false;
        PlayerController.Instance.Tool = PlayerController.ToolState.ShovelEmpty;
        Debug.Log("????^????");
        TextLog.Instance.Insert($"{_PlantsName}?????^????");
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

            // ?S????????????
            //if (_IsCompleted && Last)
            //{
            //    MyGrowth = GrowthState.Withered;
            //    M = 0;
            //}

            // ????????????????
            if (_NumOfGenerate <= _GeneratedCount)
            {
                _IsCompleted = true;
                MyGrowth = GrowthState.Withered;
            }

            //}

            MyBottle.IsManaFilled = true;
            PlayerController.Instance.Tool = PlayerController.ToolState.BottleFilled;
            Debug.Log("?}?i?????n????");
            TextLog.Instance.Insert("{_PlantsName}??}?i?????n????");
            //MyGrowth = GrowthState.Planted;
        }
    }



    public void Withered()
    {
        SoundManager.Instance.WitherSound();
        MyGarden.GetComponent<Garden>().IsPlanted = false;
        _WaterGauge.GetComponent<CanvasGroup>().alpha = 0;
        _FertGauge.GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("??????????..");
        TextLog.Instance.Insert($"{_PlantsName}???????????...");
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