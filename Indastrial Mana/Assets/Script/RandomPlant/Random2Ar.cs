using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random2Ar : Randomu2
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
                        Buff = true;
                        Debug.Log("成長速度上昇");
                        MagicTime();
                        //割り算
                        break;
                    case 2:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                        Buff = true;
                        Debug.Log("成長速度低下");
                        MagicTime();
                        //割り算
                        break;
                }

            }
        }
    }

}
