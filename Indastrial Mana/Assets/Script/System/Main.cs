using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    public int Money = 0;//����

    public int RiquiredMoney = 0;//�N���A�ɕK�v�Ȃ���

    [SerializeField]
    public int ManaBottle = 0;//�}�i�{�g��

    public float DayTime = 0;//������̎���

    public bool Start_Flag = false;//����̎n�I

    public bool Result_Flag = false;//�N���A����


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DayTime);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Start_Flag = !Start_Flag;
        //}

        //if (Start_Flag == true)
        //{
        //    DayStart();
        //}

        //if (DayTime >= 90f)
        //{
        //    DayEnd();

        //    Start_Flag = !Start_Flag;

        //    DayTime = 0;
        //}
    }
}
