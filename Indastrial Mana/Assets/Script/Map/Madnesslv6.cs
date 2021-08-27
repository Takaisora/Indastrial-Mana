using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madnesslv6 : Study
{
    int positionX = 0;
    int positionY = 0;
    float MadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Madnesslv6)
        {
            positionX = Random.Range(1, 12);
            positionY = Random.Range(1, 9);
            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
            gameObject.SetActive(true);
            transform.Translate(SetPostion);
            MadTime += Time.deltaTime;
            if (MadTime >= 14)
            {
                MadTime = 0;
                Madnesslv6 = false;
                gameObject.SetActive(false);
            }
        }
    }
}
