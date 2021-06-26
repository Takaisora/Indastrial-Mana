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

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる

        //接触したのはガーデンだったら実行
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //植物検知  //生産時間チェック
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted　&& Grow == true)
            {
                //フェアリー処理
                FairyTimeCount += Time.deltaTime;
                if (FairyTimeCount == 0)
                {
                    Gardens.MyPlants.GetComponent<PlantBase>().Mana = Mana + Robbery;
                    Debug.Log("獲得マナが増えました");
                }
            }
        }
    }
}
