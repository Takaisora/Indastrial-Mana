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

    //植物の数カウント用
    protected int Plants = 0;
    //生産と奪う判定
    int RobCreate = 0;

    bool Fairy = false;     //妖精トリガー
    private float _FTimeCount = 0;        //時間


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

        if (Fairy)
        {
            _FTimeCount += Time.deltaTime;
            //妖精呼び出し
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                Fairy = false;
                //生産、奪う処理
                RobCreate = Random.Range(1, 2);
                switch (RobCreate)
                {
                    case 1: //生産
                        Debug.Log("妖精がマナを生産しました");
                        break;

                    case 2: //奪う
                            //Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;
                        Debug.Log("マナが奪われた");
                        if (Plants >= 2)
                        {
                            Debug.Log("妖精がマナを生産しました");
                            Count = true;
                        }
                        break;
                }
            }
        }
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        //接触したのはガーデンだったら実行
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //植物検知
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
            }
        }

    }

    //植物の数をカウント
    int CheckAreaPlant()
    {
        foreach(Transform child in transform)
        {
            Plants ++;
        }
        return Plants;
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


//奪う
//植物マナ生産数-0.5
//範囲内の植物が1本なら
//+0.5
//範囲内の植物が2本なら
//+1
