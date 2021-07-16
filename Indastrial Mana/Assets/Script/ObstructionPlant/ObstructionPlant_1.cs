using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionPlant_1 : PlantBase
{
    private float DebuffTime = 3;//çSë©éûä‘
    private bool _isBuffed = false;
    // Update is called once per frame
    private void Update()
    {
        if (base.MyGrowth == GrowthState.Planted)
            base.Growing();

        if (base.MyGrowth != GrowthState.Seed)
        {
            base.DepletionCheck();
            base.DrawGauge();
            if (!_isBuffed)
            {
                if (base.PlantsWater == 0 || base.PlantsFert == 0)
                {
                    Vector3 CatchPosition = new Vector3(Mathf.RoundToInt(Player.transform.position.x), Mathf.RoundToInt(Player.transform.position.y));
                    //Debug.Log(CatchPosition);
                    for (int m = -1; m < 2; m++)
                    {
                        for (int n = -1; n < 2; n++)
                        {
                            Vector3 CatchPlantPosition = new Vector3(Mathf.RoundToInt(this.transform.position.x) + m, Mathf.RoundToInt(this.transform.position.y) + n);
                            //Debug.Log(CatchPlantPosition);
                            if (CatchPosition == CatchPlantPosition)
                            {
                                PlayerController.MoveRatio = 0;
                                PlayerController.Buff = true;
                                PlayerController.BuffTime = DebuffTime;
                                _isBuffed = true;
                            }
                        }
                    }
                }
            }
        }

        if (base.MyGrowth == GrowthState.Withered)
            base.Withered();

        if (Study.Madnesslv4)
        {
            _GeneratedCount = 0;
            Study.Madnesslv4 = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (base.MyGrowth == GrowthState.Seed)
        {
            return;
        }

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
                else if (PlayerController.Tool == PlayerController.ToolState.Bottle)// É{ÉgÉãÇ™ãÛÇ©ÇÕä÷êîÇ≈îªíf
                    base.Harvest();
            }
        }
    }
}