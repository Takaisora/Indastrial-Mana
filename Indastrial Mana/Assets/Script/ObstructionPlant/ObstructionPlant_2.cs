using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionPlant_2 : PlantBase
{
    private Animator animator;
    private const string _Grow = "Grow";
    private const string _Generat = "Generat";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.MyGrowth == GrowthState.Planted)
        {
            base.Growing();
            animator.SetBool(_Grow, true);
        }

            if (base.MyGrowth != GrowthState.Seed)
        {
            base.DepletionCheck();
            base.DrawGauge();
            if (!PlayerController.Buff)
            {
                PlayerController.Instance.MoveRatio = 1.0f;
            }
            if (base.PlantsWater <= 33 && base.PlantsFert <= 33)
            {
                Vector3 SpeedDownPosition = new Vector3(Mathf.RoundToInt(Player.transform.position.x), Mathf.RoundToInt(Player.transform.position.y));
                for (int m = -1; m < 2; m++)
                {
                    for (int n = -1; n < 2; n++)
                    {
                        Vector3 SpeedDownPlantPosition = new Vector3(Mathf.RoundToInt(this.transform.position.x) + m, Mathf.RoundToInt(this.transform.position.y) + n);
                        if (SpeedDownPosition == SpeedDownPlantPosition)
                        {
                            PlayerController.Instance.MoveRatio = 0.5f;
                        }
                    }
                }
            }

        }
        if (base.MyGrowth == GrowthState.Generated)
            animator.SetBool(_Generat, true);
        else
            animator.SetBool(_Generat, false);

        if (base.MyGrowth == GrowthState.Withered)
        SoundManager.Instance.WitherSound();
        base.Withered();
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

                if (PlayerController.Tool == PlayerController.ToolState.BucketFilled && Bucket.Instance.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.ShovelFilled && Shovel.Instance.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.BottleEmpty)// É{ÉgÉãÇ™ãÛÇ©ÇÕä÷êîÇ≈îªíf
                    base.Harvest();
            }
        }
    }
}