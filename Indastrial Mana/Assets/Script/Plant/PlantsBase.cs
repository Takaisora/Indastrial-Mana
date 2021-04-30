using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlantsBase : MonoBehaviour
{
    // 全植物共通っぽい変数
    //private Vector2 _PlantsPosition = new Vector2(0,0);// 当植物のマス座標(1単位)
    private const byte _DEFAULTWATER = 50;// 水初期量
    private const byte _DEFAULTFERT = 50;// 肥料初期量
    private const byte _MAXPLANTSWATER = 100;// 水蓄積上限値
    private const byte _MAXPLANTFERT = 100;// 肥料蓄積上限値
    private byte _PlantsWater = 0;// 水蓄積値
    private byte _PlantsFert = 0;// 肥料蓄積値
    private float _GrowthTimeCount = 0;// 成長用経過時間カウント
    private float _ConsumptionTimeCount = 0;// 消費用経過時間カウント
    protected GrowthState myGrowth;// 植物の成長状態

    // 植物によって変わりそうな変数
    private byte _NumOfGenerateMana;// マナ生成数(本)
    private float _GrowthTime;//成長に必要な時間 
    private float _GenerateTime;// マナ生成に必要な時間
    private byte _WaterConsumption = 10;// 水消費量/秒
    private byte _FertConsumption = 10;// 肥料消費量/秒

    protected enum GrowthState : byte
    {
        Seed,     // 種
        Grown,    // 成長後
        Generated,// マナ生成
        Harvested,// 収穫済
        Dead      // 枯れ
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


    // 以下プレイヤー呼び出し用関数

    //植える(要らないかも)
    //public void Plant(Vector2 PlayerPosition)
    //{
    //    myGrowth = GrowthState.Seed;
    //    _PlantsPosition = PlayerPosition;
    //}
    // 水やり
    public void Watering()
    {
        _PlantsWater = _MAXPLANTSWATER;
    }
    // 施肥
    public void fertilizing()
    {
        _PlantsFert = _MAXPLANTFERT;
    }
    // 収穫
    public byte Harvest()
    {
        myGrowth = GrowthState.Harvested;
        return _NumOfGenerateMana;
    }
}
