using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day_1_Time : Main
{
    [SerializeField]
    public GameObject Day_Time;

    Day_1 script;

    public Text Day1Time;

    public float time = 0;

    public float LimitTime = 90;
    // Start is called before the first frame update
    void Start()
    {
        GameObject DayManager = GameObject.Find("DayManager");

        Day_1 script = DayManager.GetComponent<Day_1>();

        Text Day1Time = Day_Time.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Day1Time.text = "Žc‚è" + time;

        float DayT = script.DayTime;

        time = LimitTime - DayT;

        if(time <= 0)
        {
            Day1Time.text = "I—¹";
        }
    }
}
