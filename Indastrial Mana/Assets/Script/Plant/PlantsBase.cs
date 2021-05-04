using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsBase : MonoBehaviour
{
    // 全植物共通っぽい変数
    // Serializeはテスト用
    private const byte _MAXPLANTSWATER = 100;// 水蓄積上限値
    private const byte _MAXPLANTSFERT = 100;// 肥料蓄積上限値
    [SerializeField]
    public float _PlantsWater = 0;// 水蓄積値
    [SerializeField]
    public float _PlantsFert = 0;// 肥料蓄積値
    [SerializeField]
    private float _GenerateTimeCount = 0;// 生成までの経過時間カウント
    [SerializeField]
    private float _WitherTimeCount = 0;// 枯渇時間カウント
    [SerializeField]
    protected GrowthState MyGrowth;// 植物の成長状態
    private byte _GeneratedCount = 0;// マナ生成毎にカウント
    private bool IsCompleted = false;// マナを全て生成しきったか


    // 植物によって変わりそうな変数
    [SerializeField, Header("1度のマナ生成数(本, 整数)")]// 1本 = 100マナ
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
        if(!IsCompleted)
        {
            // 水と肥料の消耗
            _PlantsWater -= _WaterConsumption * Time.deltaTime;
            if (_PlantsWater < 0)
                _PlantsWater = 0;
            _PlantsFert -= _FertConsumption * Time.deltaTime;
            if (_PlantsFert < 0)
                _PlantsFert = 0;

            // 枯渇しているなら経過時間をカウント
            _WitherTimeCount += Time.deltaTime;

            // 水肥料片方でもない場合枯れ始める
            if (_IsOR)
            {
                // 両方ともあるならカウントリセット
                if (_PlantsWater > 0 && _PlantsFert > 0)
                    _WitherTimeCount = 0;
            }
            // 水肥料両方ない場合枯れ始める
            else
            {
                // どちらかあるならカウントリセット
                if (_PlantsWater > 0 || _PlantsFert > 0)
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
            Debug.Log("マナ" + _NumOfGenerateMana +"本収穫！");
            MyGrowth = GrowthState.Planted;
            // 全て生成済なら枯れる
            if(IsCompleted)
                MyGrowth = GrowthState.Withered;
        }
    }
}   