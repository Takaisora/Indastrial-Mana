using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlant : PlantBase
{
    private Animator animator;

    private const string _Grow = "Grow";

    private const string _Generat = "Generat";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        }

        if (base.MyGrowth == GrowthState.Generated)
            animator.SetBool(_Generat, true);
        else
            animator.SetBool(_Generat, false);

        if (base.MyGrowth == GrowthState.Withered)
        {
            SoundManager.Instance.WitherSound();
            base.Withered();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (base.MyGrowth == GrowthState.Seed)
            return;

        if(collision.gameObject.tag == "Player")
        {
            base.Player = collision.gameObject;
            Vector3 CellPosition = new Vector3(Mathf.RoundToInt(Player.transform.position.x)
                                 , Mathf.RoundToInt(Player.transform.position.y));

            if(CellPosition == this.transform.position)
            {
                switch (PlayerController.Instance.Tool)
                {
                    case PlayerController.ToolState.BucketFilled:
                        base.Watering();
                        break;
                    case PlayerController.ToolState.ShovelFilled:
                        base.Fertilizing();
                        break;
                    case PlayerController.ToolState.BottleEmpty:
                        base.Harvest();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
