using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu2 : PlantBase
{
    [SerializeField, Header("�������x�̃o�t�p")]
    float GrowRatio;
    [SerializeField, Header("�������x�̃f�o�t�p")]
    float GrowRatioDecline;
    //���@���g�����
    int MagicCount = 0;
    [SerializeField, Header("���@���s��")]
    int MagicNumber;

    //�o�t�J�n�܂�
    float _BTime = 0;
    //�o�t���|���鎞��
    float _BTimeEND = 0;
    //�o�t�J�n
    bool B = false;
    //�������x��
    bool Grow_R = false;
    //�������x��
    bool Grow_RD = false;

    //���@���I�p
    int ProductionSpeed = 0;

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

        if (GrowS)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(5, 5);
        }
        if (GrowS)
        {
            //�o�t���Ԋ֘A
            _BTime += Time.deltaTime;
            if (_BTime >= BuffTime)
            {
                B = true;
                _BTime = 0;
            }
        }
        if (Buff && Grow_R)
        {
            _BTimeEND += Time.deltaTime;
            //���@���ʑŏ���
            if (_BTimeEND >= GetBuff)
            {
                _BTimeEND = 0;
                MagicTime();
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //�ڐG�����̂̓K�[�f������������s
        //�o�t
        if (collision.gameObject.CompareTag("Garden"))
        {
            Garden Gardens = collision.gameObject.GetComponent<Garden>();
            //�A�����m
            if (Gardens.MyPlants != null && base.MyGrowth == GrowthState.Planted && MagicCount < MagicNumber && B)
            {
                //���I�A�񐔏���
                ProductionSpeed = Random.Range(1, 2);
                switch (ProductionSpeed)
                {
                    case 1:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatio;
                        MagicCount++;
                        Buff = true;
                        Grow_R = true;
                        Debug.Log("�������x�㏸");
                        //����Z
                        break;
                    case 2:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                        Buff = true;
                        Grow_RD = true;
                        Debug.Log("�������x�ቺ");
                        //����Z
                        break;
                }
            }
        }

        //���n

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

    //�����_���^2���@���ԁA����
    public void MagicTime()
    {
        //�������x����
        if (Grow_R && Buff)
        {
            GetComponent<Randomu2>().GrowSpeed = GrowSpeed / GrowRatio;
            Debug.Log("�������x���߂�܂���");
            Buff = false;
        }
        //�������x����
        if (Grow_RD && Buff)
        {
            GetComponent<Randomu2>().GrowSpeed = GrowSpeed / GrowRatioDecline;
            Debug.Log("�������x���߂�܂���");
            Buff = false;
        }
    }


}
//���ʔ͈͂�ϐ��Őݒ�ł���悤�ɂ���i�V���A���C�Y���j(�D��x��)

        //�����_���H�Ńo�t���f�o�t���𔻒肷��

        //���ʎ��Ԃ�ϐ��Ő錾�i�V���A���C�Y���j�A���ԂɂȂ����猳�̃X�s�[�h�ɖ߂�(���@���󂯂鑤�Őݒ肵�������y����)

//���@�̎g�p�񐔏���ɂȂ�����͂��