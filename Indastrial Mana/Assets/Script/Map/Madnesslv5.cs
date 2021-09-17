using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instace { get => _instance; }
    static UIManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    public Canvas GetParentCanvas { get => parentCanvas; }
    [SerializeField] Canvas parentCanvas;
}

public class Madnesslv5 : Study
{
    int positionX = 0;
    int positionY = 0;
    float MadTime = 0;
    [SerializeField] GameObject Mad;
    Canvas parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //parentCanvas.transform.parent = GameObject.Find("").transform;
        parentCanvas.transform.parent = UIManager.Instace.GetParentCanvas.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Madnesslv5)
        {
            positionX = Random.Range(1, 13);
            positionY = Random.Range(1, 10);
            Vector3 SetPostion = new Vector3(Mathf.RoundToInt(positionX), Mathf.RoundToInt(positionY));
            var temp = Instantiate(Mad, SetPostion, Quaternion.identity);
            temp.transform.parent = parentCanvas.gameObject.transform;
            MadTime += Time.deltaTime;
            if (MadTime >= 14)
            {
                MadTime = 0;
                Madnesslv5 = false;
                Destroy(this.gameObject);
            }
        }
    }
}
