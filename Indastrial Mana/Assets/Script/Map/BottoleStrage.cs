using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottoleStrage : MonoBehaviour
{
    [SerializeField]
    private GameObject _MapCanvas = null;
    [SerializeField]
    private GameObject _Bottle = null;
    [SerializeField]
    private int _MaxBottle = 5;
    private List<GameObject> _BottleList = new List<GameObject>();
    private int _BottleCount = 0;

    void Start()
    {
        for (int i = 0; i < _MaxBottle; i++)
        {
            GameObject MyBottle = Instantiate(_Bottle, new Vector3(999, 999), Quaternion.identity);
            MyBottle.name = "Bottle_" + i.ToString();
            MyBottle.transform.parent = _MapCanvas.transform;
            _BottleList.Add(MyBottle);
        }
    }

    // ボタンでビンを持つようにする
    private void OnTriggerStay2D(Collider2D collision)
    {
        // プレイヤーが何も持っていなければ
        if (collision.gameObject.tag == "Player" && PlayerController.Instance.Tool == PlayerController.ToolState.None)
        {
            _BottleList[_BottleCount].GetComponent<Bottle>().IsManaFilled = false;
            PlayerController.Instance.CarryItem = _BottleList[_BottleCount];
            PlayerController.Instance.CarryItem.transform.position = new Vector3(999, 999);
            _BottleCount++;
            if (_BottleCount > _MaxBottle - 1)
                _BottleCount = 0;

            PlayerController.Instance.Tool = PlayerController.ToolState.BottleEmpty;
            Debug.Log("ビンを手に入れた");
        }
    }
}
