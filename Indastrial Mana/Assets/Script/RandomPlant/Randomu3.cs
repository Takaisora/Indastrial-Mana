using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("妖精出現カウント")]
    float FairyTimeCount;

    //フェアリーによるマナ生産への影響
    [SerializeField, Header("強奪数")]
    float Robbery;

    //植物の数カウント用
    protected int Plants = 0;
    //生産と奪う判定
    int RobCreate = 0;


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
    }

    //植物の数をカウント
    int checkAreaPlant()
    {
        foreach(Transform child in transform)
        {
            Plants += 1;
        }
        return Plants;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる
        //植物
        //接触したのはガーデンだったら実行
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //植物検知
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                //妖精の処理
                FairyTimeCount += Time.deltaTime;
                if (FairyTimeCount <= 0)
                {
                   
                        RobCreate = Random.Range(1, 3);
                    switch (RobCreate)
                    {
                        case 1: //生産
                            Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount++;
                            Debug.Log("妖精がマナを生産しました");
                            break;

                        case 2: //奪う
                            //植物の数判断
                            if (Plants == 1)   //1個なら
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            if (Plants == 2)    //2個なら
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            break;
                    }
                    
                }

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
