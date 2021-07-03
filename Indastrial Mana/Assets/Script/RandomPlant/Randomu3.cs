using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("—d¸oŒ»ƒJƒEƒ“ƒg")]
    int _FairyTime = 0;

    //ƒtƒFƒAƒŠ[‚É‚æ‚éƒ}ƒi¶Y‚Ö‚Ì‰e‹¿
    [SerializeField, Header("‹­’D”")]
    float Robbery;

    [SerializeField, Header("—d¸¶Y”")]
    float production;

    //A•¨‚Ì”ƒJƒEƒ“ƒg—p
    protected int Plants = 0;
    //¶Y‚Æ’D‚¤”»’è
    int RobCreate = 0;

    bool Fairy = false;     //—d¸ƒgƒŠƒK[
    private float _FTimeCount = 0;        //ŠÔ


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

        //—d¸

        if (Fairy)
        {
            _FTimeCount += Time.deltaTime;
            //—d¸ŒÄ‚Ño‚µ
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                Fairy = false;
                //¶YA’D‚¤ˆ—
                RobCreate = Random.Range(1, 2);
                switch (RobCreate)
                {
                    case 1: //¶Y
                        Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                        break;

                    case 2: //’D‚¤
                            //Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;
                        Debug.Log("ƒ}ƒi‚ª’D‚í‚ê‚½");
                        if (Plants >= 2)
                        {
                            Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                            Count = true;
                        }
                        break;
                }
            }
        }
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // •¨‘Ì‚ªƒgƒŠƒK[‚ÉÚG‚µ‚Æ‚«A‚P“x‚¾‚¯ŒÄ‚Î‚ê‚é
        //ÚG‚µ‚½‚Ì‚ÍƒK[ƒfƒ“‚¾‚Á‚½‚çÀs
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //A•¨ŒŸ’m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
            }
        }

    }

    //A•¨‚Ì”‚ğƒJƒEƒ“ƒg
    int CheckAreaPlant()
    {
        foreach(Transform child in transform)
        {
            Plants ++;
        }
        return Plants;
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


//’D‚¤
//A•¨ƒ}ƒi¶Y”-0.5
//”ÍˆÍ“à‚ÌA•¨‚ª1–{‚È‚ç
//+0.5
//”ÍˆÍ“à‚ÌA•¨‚ª2–{‚È‚ç
//+1
