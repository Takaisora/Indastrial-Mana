using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random2Ar : Randomu2
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
                        Buff = true;
                        Debug.Log("�������x�㏸");
                        MagicTime();
                        //����Z
                        break;
                    case 2:
                        Gardens.MyPlants.GetComponent<PlantBase>().GrowSpeed = GrowRatioDecline;
                        MagicCount++;
                        Buff = true;
                        Debug.Log("�������x�ቺ");
                        MagicTime();
                        //����Z
                        break;
                }

            }
        }
    }

}
