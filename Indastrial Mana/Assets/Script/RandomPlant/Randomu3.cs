using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("妖精出現カウント")]
    int _FairyTime = 0;
    //フェアリーによるマナ生産への影響
    [SerializeField, Header("強奪数")]
    float Robbery;
    [SerializeField, Header("妖精生産数")]
    float production;

    //影響範囲
    [SerializeField, Header("妖精行動範囲")]
    float FArea = 5;

    //植物の数カウント用
    protected int Plants = 0;
    //生産と奪う判定
    int RobCreate = 0;
    //時間
    private float _FTimeCount = 0;

    //左右判定
    int RL = 0;

    //花壇の座標格納用
    GameObject Hitr;
    GameObject Hitl;

    float X;
    float Y;

    //Garden Gardens;



    private void Update()
    {
        if (base.MyGrowth == GrowthState.Planted)
            base.Growing();

        if (base.MyGrowth != GrowthState.Seed)
        {
            base.DepletionCheck();
            base.DrawGauge();
        }

        if (base.MyGrowth == GrowthState.Withered)
            base.Withered();


        

        //妖精
        if (GrowS)
        {
            _FTimeCount += Time.deltaTime;
            //妖精呼び出し
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                //生産、奪う処理
                RobCreate = Random.Range(2, 3);
                switch (RobCreate)
                {
                    case 1: //生産
                        Create = true;
                        Debug.Log("妖精がマナの生産に成功した");
                        GrowS = false;
                        Randomu3();
                        break;
                    case 2: //枯らして増やす
                            RL = Random.Range(1, 3);
                            switch (RL)
                            {
                                case 1:
                                    Create = true;
                                    Debug.Log("妖精がマナを生産しました");
                                    GrowS = false;

                                RaycastHit2D HitR = CheckPlantR(transform.position.x, transform.position.y);
                                GetPlantsR();

                                Randomu3();
                                    break;
                                case 2:
                                    Create = true;
                                    Debug.Log("妖精がマナを生産しました");
                                    GrowS = false;

                                RaycastHit2D HitL = CheckPlantL(transform.position.x, transform.position.y);
                                GetPlantsL();

                                Randomu3();
                                    break;
                            }
                        break;

                }
            }
        }
    }

    ////植物の数チェック
    private RaycastHit2D CheckPlantR(float X, float Y)
    {
        Hitr = null;

        // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //植物あるかないか判定
        RaycastHit2D HitR = Physics2D.Raycast(CellPosition, new Vector3(2, 0, 1), 100);

        return HitR;
    }

    private RaycastHit2D CheckPlantL(float X, float Y)
    {
        Hitl = null;

        // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //植物あるかないか判定
        RaycastHit2D HitL = Physics2D.Raycast(CellPosition, new Vector3(-2, 0, 1), 100);

        return HitL;
    }

    //右
    void GetPlantsR()
    {
        RaycastHit2D HitR = CheckPlantR(transform.position.x, transform.position.y);
        //植物検知
        if (HitR.collider != null && HitR.collider.gameObject.CompareTag("Garden"))
        {
            if (base.MyGrowth == GrowthState.Planted && Hitr.GetComponent<Garden>().IsPlanted != false)
            {
                Hitr = HitR.collider.gameObject;
                string Plantnemu = Hitr.gameObject.name;
                string[] Pname = Plantnemu.Split('_');

            }
        }
    }

    //左
    void GetPlantsL()
    {
        RaycastHit2D HitL = CheckPlantL(transform.position.x, transform.position.y);
        if (HitL.collider != null && HitL.collider.gameObject.CompareTag("Garden"))
        {
            if (base.MyGrowth == GrowthState.Planted && Hitl.GetComponent<Garden>().IsPlanted != false)
            {
                Hitl = HitL.collider.gameObject;
            }
        }
    }

    //収穫
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (base.MyGrowth == GrowthState.Seed)
            return;

        if (collision.gameObject.tag == "Player")
        {
            base.Player = collision.gameObject;
            Vector3 CellPosition = new Vector3(Mathf.RoundToInt(Player.transform.position.x)
                                 , Mathf.RoundToInt(Player.transform.position.y));

            if (CellPosition == this.transform.position)
            {
                PlayerController PlayerController = Player.GetComponent<PlayerController>();

                if (PlayerController.Tool == PlayerController.ToolState.Bucket && Bucket.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.Shovel && Shovel.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.Bottle)// ボトルが空かは関数で判断
                    base.Harvest();
            }
        }
    }
}


//ランダム型3の修正後仕様仮


////                        Rob = true;
//Debug.Log("妖精がマナを奪った");
//if (Plants >= 2)
//{
//    Create = true;
//    Debug.Log("妖精がマナの生産に成功した");
//}
//GrowS = false;
//Randomu3();
//break;
////