using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Madnesslv5 : MonoBehaviour
{
    private int MadnessTime = 0;
    private int positionX = 0;
    private int positionY = 0;
    private float MadTime = 0;
    private float MadDelayTime = 0;
    private int Cra5Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Cra5Count == 0)
        {
            positionX = Random.Range(1, 13);
            positionY = Random.Range(1, 10);
            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
            this.transform.Translate(SetPostion);
            MadTime += Time.deltaTime;
            if (MadTime > 14)
            {
                Cra5Count++;
                MadTime = 0;
            }
        }
        else
        {*/
            MadDelayTime += Time.deltaTime;
            if(MadDelayTime >= 3)
            {
                positionX = Random.Range(1, 13);
                positionY = Random.Range(1, 10);
                Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
                this.transform.Translate(SetPostion);
                MadTime += Time.deltaTime;
                if (MadTime > 14)
                {
                    Cra5Count++;
                    MadTime = 0;
                    MadDelayTime = 0;
                    if (Cra5Count == 5)
                    {
                        Destroy(this.gameObject);
                        Cra5Count = 0;
                    }
                }

            }
        //}
    }
}
