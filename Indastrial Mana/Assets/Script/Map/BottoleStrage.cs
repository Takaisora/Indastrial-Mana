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
    private PlayerController _PlayerController = null;
    private int BottleCount = 0;

    void Start()
    {
        _PlayerController = Player.GetComponent<PlayerController>();

        for (int i = 0; i < MaxBottle; i++)
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
        if (collision.gameObject.tag == "Player" && _PlayerController.Tool == PlayerController.ToolState.None)
        {
            _BottleList[BottleCount].GetComponent<Bottle>().IsManaFilled = false;
            _PlayerController.CarryItem = _BottleList[BottleCount];
            _PlayerController.CarryItem.transform.position = new Vector3(999, 999);
            BottleCount++;
            if (BottleCount > MaxBottle - 1)
                BottleCount = 0;

            _PlayerController.Tool = PlayerController.ToolState.Bottle;
            Debug.Log("ビンを手に入れた");
        }
    }
}
