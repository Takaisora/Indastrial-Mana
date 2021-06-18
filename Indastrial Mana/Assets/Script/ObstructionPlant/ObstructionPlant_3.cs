using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionPlant_3 : PlantBase
{

    private int _RondamType = 0;
    private int _Rondamplusx = 0;
    private int _Rondamplusy = 0;
    protected float HandOffx = 0;
    protected float HandOffy = 0;
    protected float HandOffz = 0;
    public static bool start = false;
    Vector3 DebuffArea;
    [SerializeField] GameObject Debuff;
    [SerializeField] GameObject MapCanvas;
    // Update is called once per frame
    private void Update()
    {
        if (base.MyGrowth == GrowthState.Planted)
            base.Growing();

        if (base.MyGrowth != GrowthState.Seed)
        {
            base.DepletionCheck();
            base.DrawGauge();
        }

        if (base.MyGrowth == GrowthState.Withered)
        {
            if (_IsCompleted)
            {
                base.Withered();
            }
            else if (!_IsCompleted)
            {
                for (int m = 0; m < UnityEngine.Random.Range(4, 6); m++)
                {
                    Vector3 PlantPlace = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
                    _RondamType = UnityEngine.Random.Range(2, 4);
                    _Rondamplusx = UnityEngine.Random.Range(0, 2);
                    _Rondamplusy = UnityEngine.Random.Range(0, 2);
                    if (_Rondamplusx == 0)
                    {
                        if(_Rondamplusy == 0)
                        {
                            DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y + _RondamType);
                        }
                        else if(_Rondamplusy == 1)
                        {
                            DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y - _RondamType);
                        }
                    }
                    else if(_Rondamplusx == 1)
                    {
                        if (_Rondamplusy == 0)
                        {
                            DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y + _RondamType);
                        }
                        else if (_Rondamplusy == 1)
                        {
                             DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y - _RondamType);
                        }
                    }
                    //Debuff.gameObject.SetActive(true);
                    //Debuff.transform.position = DebuffArea;
                    GameObject Test = (GameObject)Resources.Load("Debuff");
                    Debug.Log(DebuffArea);
                    Debug.Log(PlantPlace);
                    //HandOffx = DebuffArea.x;
                    //HandOffy = DebuffArea.y;
                    Instantiate(Test, DebuffArea,Quaternion.identity);
                    Test.transform.SetParent(MapCanvas.transform, true);
                    //DebuffAreaの座標を通ると狂気度が上がるようにする。DebuffArea2をDebuffAreaに移し替えて同じ作業を繰り返す。新しいデバフフィールドのプレハブを作る。
                    //植物の座標取得と2,3のランダム取得、プラスかマイナスかのランダム変数を用意する。(ランダム変数が計二つ)
                }
                Withered();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (base.MyGrowth == GrowthState.Seed)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            base.Player = collision.gameObject;
            Vector3 CellPosition = new Vector3(Mathf.RoundToInt(Player.transform.position.x)
                                 , Mathf.RoundToInt(Player.transform.position.y));

            if (CellPosition == this.transform.position)
            {
                PlayerController PlayerController = Player.GetComponent<PlayerController>();

                if (PlayerController.Tool == PlayerController.ToolState.Bucket && Bucket.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.Shovel && Shovel.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.Bottle)// ボトルが空かは関数で判断
                    base.Harvest();
            }
        }
    }
}