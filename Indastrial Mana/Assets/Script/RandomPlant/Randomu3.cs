using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu3 : PlantBase
{
    [SerializeField, Header("�d���o���J�E���g")]
    float FairyTimeCount;

    //�t�F�A���[�ɂ��}�i���Y�ւ̉e��
    [SerializeField, Header("���D��")]
    float Robbery;

    //�A���̐��J�E���g�p
    protected int Plants = 0;
    //���Y�ƒD������
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

    //�A���̐����J�E���g
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
        // ���̂��g���K�[�Ɨ��ꂽ�Ƃ��A�P�x�����Ă΂��
        //�A��
        //�ڐG�����̂̓K�[�f������������s
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //�A�����m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                //�d���̏���
                FairyTimeCount += Time.deltaTime;
                if (FairyTimeCount <= 0)
                {
                   
                        RobCreate = Random.Range(1, 3);
                    switch (RobCreate)
                    {
                        case 1: //���Y
                            Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount++;
                            Debug.Log("�d�����}�i�𐶎Y���܂���");
                            break;

                        case 2: //�D��
                            //�A���̐����f
                            if (Plants == 1)   //1�Ȃ�
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            if (Plants == 2)    //2�Ȃ�
                            {
                                Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;

                            }
                            break;
                    }
                    
                }

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
