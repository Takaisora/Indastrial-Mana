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

    //¶‰E”»’è
    int RL = 0;

    //‰Ô’d‚ÌÀ•WŠi”[—p
    GameObject Hitr;
    GameObject Hitl;

    float X;
    float Y;

    //Garden Gardens;



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

        Vector3 GardenPositionR = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));

        RaycastHit2D HitR = Physics2D.Raycast(GardenPositionR, new Vector3(2, 0, 0), 100);

        if (HitR.collider != null && HitR.collider.gameObject.CompareTag("Garden"))
        {
            GetPlantsR();
            Debug.Log(HitR.collider.gameObject.name);
            Debug.Log(HitR.collider.gameObject.transform.position);
        }

        Vector3 GardenPositionL = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));

        RaycastHit2D HitL = Physics2D.Raycast(GardenPositionL, new Vector3(-2, 0, 0), 100);
        if (HitL.collider != null && HitL.collider.gameObject.CompareTag("Garden"))
        {
            GetPlantsL();
            Debug.Log(HitL.collider.gameObject.name);
            Debug.Log(HitL.collider.gameObject.transform.position);
        }
        

        //—d¸
        if (GrowS)
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
                        Create = true;
                        Debug.Log("—d¸‚ªƒ}ƒi‚Ì¶Y‚É¬Œ÷‚µ‚½");
                        GrowS = false;
                        Randomu3();
                        break;
                    case 2: //ŒÍ‚ç‚µ‚Ä‘‚â‚·
                        Rob = true;
                        Debug.Log("—d¸‚ªƒ}ƒi‚ğ’D‚Á‚½");
                        if(Plants >= 2)
                        {
                            Create = true;
                            Debug.Log("—d¸‚ªƒ}ƒi‚Ì¶Y‚É¬Œ÷‚µ‚½");
                        }
                        GrowS = false;
                        Randomu3();
                        break;
                }
            }
        }
    }

    ////A•¨‚Ì”ƒ`ƒFƒbƒN
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        Hitr = null;

        // À•W‚ğlÌŒÜ“ü‚Å®”‚É(‹ô”ŠÛ‚ß‚È‚Ì‚Å•s‹ï‡‹N‚±‚é‚©‚àH)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
        RaycastHit2D HitR = Physics2D.Raycast(CellPosition, new Vector3(2, 0, 1), 100);

        Debug.Log(1);

        if (HitR)
        {
            Hitr = HitR.transform.gameObject;
        }
        return HitR;
    }

    private RaycastHit2D CheckPlantL(float X, float Y)
    {
        Hitl = null;

        // À•W‚ğlÌŒÜ“ü‚Å®”‚É(‹ô”ŠÛ‚ß‚È‚Ì‚Å•s‹ï‡‹N‚±‚é‚©‚àH)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
        RaycastHit2D HitL = Physics2D.Raycast(CellPosition, new Vector3(-2, 0, 1), 100);

        Debug.Log(2);

        if(HitL)
        {
            Hitl = HitL.transform.gameObject;
        }

        return HitL;
    }

    void GetPlantsR()
    {
        Debug.Log("R");
        //A•¨ŒŸ’m
        if (base.MyGrowth == GrowthState.Planted && Hitr.GetComponent<Garden>().IsPlanted != false)
        {
            Plants++;
            Debug.Log(Plants);
        }
    }

    void GetPlantsL()
    {
        Debug.Log("L");
        if (base.MyGrowth == GrowthState.Planted && Hitl.GetComponent<Garden>().IsPlanted != false)
        {
            Plants++;
            Debug.Log(Plants);
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


//
//RL = Random.Range(1, 3);
//switch (RL)
//{
//    case 1:
//        Create = true;
//        Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
//        GrowStart = false;
//        Randomu3();
//        break;
//    case 2:
//        Create = true;
//        Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
//        GrowStart = false;
//        Randomu3();
//        break;
//}