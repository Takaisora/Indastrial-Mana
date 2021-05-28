using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    public int Money = 0;//お金

    public int RiquiredMoney = 0;//クリアに必要なお金

    [SerializeField]
    public int ManaBottle = 0;//マナボトル

    public float DayTime = 0;//一日分の時間

    public bool Start_Flag = false;//一日の始終

    public bool Result_Flag = false;//クリア判定


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
