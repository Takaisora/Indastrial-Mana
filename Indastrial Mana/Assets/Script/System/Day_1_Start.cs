using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day_1_Start : MonoBehaviour
{
    [SerializeField]
    public GameObject Day_Start;

    [SerializeField]
    public GameObject DayManager;

    Day_1 script;

    public Text Day1Start;

    float DelayTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Day_1 script = DayManager.GetComponent<Day_1>(); 
        
        Text Day1Start = Day_Start.GetComponent<Text>();

        Day1();
    }

    // Update is called once per frame
    void Update()
    {
        DelayTime += Time.deltaTime;

        Destroy(this.gameObject, 5.0f);

        if (DelayTime >= 3)
        {
            Start1();
        }
    }
    void Day1()
    {
        Day1Start.text = "1“ú–Ú";
    }
    void Start1()
    {
        Day1Start.text = "Start!";

        DayManager.GetComponent<Day_1>().SFlag();
    }
}
