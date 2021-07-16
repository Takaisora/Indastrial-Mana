using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Study : MonoBehaviour
{
    [SerializeField, Header("研究に必要な資金")]
    int _MoneyCost = 0;
    [SerializeField, Header("研究にかかる時間(秒)")]
    int _TimeRequired = 0;
    [SerializeField, Header("生産される植物")]
    GameObject[] PlantsType = new GameObject[3];// とりあえず基本型3種
    [SerializeField, Header("生産された種が置かれる位置")]
    GameObject SetPosition = null;
    [SerializeField, Header("種が属する親オブジェクト")]
    GameObject MapCanvas = null;
    private float _TimeCount = 0;        //時間
    private bool _IsStudying = false;    //研究中判定
    private int SelectedSeed = 0;   //抽選された種
    int Madness = 90;        //狂気度
    int AddMadness = 5;     //1度の研究で加算される狂気度
    int Craziness = 0;      //発狂種類
    int Light = 50;         //軽度判別用（％）
    int Medium = 70;        //中度、重度判別用（％）
    int BaseMadness = 10;   //発狂抽選基本確率（％）
    int MadnessLv = 0;      //0＝軽度、1＝中度、2＝重度 
    int MadnessTime = 10;   //基本の発狂時間　MadnessLv*2を追加して使用。
    float CrazyTime = 0;    //発狂時間計測
    public static bool Madness_Flag = false;//狂気フラグ1
    public static bool Madnesslv1 = false;
    public static bool Madnesslv2 = false;
    public bool Madnesslv5 = false;
    public bool Madnesslv6 = false;
    private void Start()
    {
        Madness_Flag = false;
        Madnesslv1 = false;
        Madnesslv2 = false;
    }
    void Update()
    {
        //研究中なら
        if (_IsStudying)
        {
            _TimeCount += Time.deltaTime;                 //時間加算

            //3秒経ったら種生産
            if (_TimeCount >= _TimeRequired)
            {
                _IsStudying = false;
                _TimeCount = 0;
                PlayerController.MoveRatio = 1;// 元に戻す

                //種抽選
                SelectedSeed = Random.Range(0, PlantsType.Length);     // 0から配列のサイズまでの乱数
                // 選ばれた番号の種を生成
                GameObject MySeed = Instantiate(PlantsType[SelectedSeed]);
                MySeed.name = "Seed";
                MySeed.transform.position = SetPosition.transform.position;
                MySeed.transform.parent = MapCanvas.transform;
                Debug.Log("タイプ" + SelectedSeed + "の種が生産された");

                Madness += AddMadness;                  //狂気度加算
                //狂気度は100％を超えない
                if (Madness > 100)
                {
                    Madness = 100;
                }

                //発狂の抽選(モックでは使わないのでコメントアウト)
                if (Random.Range(1,101) <= Madness + BaseMadness )
                {
                    //狂気度の判定
                    if (Madness < Light)
                    {
                        MadnessLv = 0;  //軽度
                    }
                    else if (Madness <= Medium)
                    {
                        MadnessLv = 1;  //中度
                    }
                    else
                    {
                        MadnessLv = 2;  //重度
                    }
                    //狂気度に応じたデバフの抽選
                    switch (MadnessLv)
                    {
                        case 0:
                            Craziness = Random.Range(1, 4);
                            break;
                        case 1:
                            Craziness = Random.Range(1, 4);
                            break;
                        case 2:
                            Craziness = Random.Range(1, 6);
                            break;
                        default:
                        break;
                    }
                    switch(Craziness)
                    {
                        case 1:
                            /*PlayerController.MoveRatio = 0.5f;
                            CrazyTime += Time.deltaTime;
                            if(CrazyTime >= MadnessTime)
                            {
                                PlayerController.MoveRatio = 1.0f;
                                CrazyTime = 0;
                            }*/
                            Madnesslv1 = true;
                            break;
                        case 2:
                            /*PlayerController.MoveRatio = 0.0f;
                            CrazyTime += Time.deltaTime;
                            if (CrazyTime >= MadnessTime)
                            {
                                PlayerController.MoveRatio = 1.0f;
                                CrazyTime = 0;
                            }*/
                            Madnesslv2 = true;
                            break;
                        case 3:
                            Day_1.Crazy_Flag = true;
                            break;
                        case 4:
                            Madness_Flag = true;//それぞれ種のスクリプトでフラグ成立時にマナ生成タイムをリセットする。
                            break;
                        case 5:
                            Madnesslv5 = true;
                            break;
                        case 6:
                            Madnesslv6 = true;
                            break;
                        default:
                        break;
                    }
                    Debug.Log("受けたデバフは" + Craziness + "です。");

                //後々追加予定
                //狂気度に応じてデバフを強化
                //switch (Craziness)
                //{
                //    case 0:
                //        break;
                //    default:
                //        break;
                //}
                }
            }
        }
    }

    public void Studying()
    { 
        //研究中でなければ
        if (!_IsStudying)
        {
            //左クリックで資金が足りていれば研究開始
            if (PlayerController.Money >= _MoneyCost)
            {
                _IsStudying = true;
                PlayerController.Money -= (ushort)_MoneyCost;     //資金
                PlayerController.MoveRatio = 0;// プレイヤーの移動を制限
                Debug.Log("研究開始!\n資金残り" + PlayerController.Money);
            }
            else
                Debug.Log("資金が足りません");
        }
    }
}
