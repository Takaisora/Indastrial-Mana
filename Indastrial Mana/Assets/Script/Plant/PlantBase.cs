using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBase : MonoBehaviour
{
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
    protected GrowthState MyGrowth;// 植物の成長状態
    protected GameObject Player = null;
    private byte _GeneratedCount = 0;// マナ生成毎にカウント
    private bool _IsCompleted = false;// マナを全て生成しきったか
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
        if(MyGrowth == GrowthState.Planted)
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
        _GenerateTimeCount += Time.deltaTime;

        if (_GenerateTime <= _GenerateTimeCount)
        {
            _GeneratedCount++;
            _GenerateTimeCount = 0;
            MyGrowth = GrowthState.Generated;
            Debug.Log("植物がマナを生成");
            // この生成で最後になるなら
            if (_NumOfGenerate <= _GeneratedCount)
                _IsCompleted = true;
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

            this.gameObject.tag = "Untagged";// Itemだとプレイヤーが持てるので外す
            this.name = "Plant";
            PlantsWater = _DefaultWater;
            PlantsFert = _DefaultFert;
            MyGrowth = GrowthState.Planted;
            Debug.Log("種を植えた");
        }
    }

    protected void Watering()
    {
        if(!_IsCompleted)
        {
            Bucket.IsWaterFilled = false;
            Debug.Log("水を与えた");
            PlantsWater = _MAXPLANTSWATER;
        }
    }
    
    protected void Fertilizing()
    {
        if (!_IsCompleted)
        {
            Shovel.IsFertFilled = false;
            Debug.Log("肥料を与えた");
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
                Debug.Log("マナを収穫した");
                MyGrowth = GrowthState.Planted;
                // 全て生成済なら枯れる
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
        Debug.Log("枯れてしまった..");
        Destroy(this.gameObject);
    }
}   