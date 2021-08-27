using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    int Madness = 0;        //狂気度
    int AddMadness = 5;     //1度の研究で加算される狂気度
    int Craziness = 0;      //発狂種類
    int Light = 50;         //軽度判別用（％）
    int Medium = 70;        //中度、重度判別用（％）
    int BaseMadness = 10;   //発狂抽選基本確率（％）
    int MadnessLv = 0;      //0＝軽度、1＝中度、2＝重度 }

    private Animator animator;

    private const string _Study_Anim = "Study_Anim";

    void Start()
    {
        animator = GetComponent<Animator>();
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
                MySeed.name = "Seed";
                MySeed.transform.position = SetPosition.transform.position;
                MySeed.transform.parent = MapCanvas.transform;
                Debug.Log("タイプ" + SelectedSeed + "の種が生産された");

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
            }
            else
                Debug.Log("資金が足りません");
        }
    }
}
