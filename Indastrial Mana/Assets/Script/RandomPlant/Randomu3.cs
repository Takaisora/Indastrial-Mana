using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("�d���o���J�E���g")]
    int _FairyTime = 0;
    //�t�F�A���[�ɂ��}�i���Y�ւ̉e��
    [SerializeField, Header("���D��")]
    float Robbery;
    [SerializeField, Header("�d�����Y��")]
    float production;

    //�e���͈�
    [SerializeField, Header("�d���s���͈�")]
    float FArea = 5;

    //�A���̐��J�E���g�p
    protected int Plants = 0;
    //���Y�ƒD������
    int RobCreate = 0;
    //����
    private float _FTimeCount = 0;

    //���E����
    int RL = 0;

    //�Ԓd�̍��W�i�[�p
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

        //�d��
        if (GrowS)
        {
            _FTimeCount += Time.deltaTime;
            //�d���Ăяo��
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                //���Y�A�D������
                RobCreate = Random.Range(1, 3);
                switch (RobCreate)
                {
                    case 1: //���Y
                        Create = true;
                        Debug.Log("�d�����}�i�̐��Y�ɐ�������");
                        GrowS = false;
                        Randomu3();
                        break;
                    case 2: //�͂炵�đ��₷
                            RL = Random.Range(1, 3);
                        switch (RL)
                        {
                            case 1:
                                Debug.Log("�d�����}�i�𐶎Y���܂���");
                                GetPlantsR();
                                GrowS = false;
                                break;
                            case 2:
                                Debug.Log("�d�����}�i�𐶎Y���܂���");
                                GetPlantsL();
                                GrowS = false;
                                break;
                        }
                        break;
                }
            }
        }
    }

    ////�A���̐��`�F�b�N
    private RaycastHit2D CheckPlantR(float X, float Y)
    {
        //���g�����O
        Physics2D.queriesStartInColliders = false;

        Hitr = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
        RaycastHit2D HitR = Physics2D.Raycast(CellPosition, new Vector3(2, 0, 0), 100);

        if (HitR)
        {
            Hitr = HitR.transform.gameObject;
        }
        return HitR;
    }

    private RaycastHit2D CheckPlantL(float X, float Y)
    {
        //���g�����O
        Physics2D.queriesStartInColliders = false;

        Hitl = null;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
        RaycastHit2D HitL = Physics2D.Raycast(CellPosition, new Vector3(-2, 0, 1), 100);

        if (HitL)
        {
            Hitl = HitL.transform.gameObject;
        }
        return HitL;
    }

    //�E
    void GetPlantsR()
    {
        RaycastHit2D HitR = CheckPlantR(transform.position.x, transform.position.y);
        //�A�����m
        if (HitR.collider != null && HitR.collider.gameObject.CompareTag("Untagged"))
        {

            Create = true;
            this.Randomu3();
            HitR.collider.gameObject.GetComponent<PlantBase>().Withered();
        }
    }

    //��
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

    //���n
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
                else if (PlayerController.Tool == PlayerController.ToolState.BottleEmpty)// �{�g�����󂩂͊֐��Ŕ��f
                    base.Harvest();
            }
        }
    }
}