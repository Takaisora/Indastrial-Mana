using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("—d¸oŒ»ƒJƒEƒ“ƒg")]
    float FairyTimeCount;

    //ƒtƒFƒAƒŠ[‚É‚æ‚éƒ}ƒi¶Y‚Ö‚Ì‰e‹¿
    [SerializeField, Header("‹­’D”")]
    float Robbery;

    //A•¨‚Ì”ƒJƒEƒ“ƒg—p
    protected int Plants = 0;
    //¶Y‚Æ’D‚¤”»’è
    int RobCreate = 0;


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

    //A•¨‚Ì”‚ğƒJƒEƒ“ƒg
    int checkAreaPlant()
    {
        foreach(Transform child in transform)
        {
            Plants += 1;
        }
        return Plants;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // •¨‘Ì‚ªƒgƒŠƒK[‚Æ—£‚ê‚½‚Æ‚«A‚P“x‚¾‚¯ŒÄ‚Î‚ê‚é
        //A•¨
        //ÚG‚µ‚½‚Ì‚ÍƒK[ƒfƒ“‚¾‚Á‚½‚çÀs
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //A•¨ŒŸ’m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                //—d¸‚Ìˆ—
                FairyTimeCount += Time.deltaTime;
                if (FairyTimeCount <= 0)
                {
                   
                        RobCreate = Random.Range(1, 3);
                    switch (RobCreate)
                    {
                        case 1: //¶Y
                            Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount++;
                            Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                            break;

                        case 2: //’D‚¤
                            //A•¨‚Ì””»’f
                            if (Plants == 1)   //1ŒÂ‚È‚ç
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            if (Plants == 2)    //2ŒÂ‚È‚ç
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            break;
                    }
                    
                }

            }
        }

    }

    //ûŠn
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
                else if (PlayerController.Tool == PlayerController.ToolState.Bottle)// ƒ{ƒgƒ‹‚ª‹ó‚©‚ÍŠÖ”‚Å”»’f
                    base.Harvest();
            }
        }
    }
}
