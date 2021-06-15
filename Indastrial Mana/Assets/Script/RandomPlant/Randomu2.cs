using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu2 : PlantBase
{
    [SerializeField, Header("成長速度のバフ用")]
    float GrowRatio;
    [SerializeField, Header("成長速度のデバフ用")]
    float GrowRatioDecline;
    [SerializeField, Header("魔法が使える回数")]
    int MagicCount;
    [SerializeField, Header("魔法実行回数")]
    int MagicNumber;

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


    }

    // 2Dの場合のトリガー判定
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
                    //抽選、回数処理
                    ProductionSpeed = Random.Range(1, 3);
                    switch (ProductionSpeed)
                    {
                        case 1:
                            Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatio;
                        MagicCount++;
                        //割り算
                            break;
                        case 2:
                            Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                            //割り算
                            break;
                    }

                }
            }


     }


}
//効果範囲を変数で設定できるようにする（シリアライズ化）(優先度低)

        //ランダム？でバフかデバフかを判定する

        //効果時間を変数で宣言（シリアライズ化）、時間になったら元のスピードに戻る(魔法を受ける側で設定した方が楽かも)

//魔法の使用回数上限になったら枯れる