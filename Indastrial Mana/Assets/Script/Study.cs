using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study : MonoBehaviour
{
    int Money = 1000;       //資金宣言
    int MoneyCost = 100;    //コスト宣言
    int TimeRequired = 3;   //所要時間
    int SelectedSeed = 0;   //抽選された種
    int Madness = 0;        //狂気度
    int AddMadness = 5;     //1度の研究で加算される狂気度
    int Craziness = 0;      //発狂種類
    int Light = 50;         //軽度判別用（％）
    int Medium = 70;        //中度、重度判別用（％）
    int BaseMadness = 10;   //発狂抽選基本確率（％）
    int MadnessLv = 0;      //0＝軽度、1＝中度、2＝重度


    float TimeCount = 0;        //時間
    bool IsStudying = false;    //研究中判定

    [SerializeField]
    GameObject[] PlantsType = new GameObject[3];// とりあえず基本型3種

    void Start()
    {
        
    }

    void Update()
    {
        //研究中なら
        if (IsStudying)
        {
            TimeCount += Time.deltaTime;                 //時間加算
            //3秒経ったら種生産
            if (TimeCount >= TimeRequired)
            {
                IsStudying = false;
                TimeCount = 0;
                //種抽選
                SelectedSeed = Random.Range(0, PlantsType.Length);     //0から配列のサイズまでの乱数
                GameObject MySeed = PlantsType[SelectedSeed];// 選ばれた番号の種を生成
                MySeed.name = "Seed";
                // プレイヤーに種を渡す
                Debug.Log("タイプ" + SelectedSeed + "の種を入手した");

                //Madness += AddMadness;                  //狂気度加算
                ////狂気度は100％を超えない
                //if (Madness > 100)
                //{
                //    Madness = 100;
                //}

                //発狂の抽選(モックでは使わないのでコメントアウト)
                //if (Random.Range(1,101) <= Madness + BaseMadness )
                //{
                //    //狂気度の判定
                //    if (Madness < Light)
                //    {
                //        MadnessLv = 0;  //軽度
                //    }
                //    else if (Madness <= Medium)
                //    {
                //        MadnessLv = 1;  //中度
                //    }
                //    else
                //    {
                //        MadnessLv = 2;  //重度
                //    }
                //    //狂気度にお応じたデバフの抽選
                //    switch (MadnessLv)
                //    {
                //        case 0:
                //            Craziness = Random.Range(1, 4);
                //            break;
                //        case 1:
                //            Craziness = Random.Range(1, 4);
                //            break;
                //        case 2:
                //            Craziness = Random.Range(1, 6);
                //            break;
                //        default:
                //        break;
                //    }
                //    Debug.Log("受けたデバフは" + Craziness + "です。");

                    //後々追加予定
                    //狂気度に応じてデバフを強化
                    //switch (Craziness)
                    //{
                    //    case 0:
                    //        break;
                    //    default:
                    //        break;
                    //}
                //}
            }
        }
        //研究中でなければ
        else if (!IsStudying)    
        {
            //左クリックで資金が足りていれば研究開始
            if (Input.GetMouseButtonDown(0) && Money >= MoneyCost)    //0=左クリック、1＝右クリック
            {
                Money -= MoneyCost;     //資金
                Debug.Log("資金残り" + Money);
                IsStudying = true;
            }      
        }
    }
}
