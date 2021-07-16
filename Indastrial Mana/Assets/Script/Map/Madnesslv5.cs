using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madnesslv5 : Study
{
    int positionX = 0;
    int positionY = 0;
    float MadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Madnesslv5)
        {
            gameObject.SetActive(true);
            positionX = Random.Range(1, 12);
            positionY = Random.Range(1, 9);
            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
            transform.Translate(SetPostion);
            MadTime += Time.deltaTime;
            if (MadTime >= 14)
            {
                MadTime = 0;
                Madnesslv5 = false;
                gameObject.SetActive(false);
            }
        }
    }
}
