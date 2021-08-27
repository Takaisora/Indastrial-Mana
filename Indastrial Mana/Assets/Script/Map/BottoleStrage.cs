using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottoleStrage : MonoBehaviour
{
    [SerializeField]
    GameObject Player = null;
    [SerializeField]
    GameObject MapCanvas = null;
    [SerializeField]
    GameObject Bottle = null;
    [SerializeField]
    int MaxBottle = 5;
    private List<GameObject> _BottleList = new List<GameObject>();
    private int BottleCount = 0;

    void Start()
    {
        for(int i = 0; i < MaxBottle; i++)
        {
            GameObject MyBottle = Instantiate(Bottle, new Vector3(999, 999), Quaternion.identity);
            MyBottle.name = "Bottle_" + i.ToString();
            MyBottle.transform.parent = MapCanvas.transform;
            _BottleList.Add(MyBottle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが何も持っていなければ
        if (collision.gameObject.tag == "Player" && PlayerController.Instance.Tool == PlayerController.ToolState.None)
        {
            _BottleList[BottleCount].GetComponent<Bottle>().IsManaFilled = false;
            PlayerController.Instance.CarryItem = _BottleList[BottleCount];
            PlayerController.Instance.CarryItem.transform.position = new Vector3(999, 999);
            BottleCount++;
            if (BottleCount > MaxBottle - 1)
                BottleCount = 0;

            PlayerController.Instance.Tool = PlayerController.ToolState.BottleFilled;
            Debug.Log("ビンを手に入れた");
        }
    }
}
