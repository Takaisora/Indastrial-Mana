using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Tutorial : MonoBehaviour
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

        if (DelayTime >= 3)
        {
            Start1();

            Invoke("Active", 3f);
        }
    }
    void Day1()
    {
        Day1Start.text = Day_1.Days + "Days";
    }
    void Start1()
    {
        Day1Start.text = "Start!";

        DayManager.GetComponent<Day_1>().SFlag();
    }

    void Active()
    {
        this.gameObject.SetActive(false);
    }

    public void ReStart()
    {
        DelayTime = 0;

        Day1();

        DayManager.GetComponent<Day_1>().ResultClose();
    }
}
