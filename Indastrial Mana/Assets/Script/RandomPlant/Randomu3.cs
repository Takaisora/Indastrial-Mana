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

        //—d¸
        if (GrowS)
        {
            _FTimeCount += Time.deltaTime;
            //—d¸ŒÄ‚Ño‚µ
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                //¶YA’D‚¤ˆ—
                RobCreate = Random.Range(1, 3);
                switch (RobCreate)
                {
                    case 1: //¶Y
                        Create = true;
                        Debug.Log("—d¸‚ªƒ}ƒi‚Ì¶Y‚É¬Œ÷‚µ‚½");
                        GrowS = false;
                        Randomu3();
                        break;
                    case 2: //ŒÍ‚ç‚µ‚Ä‘‚â‚·
                            RL = Random.Range(1, 3);
                        switch (RL)
                        {
                            case 1:
                                Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                                GetPlantsR();
                                GrowS = false;
                                break;
                            case 2:
                                Debug.Log("—d¸‚ªƒ}ƒi‚ğ¶Y‚µ‚Ü‚µ‚½");
                                GetPlantsL();
                                GrowS = false;
                                break;
                        }
                        break;
                }
            }
        }
    }

    ////A•¨‚Ì”ƒ`ƒFƒbƒN
    private RaycastHit2D CheckPlantR(float X, float Y)
    {
        //©g‚ğœŠO
        Physics2D.queriesStartInColliders = false;

        Hitr = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
        RaycastHit2D HitR = Physics2D.Raycast(CellPosition, new Vector3(2, 0, 0), 100);

        if (HitR)
        {
            Hitr = HitR.transform.gameObject;
        }
        return HitR;
    }

    private RaycastHit2D CheckPlantL(float X, float Y)
    {
        //©g‚ğœŠO
        Physics2D.queriesStartInColliders = false;

        Hitl = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //A•¨‚ ‚é‚©‚È‚¢‚©”»’è
        RaycastHit2D HitL = Physics2D.Raycast(CellPosition, new Vector3(-2, 0, 1), 100);

        if (HitL)
        {
            Hitl = HitL.transform.gameObject;
        }
        return HitL;
    }

    //‰E
    void GetPlantsR()
    {
        RaycastHit2D HitR = CheckPlantR(transform.position.x, transform.position.y);
        //A•¨ŒŸ’m
        if (HitR.collider != null && HitR.collider.gameObject.CompareTag("Untagged"))
        {

            Create = true;
            this.Randomu3();
            HitR.collider.gameObject.GetComponent<PlantBase>().Withered();
        }
    }

    //¶
    void GetPlantsL()
    {
        RaycastHit2D HitL = CheckPlantL(transform.position.x, transform.position.y);

        if (HitL.collider != null && HitL.collider.gameObject.CompareTag("Untagged"))
        {
            Create = true;
            this.Randomu3();
            HitL.collider.gameObject.GetComponent<PlantBase>().Withered();
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

                if (PlayerController.Tool == PlayerController.ToolState.BucketFilled && Bucket.Instance.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.ShovelFilled && Shovel.Instance.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.BottleEmpty)// ƒ{ƒgƒ‹‚ª‹ó‚©‚ÍŠÖ”‚Å”»’f
                    base.Harvest();
            }
        }
    }
}