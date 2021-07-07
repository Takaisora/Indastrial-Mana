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
    //妖精トリガー
    bool Fairy = false;
    //時間
    private float _FTimeCount = 0;

    Garden Gardens;



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


        RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
        {
            GetPlants();
        }

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
                RobCreate = Random.Range(2, 3);
                switch (RobCreate)
                {
                    case 1: //生産
                        Create = true;
                        Debug.Log("妖精がマナを生産しました");
                        break;
                    case 2: //奪う
                        Rob = true;
                        Debug.Log("マナを奪た");
                        Debug.Log("範囲内の植物は" + Plants);
                        if (Plants == 1)
                        {
                            Create = true;
                            Debug.Log("妖精がマナを生産しました");
                        }
                        break;
                }
            }
        }
    }

    //植物の数チェック
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //植物あるかないか判定
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 0, 1), 100);

        return Hit;
    }

    void GetPlants()
    {
        //RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

            //植物検知
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Plants++;
            }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        //接触したのはガーデンだったら実行
        if (collision.gameObject.CompareTag("Garden"))
        {
            Gardens = collision.gameObject.GetComponent<Garden>();
            //植物検知
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
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


//奪う
//植物マナ生産数-0.5
//範囲内の植物が1本なら
//+0.5
//範囲内の植物が2本なら
//+1

//メモ
////植物の数をカウント
//Plants++;
//Debug.Log("範囲内の植物の数は" + Plants + "です");

//                Check("Untagged");
////数
//void Check(string tagname)
//{
//    tagObjects = GameObject.FindGameObjectsWithTag(tagname);
//    Debug.Log(tagObjects.Length);
//}


//RaycastHit2D CheckPlant(float X, float Y)
//{
//    // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
//    Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
//                                     , Mathf.RoundToInt(Y));
//    //植物あるかないか判定
//    RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 5, 1), 100);

//    return Hit;
//}
//void GetPlants()
//{
//    RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

//    if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
//    {
//        Garden Gardens = gameObject.GetComponent<Garden>();
//        //植物検知
//        if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
//        {
//            Plants++;
//        }
//    }
//}

//
//float laserLength = 50;
//Vector2 startPosition = (Vector2)transform.position + new Vector2(0, 1);
//int layerMask = LayerMask.GetMask("Garden");
//RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, layerMask, 0);

//if (hit.collider != null)
//{
//    Debug.Log("ヒット" + hit.collider.tag);
//    Plants++;
//}