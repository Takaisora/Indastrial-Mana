using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantBase : MonoBehaviour
{
    public float GrowSpeed = 1;// 植物の成長速度

    //生産が終わっていいるかどうか
    protected bool Grow = false;
    //植えられたら
    public bool GrowS = false;

    public float PlantsWater = 0;// 水蓄積値
    public float PlantsFert = 0;// 肥料蓄積値
    private const byte _MAXPLANTSWATER = 100;// 水蓄積上限値
    private const byte _MAXPLANTSFERT = 100;// 肥料蓄積上限値
    // デバッグ用Serialize
    [SerializeField]
    private float _GenerateTimeCount = 0;// 生成までの経過時間カウント
    [SerializeField]
    private float _WitherTimeCount = 0;// 枯渇時間カウント
    [SerializeField]
    protected GrowthState MyGrowth = GrowthState.Seed;// 植物の成長状態
    protected GameObject Player = null;
    public byte _GeneratedCount = 0;// マナ生成毎にカウント
    protected bool _IsCompleted = false;// マナを全て生成しきったか
    private GameObject MyGarden = null;
    private Image _WaterGauge = null;
    private Image _FertGauge = null;

    // レベルデザイン用
    [SerializeField, Header("マナ生成回数（回, 整数）"), Space, Space, Space]
    byte _NumOfGenerate;
    [SerializeField, Header("水の初期量(上限100, 少数可)")]
    byte _DefaultWater;
    [SerializeField, Header("肥料の初期量(上限100, 少数可)")]
    byte _DefaultFert;
    [SerializeField, Header("水消費量/秒(整数)")]
    byte _WaterConsumption;
    [SerializeField, Header("肥料消費量/秒(整数)")]
    byte _FertConsumption;
    [SerializeField, Header("マナ生成までに必要な時間(秒, 少数可)")]
    float _GenerateTime;
    [SerializeField, Header("条件を満たした時に枯れるまでの時間(秒, 少数可)")]
    float _WitherTime;
    [SerializeField, Header("水か肥料どちらかだけでも枯れる？")]
    bool _IsOR;



    //ランダム型2
    // バフがかかっているかを判別するbool変数
    public bool Buff = false;
    // バフがかかっているなら時間をカウントするfloat変数
    [SerializeField, Header("バフ掛かるまで")]
    public float BuffTime;
    // バフの効果時間を受け取るfloat変数
    [SerializeField, Header("バフ時間")]
    public float GetBuff;

    //ランダム型3
    //奪う
    public bool Rob = false;
    //生産
    public bool Create = false;
    //複数収穫用
    bool Last = false;
    //収穫回数
    float M = 0;




    protected enum GrowthState : byte
    {
        Seed,     // 種
        Planted,  // 植えた
        Generated,// マナ生成
        Withered  // 枯れた
    }

    /// <summary>
    /// 水と肥料を消耗し、枯渇しているなら条件に応じて枯れる
    /// </summary>
    protected void DepletionCheck()
    {
        if (MyGrowth == GrowthState.Planted)
        {
            // 水と肥料の消耗
            PlantsWater -= _WaterConsumption * Time.deltaTime;
            if (PlantsWater < 0)
                PlantsWater = 0;
            PlantsFert -= _FertConsumption * Time.deltaTime;
            if (PlantsFert < 0)
                PlantsFert = 0;

            // 枯渇経過時間をカウント
            _WitherTimeCount += Time.deltaTime;

            // 水肥料片方でもない場合枯れ始める
            if (_IsOR)
            {
                // 両方ともあるならカウントリセット
                if (PlantsWater > 0 && PlantsFert > 0)
                    _WitherTimeCount = 0;
            }
            // 水肥料両方ない場合枯れ始める
            else
            {
                // どちらかあるならカウントリセット
                if (PlantsWater > 0 || PlantsFert > 0)
                    _WitherTimeCount = 0;
            }

            // 規定の時間に達したら枯れる
            if (_WitherTime <= _WitherTimeCount)
                MyGrowth = GrowthState.Withered;
        }
    }

    /// <summary>
    /// 時間をカウント、一定時間で成長させる
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
            Debug.Log("植物がマナを合計" + _GeneratedCount + "生成");
            // この生成で最後になるなら
            if (_NumOfGenerate <= _GeneratedCount)
            {
                _IsCompleted = true;
            }
        }
    }

    protected void DrawGauge()
    {
        // ゲージUIに百分率で代入
        _WaterGauge.fillAmount = PlantsWater / 100;
        _FertGauge.fillAmount = PlantsFert / 100;
    }

    /// <summary>
    /// 植物を植える処理
    /// </summary>
    /// <param name="WGauge">植えた花壇の水ゲージUI</param>
    /// <param name="FGauge">植えた花壇の肥料ゲージUI</param>
    public void Plant(GameObject Garden)
    {
        if (MyGrowth == GrowthState.Seed)
        {
            Garden Gardens = Garden.GetComponent<Garden>();
            // ゲージ起動
            _WaterGauge = Gardens.WaterGauge.GetComponent<Image>();
            _WaterGauge.GetComponent<CanvasGroup>().alpha = 1;
            _FertGauge = Gardens.FertGauge.GetComponent<Image>();
            _FertGauge.GetComponent<CanvasGroup>().alpha = 1;

            // フラグ処理
            Gardens.IsPlanted = true;
            PlayerController.Instance.CarryItem = null;
            PlayerController.Instance.Tool = PlayerController.ToolState.None;

            MyGarden = Garden;
            this.gameObject.tag = "Untagged";// Itemだとプレイヤーが持てるので外す
            this.name = "Plant";
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
            Debug.Log("種を植えた");
            //植えたら
            GrowS = true;
        }
    }


    //ランダム型3のマナ処理
    public void Randomu3()
    {
        if (Create)
        {
            _GeneratedCount = 1;
            Debug.Log("妖精はマナを" + _GeneratedCount + "本生産した");
        }
    }


    protected void Watering()
    {
        if (!_IsCompleted)
        {
            Bucket.Instance.IsWaterFilled = false;
            PlayerController.Instance.Tool = PlayerController.ToolState.BucketEmpty;
            Debug.Log("水を与えた");
            PlantsWater = _MAXPLANTSWATER;
            Tutorial_Text.Water = true;
        }
    }

    protected void Fertilizing()
    {
        if (!_IsCompleted)
        {
            Shovel.Instance.IsFertFilled = false;
            PlayerController.Instance.Tool = PlayerController.ToolState.ShovelEmpty;
            Debug.Log("肥料を与えた");
            PlantsFert = _MAXPLANTSFERT;
            Tutorial_Text.Fert = true;
        }
    }

    protected void Harvest()
    {
        if (MyGrowth == GrowthState.Generated)
        {
            Bottle MyBottle = PlayerController.Instance.CarryItem.GetComponent<Bottle>();
            if (MyBottle.IsManaFilled == false)
                if (MyBottle.IsManaFilled == false)
                    if (MyBottle.IsManaFilled == false)
                        if (MyBottle.IsManaFilled == false)
                            if (MyBottle.IsManaFilled == false)
                                M++;
            if (M >= _GeneratedCount)
            {
                Last = true;

                MyGrowth = GrowthState.Planted;
                Tutorial_Text.Delivery = true;
                // 全て生成済なら枯れる
                if (_IsCompleted && Last)
                {
                    MyGrowth = GrowthState.Withered;
                    M = 0;
                }
            }

            MyBottle.IsManaFilled = true;
            PlayerController.Instance.Tool = PlayerController.ToolState.BottleFilled;
            Debug.Log("マナを収穫した");
            MyGrowth = GrowthState.Planted;

        }
    }



    public void Withered()
    {
        MyGarden.GetComponent<Garden>().IsPlanted = false;
        _WaterGauge.GetComponent<CanvasGroup>().alpha = 0;
        _FertGauge.GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("枯れてしまった..");
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