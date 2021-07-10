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
    //�d���g���K�[
    bool Fairy = false;
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
        

        //�d��
        if (GrowS)
        {
            _FTimeCount += Time.deltaTime;
            //�d���Ăяo��
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                Fairy = false;
                //���Y�A�D������
                RobCreate = Random.Range(1, 2);
                switch (RobCreate)
                {
                    case 1: //���Y
                        Create = true;
                        Debug.Log("�d�����}�i�̐��Y�ɐ�������");
                        GrowS = false;
                        Randomu3();
                        break;
                    case 2: //�͂炵�đ��₷
                        Rob = true;
                        Debug.Log("�d�����}�i��D����");
                        if(Plants >= 2)
                        {
                            Create = true;
                            Debug.Log("�d�����}�i�̐��Y�ɐ�������");
                        }
                        GrowS = false;
                        Randomu3();
                        break;
                }
            }
        }
    }

    ////�A���̐��`�F�b�N
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        Hitr = null;

        // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
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

        // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
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
        //�A�����m
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

                if (PlayerController.Tool == PlayerController.ToolState.Bucket && Bucket.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.Shovel && Shovel.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.Bottle)// �{�g�����󂩂͊֐��Ŕ��f
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
//        Debug.Log("�d�����}�i�𐶎Y���܂���");
//        GrowStart = false;
//        Randomu3();
//        break;
//    case 2:
//        Create = true;
//        Debug.Log("�d�����}�i�𐶎Y���܂���");
//        GrowStart = false;
//        Randomu3();
//        break;
//}