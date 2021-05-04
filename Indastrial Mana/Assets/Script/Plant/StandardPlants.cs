using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlants : PlantsBase
{
    private void Update()
    {
        // 関数呼び出しテスト
        //if (Input.GetMouseButtonDown(2))
        //    base.Plant();
        //if (Input.GetMouseButtonDown(0))
        //    base.Watering();
        //if (Input.GetMouseButtonDown(1))
        //    base.fertilizing();
        //if (Input.GetKeyDown(KeyCode.Space))
        //    base.Harvest();

        if (base.MyGrowth == GrowthState.Planted)
            base.Growing();

        if (MyGrowth == GrowthState.Withered)
        {
            Debug.Log("枯れました...");
            Destroy(this.gameObject);
        }
        else if (base.MyGrowth != GrowthState.Seed)
            base.DepletionCheck();
    }
}
