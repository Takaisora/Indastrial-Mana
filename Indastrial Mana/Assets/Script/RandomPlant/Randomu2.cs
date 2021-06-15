using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomu2 : PlantBase
{
    [SerializeField, Header("�������x�̃o�t�p")]
    float GrowRatio;
    [SerializeField, Header("�������x�̃f�o�t�p")]
    float GrowRatioDecline;
    [SerializeField, Header("���@���g�����")]
    int MagicCount;
    [SerializeField, Header("���@���s��")]
    int MagicNumber;

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


    }

    // 2D�̏ꍇ�̃g���K�[����
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
                    //���I�A�񐔏���
                    ProductionSpeed = Random.Range(1, 3);
                    switch (ProductionSpeed)
                    {
                        case 1:
                            Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatio;
                        MagicCount++;
                        //����Z
                            break;
                        case 2:
                            Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                            //����Z
                            break;
                    }

                }
            }


     }


}
//���ʔ͈͂�ϐ��Őݒ�ł���悤�ɂ���i�V���A���C�Y���j(�D��x��)

        //�����_���H�Ńo�t���f�o�t���𔻒肷��

        //���ʎ��Ԃ�ϐ��Ő錾�i�V���A���C�Y���j�A���ԂɂȂ����猳�̃X�s�[�h�ɖ߂�(���@���󂯂鑤�Őݒ肵�������y����)

//���@�̎g�p�񐔏���ɂȂ�����͂��