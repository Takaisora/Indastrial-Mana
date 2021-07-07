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

    //‰e‹¿”ÍˆÍ
    [SerializeField, Header("—d¸s“®”ÍˆÍ")]
    float FArea = 5;

    //A•¨‚Ì”ƒJƒEƒ“ƒg—p
    protected int Plants = 0;
    //¶Y‚Æ’D‚¤”»’è
    int RobCreate = 0;
    //—d¸ƒgƒŠƒK[
    bool Fairy = false;
    //ŠÔ
    private float _FTimeCount = 0;

    Garden Gardens;



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


        RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
        {
            GetPlants();
        }

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
                RobCreate = Random.Range(2, 3);
                switch (RobCreate)
                {
                    case 1: //¶Y
                        Create = true;
                        Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                        break;
                    case 2: //’D‚¤
                        Rob = true;
                        Debug.Log("ƒ}ƒi‚ğ’D‚½");
                        Debug.Log("”ÍˆÍ“à‚ÌA•¨‚Í" + Plants);
                        if (Plants == 1)
                        {
                            Create = true;
                            Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                        }
                        break;
                }
            }
        }
    }

    //A•¨‚Ì”ƒ`ƒFƒbƒN
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        // À•W‚ğlÌŒÜ“ü‚Å®”‚É(‹ô”ŠÛ‚ß‚È‚Ì‚Å•s‹ï‡‹N‚±‚é‚©‚àH)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 0, 1), 100);

        return Hit;
    }

    void GetPlants()
    {
        //RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

            //A•¨ŒŸ’m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Plants++;
            }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // •¨‘Ì‚ªƒgƒŠƒK[‚ÉÚG‚µ‚Æ‚«A‚P“x‚¾‚¯ŒÄ‚Î‚ê‚é
        //ÚG‚µ‚½‚Ì‚ÍƒK[ƒfƒ“‚¾‚Á‚½‚çÀs
        if (collision.gameObject.CompareTag("Garden"))
        {
            Gardens = collision.gameObject.GetComponent<Garden>();
            //A•¨ŒŸ’m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
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


//’D‚¤
//A•¨ƒ}ƒi¶Y”-0.5
//”ÍˆÍ“à‚ÌA•¨‚ª1–{‚È‚ç
//+0.5
//”ÍˆÍ“à‚ÌA•¨‚ª2–{‚È‚ç
//+1

//ƒƒ‚
////A•¨‚Ì”‚ğƒJƒEƒ“ƒg
//Plants++;
//Debug.Log("”ÍˆÍ“à‚ÌA•¨‚Ì”‚Í" + Plants + "‚Å‚·");

//                Check("Untagged");
////”
//void Check(string tagname)
//{
//    tagObjects = GameObject.FindGameObjectsWithTag(tagname);
//    Debug.Log(tagObjects.Length);
//}


//RaycastHit2D CheckPlant(float X, float Y)
//{
//    // À•W‚ğlÌŒÜ“ü‚Å®”‚É(‹ô”ŠÛ‚ß‚È‚Ì‚Å•s‹ï‡‹N‚±‚é‚©‚àH)
//    Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
//                                     , Mathf.RoundToInt(Y));
//    //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
//    RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 5, 1), 100);

//    return Hit;
//}
//void GetPlants()
//{
//    RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

//    if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
//    {
//        Garden Gardens = gameObject.GetComponent<Garden>();
//        //A•¨ŒŸ’m
//        if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
//        {
//            Plants++;
//        }
//    }
//}

//
//float laserLength = 50;
//Vector2 startPosition = (Vector2)transform.position + new Vector2(0, 1);
//int layerMask = LayerMask.GetMask("Garden");
//RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, layerMask, 0);

//if (hit.collider != null)
//{
//    Debug.Log("ƒqƒbƒg" + hit.collider.tag);
//    Plants++;
//}