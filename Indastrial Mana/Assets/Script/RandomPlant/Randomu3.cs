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

    //�A���̐��J�E���g�p
    protected int Plants = 0;
    //���Y�ƒD������
    int RobCreate = 0;

    bool Fairy = false;     //�d���g���K�[
    private float _FTimeCount = 0;        //����


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

        if (Fairy)
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
                        Debug.Log("�d�����}�i�𐶎Y���܂���");
                        break;

                    case 2: //�D��
                            //Gardens.MyPlants.GetComponent<PlantBase>()._GeneratedCount--;
                        Debug.Log("�}�i���D��ꂽ");
                        if (Plants >= 2)
                        {
                            Debug.Log("�d�����}�i�𐶎Y���܂���");
                            Count = true;
                        }
                        break;
                }
            }
        }
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ���̂��g���K�[�ɐڐG���Ƃ��A�P�x�����Ă΂��
        //�ڐG�����̂̓K�[�f������������s
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //�A�����m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted)
            {
                Fairy = true;
            }
        }

    }

    //�A���̐����J�E���g
    int CheckAreaPlant()
    {
        foreach(Transform child in transform)
        {
            Plants ++;
        }
        return Plants;
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
