using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlants_1 : MonoBehaviour
{
    [SerializeField, Header("マナ生成数(本)")]
    byte _NumOfGenerateMana;
    [SerializeField, Header("成長に必要な時間")]
    float _GrowthTime;
    [SerializeField, Header("マナ生成に必要な時間")]
    float _GenerateTime;
    [SerializeField, Header("水消費量/秒")]
    byte _WaterConsumption = 10;
    [SerializeField, Header("肥料消費量/秒")]
    private byte _FertConsumption = 10;

    void Start()
    {
        
    }

    void Update()
    {

    }
}
