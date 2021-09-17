using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Madnesslv6 : Study
{
    int positionX = 0;
    int positionY = 0;
    float MadTime = 0;
    [SerializeField] GameObject Mad;
    Canvas parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        parentCanvas.transform.parent = UIManager.Instace.GetParentCanvas.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Madnesslv6)
        {
            positionX = Random.Range(1, 12);
            positionY = Random.Range(1, 9);
            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
            var temp = Instantiate(Mad, SetPostion, Quaternion.identity);
            temp.transform.parent = parentCanvas.gameObject.transform;
            MadTime += Time.deltaTime;
            if (MadTime >= 14)
            {
                MadTime = 0;
                Madnesslv6 = false;
                Destroy(this.gameObject);
            }
        }
    }
}
