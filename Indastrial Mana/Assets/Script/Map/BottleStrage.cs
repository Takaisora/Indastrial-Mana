using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStrage : SingletonMonoBehaviour<BottleStrage>
{

    [SerializeField]
    private GameObject _Bottle = null;
    [SerializeField, Header("ボトル生成数の上限（上限を超えた場合は古いボトルから消える）")]
    private int _MaxBottle = 5;
    private GameObject _MapCanvas = null;
    private List<GameObject> _BottleList = new List<GameObject>();
    private int _BottleCount = 0;

    private void Start()
    {
        _MapCanvas = GameObject.Find("MapCanvas");

        for (int i = 0; i < _MaxBottle; i++)
        {
            GameObject MyBottle = Instantiate(_Bottle, new Vector3(999, 999), Quaternion.identity);
            MyBottle.name = "Bottle_" + i.ToString();
            MyBottle.transform.parent = _MapCanvas.transform;
            _BottleList.Add(MyBottle);
        }
    }

    public void GetBottle()
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