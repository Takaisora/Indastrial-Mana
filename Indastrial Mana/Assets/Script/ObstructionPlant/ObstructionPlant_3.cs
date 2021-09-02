using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionPlant_3 : PlantBase
{
    private Animator animator;
    private const string _Grow = "Grow";
    private const string _Generat = "Generat";

    private int _RondamType = 0;
    private int _RondamType2 = 0;
    private int _Rondamplusx = 0;
    private int _Rondamplusy = 0;
    private int n = 0;
    protected float HandOffx = 0;
    protected float HandOffy = 0;
    protected float HandOffz = 0;
    public static bool start = false;
    private bool Judge = false;
    Vector3 DebuffArea;
    [SerializeField] GameObject Debuff;
    [SerializeField] GameObject MapCanvas;
   // [SerializeField] Canvas parentCanvas;
   // [SerializeField] GameObject Test;
    List<Vector3> _RondamList = new List<Vector3>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (base.MyGrowth == GrowthState.Planted)
        {
            base.Growing();
            animator.SetBool(_Grow, true);
        }
        if (base.MyGrowth != GrowthState.Seed)
        {
            base.DepletionCheck();
            base.DrawGauge();
        }
        if (base.MyGrowth == GrowthState.Generated)
            animator.SetBool(_Generat, true);
        else
            animator.SetBool(_Generat, false);


        if (base.MyGrowth == GrowthState.Withered)
        {
            if (_IsCompleted)
            {
                SoundManager.Instance.WitherSound();
                base.Withered();
            }
            else if (!_IsCompleted)
            {
                int noft = UnityEngine.Random.Range(4, 6);
                for (int m = 0; m < noft; m++)
                {
                    Vector3 PlantPlace = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));

                    _RondamType = UnityEngine.Random.Range(0, 3);
                    _RondamType2 = UnityEngine.Random.Range(0, 3);
                    _Rondamplusx = UnityEngine.Random.Range(0, 2);
                    _Rondamplusy = UnityEngine.Random.Range(0, 2);
                    if (_Rondamplusx == 0)
                    {
                        if (_Rondamplusy == 0)
                        {
                            DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y + _RondamType2);
                        }
                        else if (_Rondamplusy == 1)
                        {
                            DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y - _RondamType2);
                        }
                    }
                    else if (_Rondamplusx == 1)
                    {
                        if (_Rondamplusy == 0)
                        {
                            DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y + _RondamType2);
                        }
                        else if (_Rondamplusy == 1)
                        {
                            DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y - _RondamType2);
                        }
                    }
                    _RondamList.Add(DebuffArea);
                }
                for(int a = 0; a< noft; a++)
                {
                    for (int b = 0; b< noft; b++)
                    {
                        if (a != b)
                        {
                            if (_RondamList[a] == _RondamList[b])
                            {
                                Vector3 PlantPlace = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));

                                _RondamType = UnityEngine.Random.Range(0, 3);
                                _RondamType2 = UnityEngine.Random.Range(0, 3);
                                _Rondamplusx = UnityEngine.Random.Range(0, 2);
                                _Rondamplusy = UnityEngine.Random.Range(0, 2);
                                if (_Rondamplusx == 0)
                                {
                                    if (_Rondamplusy == 0)
                                    {
                                        DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y + _RondamType2);
                                    }
                                    else if (_Rondamplusy == 1)
                                    {
                                        DebuffArea = new Vector3(PlantPlace.x + _RondamType, PlantPlace.y - _RondamType2);
                                    }
                                }
                                else if (_Rondamplusx == 1)
                                {
                                    if (_Rondamplusy == 0)
                                    {
                                        DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y + _RondamType2);
                                    }
                                    else if (_Rondamplusy == 1)
                                    {
                                        DebuffArea = new Vector3(PlantPlace.x - _RondamType, PlantPlace.y - _RondamType2);
                                    }
                                }
                                _RondamList[b] = DebuffArea;
                            }
                        }
                    }
                }
                for(int i = 0; i < noft; i++)
                {
                    var temp = Instantiate(Debuff, _RondamList[i], Quaternion.identity);
                    // var t = temp.GetComponent<DebuffField>();
                    MapCanvas = GameObject.Find("MapCanvas");
                    temp.transform.parent = MapCanvas.transform;
                }
                SoundManager.Instance.WitherSound();
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

                if (PlayerController.Tool == PlayerController.ToolState.BucketFilled && Bucket.Instance.IsWaterFilled)
                    base.Watering();
                else if (PlayerController.Tool == PlayerController.ToolState.ShovelFilled && Shovel.Instance.IsFertFilled)
                    base.Fertilizing();
                else if (PlayerController.Tool == PlayerController.ToolState.BottleEmpty)// É{ÉgÉãÇ™ãÛÇ©ÇÕä÷êîÇ≈îªíf
                    base.Harvest();
            }
        }
    }
}