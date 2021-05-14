using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
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
    private byte _GeneratedCount = 0;// マナ生成毎にカウント
    private bool _IsCompleted = false;// マナを全て生成しきったか
    // UI
    [SerializeField]
    Image WaterGauge;// 水ゲージUI
    [SerializeField]
    Image FertGauge;// 肥料ゲージUI


    // レベルデザイン用
    [SerializeField, Header("1度のマナ生成数(本, 整数)"), Space, Space, Space]// 1本 = 100マナ
    private byte _NumOfGenerateMana;
    [SerializeField, Header("マナ生成回数（回, 整数）")]
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
            // この生成で最後になるなら
            if (_NumOfGenerate <= _GeneratedCount)
                _IsCompleted = true;
        }
    }

    protected void DrawGauge()
    {
        // ゲージUIに百分率で代入
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
            Debug.Log("マナ" + _NumOfGenerateMana +"本収穫！");
            MyGrowth = GrowthState.Planted;
            // 全て生成済なら枯れる
            if(_IsCompleted)
                MyGrowth = GrowthState.Withered;
        }
    }
}   