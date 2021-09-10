using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlant : PlantBase
{
    private Animator animator;

    private const string _Grow = "Grow";

    private const string _Generat = "Generat";

    //デバフ関数
    int MovingSpeed = 0;
    int Speed = 0;
    int TimeRequired = 5;   //所要時間

    float TimeCount = 0;        //時間

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //関数呼び出しテスト
        //if (Input.GetMouseButtonDown(2))
        //    base.Plant();
        //if (Input.GetMouseButtonDown(0))
        //    base.Watering();
        //if (Input.GetMouseButtonDown(1))
        //    base.fertilizing();
        //if (Input.GetKeyDown(KeyCode.Space))
        //    base.Harvest();

        if (base.MyGrowth == GrowthState.Planted)
        {
            base.Growing();
            animator.SetBool(_Grow, true);
        }
        if (base.MyGrowth == GrowthState.Generated)
            animator.SetBool(_Generat, true);
        else
            animator.SetBool(_Generat, false);



        //時間経過によるバフ、デバフ抽選
        TimeCount += Time.deltaTime;
        if (TimeCount >= TimeRequired)
        {
            TimeCount = 0;
            MovingSpeed = Random.Range(1, 3);
            switch (MovingSpeed)
            {
                case 1:
                    Speed = 1;
                    break;
                case 2:
                    Speed = 2;
                    break;
            }
            Debug.Log("受けた状態は" + Speed + "です。");
        }

        if (MyGrowth == GrowthState.Withered)
        {
            Debug.Log("枯れました...");
            SoundManager.Instance.WitherSound();
            Destroy(this.gameObject);
        }
        else if (base.MyGrowth != GrowthState.Seed)
            base.DepletionCheck();


        DrawGauge();
    }
}
