using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu2 : PlantBase
{
    [SerializeField, Header("成長速度のバフ用")]
    float GrowRatio;
    [SerializeField, Header("成長速度のデバフ用")]
    float GrowRatioDecline;
    //魔法が使える回数
    int MagicCount = 0;
    [SerializeField, Header("魔法実行回数")]
    int MagicNumber;

    //バフ開始まで
    float _BTime = 0;
    //バフが掛かる時間
    float _BTimeEND = 0;
    //バフ開始
    bool B = false;
    //成長速度増
    bool Grow_R = false;
    //成長速度減
    bool Grow_RD = false;

    //魔法抽選用
    int ProductionSpeed = 0;

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

        if (GrowS)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(5, 5);
        }
        if (GrowS)
        {
            //バフ時間関連
            _BTime += Time.deltaTime;
            if (_BTime >= BuffTime)
            {
                B = true;
                _BTime = 0;
            }
        }
        if (Buff && Grow_R)
        {
            _BTimeEND += Time.deltaTime;
            //魔法効果打消し
            if (_BTimeEND >= GetBuff)
            {
                _BTimeEND = 0;
                MagicTime();
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //接触したのはガーデンだったら実行
        //バフ
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //植物検知
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted && MagicCount < MagicNumber && B)
            {
                //抽選、回数処理
                ProductionSpeed = Random.Range(1, 2);
                switch (ProductionSpeed)
                {
                    case 1:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatio;
                        MagicCount++;
                        Buff = true;
                        Grow_R = true;
                        Debug.Log("成長速度上昇");
                        //割り算
                        break;
                    case 2:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                        Buff = true;
                        Grow_RD = true;
                        Debug.Log("成長速度低下");
                        //割り算
                        break;
                }
            }
        }

        //収穫

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

    //ランダム型2魔法時間、効果
    public void MagicTime()
    {
        //成長速度増加
        if (Grow_R && Buff)
        {
            GetComponent<Randomu2>().GrowSpeed = GrowSpeed / GrowRatio;
            Debug.Log("成長速度が戻りました");
            Buff = false;
        }
        //成長速度減少
        if (Grow_RD && Buff)
        {
            GetComponent<Randomu2>().GrowSpeed = GrowSpeed / GrowRatioDecline;
            Debug.Log("成長速度が戻りました");
            Buff = false;
        }
    }


}
//効果範囲を変数で設定できるようにする（シリアライズ化）(優先度低)

        //ランダム？でバフかデバフかを判定する

        //効果時間を変数で宣言（シリアライズ化）、時間になったら元のスピードに戻る(魔法を受ける側で設定した方が楽かも)

//魔法の使用回数上限になったら枯れる