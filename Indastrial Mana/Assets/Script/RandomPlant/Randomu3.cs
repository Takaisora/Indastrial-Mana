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

        //�d��
        if (Fairy)
        {
            _FTimeCount += Time.deltaTime;
            //�d���Ăяo��
            if (_FTimeCount >= _FairyTime)
            {
                _FTimeCount = 0;
                Fairy = false;
                //���Y�A�D������
                RobCreate = Random.Range(2, 3);
                switch (RobCreate)
                {
                    case 1: //���Y
                        Create = true;
                        Debug.Log("�d�����}�i�𐶎Y���܂���");
                        break;
                    case 2: //�D��
                        Rob = true;
                        Debug.Log("�}�i��D��");
                        Debug.Log("�͈͓��̐A����" + Plants);
                        if (Plants == 1)
                        {
                            Create = true;
                            Debug.Log("�d�����}�i�𐶎Y���܂���");
                        }
                        break;
                }
            }
        }
    }

    //�A���̐��`�F�b�N
    private RaycastHit2D CheckPlant(float X, float Y)
    {
        // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        //�A�����邩�Ȃ�������
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 0, 1), 100);

        return Hit;
    }

    void GetPlants()
    {
        //RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

            //�A�����m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Plants++;
            }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ���̂��g���K�[�ɐڐG���Ƃ��A�P�x�����Ă΂��
        //�ڐG�����̂̓K�[�f������������s
        if (collision.gameObject.CompareTag("Garden"))
        {
            Gardens = collision.gameObject.GetComponent<Garden>();
            //�A�����m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
            }
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


//�D��
//�A���}�i���Y��-0.5
//�͈͓��̐A����1�{�Ȃ�
//+0.5
//�͈͓��̐A����2�{�Ȃ�
//+1

//����
////�A���̐����J�E���g
//Plants++;
//Debug.Log("�͈͓��̐A���̐���" + Plants + "�ł�");

//                Check("Untagged");
////��
//void Check(string tagname)
//{
//    tagObjects = GameObject.FindGameObjectsWithTag(tagname);
//    Debug.Log(tagObjects.Length);
//}


//RaycastHit2D CheckPlant(float X, float Y)
//{
//    // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
//    Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
//                                     , Mathf.RoundToInt(Y));
//    //�A�����邩�Ȃ�������
//    RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(5, 5, 1), 100);

//    return Hit;
//}
//void GetPlants()
//{
//    RaycastHit2D Hit = CheckPlant(transform.position.x, transform.position.y);

//    if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
//    {
//        Garden Gardens = gameObject.GetComponent<Garden>();
//        //�A�����m
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
//    Debug.Log("�q�b�g" + hit.collider.tag);
//    Plants++;
//}