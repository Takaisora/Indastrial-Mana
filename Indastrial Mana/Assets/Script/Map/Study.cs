using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Study : SingletonMonoBehaviour<Study>
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
    public bool IsStudying = false;    //研究中判定
    private int SelectedSeed = 0;   //抽選された種
    public static int Madness = 0;        //狂気度
    public static int AddMadness = 5;     //1度の研究で加算される狂気度
    int Craziness = 0;      //発狂種類
    int Light = 50;         //軽度判別用（％）
    int Medium = 70;        //中度、重度判別用（％）
    int BaseMadness = 10;   //発狂抽選基本確率（％）
    int MadnessLv = 0;      //0＝軽度、1＝中度、2＝重度 
    int MadnessTime = 10;   //基本の発狂時間　MadnessLv*2を追加して使用。
    float CrazyTime = 0;    //発狂時間計測
                            //狂気フラグ
    public static bool Madnesslv4 = false;
    public bool Madnesslv5 = false;
    public bool Madnesslv6 = false;

    //突然変異用
    int B = 0;
    int Y = 0;
    //Rey用
    //X
    float X_ = 0;
    //Y
    float Y_ = 0;
    //花壇の座標格納用
    GameObject hit;

    [SerializeField]
    public Image crazygauge = null;

    private Animator animator;

    private const string _Study_Anim = "Study_Anim";

    void Start()
    {
        Madnesslv4 = false;
        Madness = 50;
        AddMadness = 5;
        animator = GetComponent<Animator>();
        crazygauge.fillAmount = (float)Madness / (float)100;
    }

    void Update()
    {
        //研究中なら
        if (IsStudying)
        {
            _TimeCount += Time.deltaTime;                 //時間加算

            animator.SetBool(_Study_Anim, true);

            //3秒経ったら種生産
            if (_TimeCount >= _TimeRequired)
            {
                animator.SetBool(_Study_Anim, false);
                IsStudying = false;
                _TimeCount = 0;
                PlayerController.Instance.MoveRatio = 1;// 元に戻す

                //種抽選
                SelectedSeed = Random.Range(0, PlantsType.Length);     // 0から配列のサイズまでの乱数
                // 選ばれた番号の種を生成
                GameObject MySeed = Instantiate(PlantsType[SelectedSeed]);
                if (SelectedSeed < 3)
                    MySeed.name = "Seed";
                else if (SelectedSeed < 6)
                    MySeed.name = "ObsSeed";
                MySeed.transform.position = SetPosition.transform.position;
                MySeed.transform.parent = MapCanvas.transform;
                Debug.Log("タイプ" + SelectedSeed + "の種が生産された");
                TextLog.Instance.Insert($"タイプ{SelectedSeed}の種が生産された");
                AddMad();

                


                //突然変異
                //if (MadnessLv >= 0)
                //{
                    B = Random.Range(1, 6);
                    switch (B)
                    {
                        case 1:
                            //花壇の座標
                            X_ = -6;
                            Y_ = 3;
                            //呼び出し
                            GetPlants();
                            break;

                        case 2:
                            //花壇の座標
                            X_ = -3;
                            Y_ = 3;
                            //呼び出し
                            GetPlants();
                        break;

                        case 3:
                            //花壇の座標
                            X_ = 0;
                            Y_ = 3;
                            //呼び出し
                            GetPlants();
                        break;

                        case 4:
                            //花壇の座標
                            X_ = 3;
                            Y_ = 3;
                            //呼び出し
                            GetPlants();
                        break;

                        case 5:
                            //花壇の座標
                            X_ = 6;
                            Y_ = 3;
                            //呼び出し
                            GetPlants();
                        break;
                    }
                //}


                //発狂の抽選(モックでは使わないのでコメントアウト)
                if (Random.Range(1, 101) <= Madness + BaseMadness)
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
                            Craziness = Random.Range(1, 5);
                            break;
                        case 1:
                            Craziness = Random.Range(1, 5);
                            break;
                        case 2:
                            Craziness = Random.Range(1, 7);
                            break;
                        default:
                            break;
                    }
                    switch (Craziness)
                    {
                        case 1:
                            PlayerController.Instance.MoveRatio = 0.5f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            break;
                        case 2:
                            PlayerController.Instance.MoveRatio = 0.0f;
                            PlayerController.Buff = true;
                            PlayerController.BuffTime += MadnessTime;
                            break;
                        case 3:
                            Day_1.Crazy_Flag = true;
                            break;
                        case 4:
                            Madnesslv4 = true;//それぞれ種のスクリプトでフラグ成立時にマナ生成タイムをリセットする。
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
                    TextLog.Instance.Insert($"デバフ{ Craziness}を受けた");
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
        if (!IsStudying)
        {
            //左クリックで資金が足りていれば研究開始
            if (PlayerController.Money >= _MoneyCost)
            {
                IsStudying = true;
                PlayerController.Money -= (ushort)_MoneyCost;     //資金
                PlayerController.Instance.MoveRatio = 0;// プレイヤーの移動を制限
                Debug.Log("研究開始!\n資金残り" + PlayerController.Money);
                TextLog.Instance.Insert($"研究開始!(資金残り{PlayerController.Money})");
                SoundManager.Instance.MoneySound();
            }
            else
            {
                Debug.Log("資金が足りません");
                TextLog.Instance.Insert("資金が足りません");
            }
        }
    }

    public void AddMad()
    {
        Madness += AddMadness;                  //狂気度加算
                                                //狂気度は100％を超えない
        crazygauge.fillAmount = (float)Madness / (float)100;
        if (Madness > 100)
        {
            Madness = 100;
        }
    }

    //花壇の選定
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        //自身を除外
        Physics2D.queriesStartInColliders = false;

        hit = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //植物あるかないか判定
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(X_, Y_, 0), 100);

        if (Hit)
        {
            hit = Hit.transform.gameObject;
        }
        Debug.Log(Hit.collider.gameObject.transform.position);
        return Hit;
    }

    void GetPlants()
    {
        RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);
        //植物検知
        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Untagged"))
        {
            Hit.collider.gameObject.GetComponent<PlantBase>(). saiz();
        }
    }
}