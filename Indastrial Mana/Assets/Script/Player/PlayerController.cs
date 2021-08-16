using UnityEditor;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField]
    private Animator _Animator = null;
    [SerializeField, Header("初期資金")]
    public static ushort Money = 500;// (日数またいで引き継ぐ)
    [SerializeField, Header("移動の速さ")]
    private float _MoveSpeed = 0;//移動量の為の変数
    public static float MoveRatio = 1;// デバフなどに使用
    public GameObject CarryItem = null;// 今持ち運んでいるアイテム
    public ToolState Tool = ToolState.None;// 今持ち運んでいるアイテムの種類を表す状態
    private float _DeltaMove = 0;// MoveSpeed * MoveRatio * Time.DeltaTime;
    private float _MoveX = 0;//左右移動の為の変数
    private float _MoveY = 0;//前後移動の為の変数
    private Vector3 _PlayerScale = Vector3.zero;
    private Rigidbody2D rb = null;

    public Joystick joystick;

    public enum ToolState : byte
    {
        None,
        ShovelEmpty,
        ShovelFilled,
        BucketEmpty,
        BucketFilled,
        BottleEmpty,
        BottleFilled,
        Seed,
    }

    private void Start()
    {
        MoveRatio = 1;
        _PlayerScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        #region 移動処理
#if UNITY_EDITOR
        _MoveSpeed = 5;
#endif
        _MoveX = 0;
        _MoveY = 0;
        _DeltaMove = _MoveSpeed * MoveRatio * Time.deltaTime;

        /*
        if (Input.GetKey(KeyCode.A))
        {
            _MoveX = -_DeltaMove;
            transform.localScale = new Vector3(_PlayerScale.x, _PlayerScale.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _MoveX = _DeltaMove;
            transform.localScale = new Vector3(-_PlayerScale.x, _PlayerScale.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _MoveY = _DeltaMove;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _MoveY = -_DeltaMove;
        }
        */


        //JoyStick

        Vector3 MoveVecter = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        if(MoveVecter != Vector3.zero)
        {
            transform.Translate(MoveVecter * _DeltaMove, Space.World);
        }

        if(joystick.Horizontal <= 0)
        {
            transform.localScale = new Vector3(_PlayerScale.x, _PlayerScale.y);
        }
        else
        {
            transform.localScale = new Vector3(-_PlayerScale.x, _PlayerScale.y);
        }

        #endregion

        #region アニメーション
        int AddState = 0;
        if (_MoveX != 0 || _MoveY != 0 || MoveVecter != Vector3.zero)
            AddState = 1;// 移動中なら1
        // アニメーションをブランチなしで制御したい
        _Animator.SetInteger("PlayerState", (int)Tool + AddState);

        //if (_MoveX != 0 || _MoveY != 0 || MoveVecter != Vector3.zero)
        //{
        //    if (Tool == ToolState.None)
        //        _Animator.SetInteger("PlayerState", i);
        //    else if (Tool == ToolState.ShovelEmpty && Shovel.IsFertFilled == true)
        //        _Animator.SetInteger("PlayerState", 11);
        //    else if (Tool == ToolState.BucketEmpty && Bucket.IsWaterFilled == true)
        //        _Animator.SetInteger("PlayerState", 13);
        //    else if (Tool == ToolState.BottleFilled && CarryItem.GetComponent<Bottle>().IsManaFilled == true)
        //        _Animator.SetInteger("PlayerState", 15);
        //    else if (Tool == ToolState.ShovelEmpty)
        //        _Animator.SetInteger("PlayerState", 3);
        //    else if (Tool == ToolState.BucketEmpty)
        //        _Animator.SetInteger("PlayerState", 5);
        //    else if (Tool == ToolState.BottleFilled)
        //        _Animator.SetInteger("PlayerState", 7);
        //    else if (Tool == ToolState.Seed)
        //        _Animator.SetInteger("PlayerState", 9);
            
        //}
        //else
        //{
        //    if (Tool == ToolState.None)
        //        _Animator.SetInteger("PlayerState", 0);
        //    else if (Tool == ToolState.ShovelEmpty && Shovel.IsFertFilled == true)
        //        _Animator.SetInteger("PlayerState", 10);
        //    else if (Tool == ToolState.BucketEmpty && Bucket.IsWaterFilled == true)
        //        _Animator.SetInteger("PlayerState", 12);
        //    else if (Tool == ToolState.BottleFilled && CarryItem.GetComponent<Bottle>().IsManaFilled == true)
        //        _Animator.SetInteger("PlayerState", 14);
        //    else if (Tool == ToolState.ShovelEmpty)
        //        _Animator.SetInteger("PlayerState", 2);
        //    else if (Tool == ToolState.BucketEmpty)
        //        _Animator.SetInteger("PlayerState", 4);
        //    else if (Tool == ToolState.BottleFilled)
        //        _Animator.SetInteger("PlayerState", 6);
        //    else if (Tool == ToolState.Seed)
        //        _Animator.SetInteger("PlayerState", 8);
        //}
        #endregion

        #region 種を植える処理
        if (Tool == ToolState.Seed)
        {
            RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);
            if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Garden"))
            {
                GameObject Garden = Hit.collider.gameObject;
                Garden Gardens = Garden.GetComponent<Garden>();
                if (!Gardens.IsPlanted)
                {
                    Vector3 SetPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                , Mathf.RoundToInt(transform.position.y));
                    // 植物をその場に植える
                    CarryItem.transform.position = SetPosition;
                    // ゲージ起動
                    CarryItem.GetComponent<PlantBase>().Plant(Gardens.WaterGauge, Gardens.FertGauge);
                    CarryItem = null;
                    Tool = ToolState.None;
                    Tutorial_Text.Planted = true;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 指定のマスに何があるかを調べます
    /// </summary>
    /// <param name="X">調べたいx座標</param>
    /// <param name="Y">調べたいy座標</param>
    /// <returns></returns>
    private RaycastHit2D CheckCell(float X, float Y)
    {
        // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        // 足元に何があるかを判別
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(0, 0, 1), 100);

        return Hit;
    }

    /// <summary>
    /// プレイヤーのマスにアイテムがあれば拾います
    /// </summary>
    public void ActionBotton()
    {
        RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);
        // アイテムを持っていないなら
        if (Tool == ToolState.None)
        {
            if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Study"))
            {
                Study.Instance.Studying();
                Tutorial_Text.Stady = true;
            }
            else if(Hit.collider != null && Hit.collider.gameObject.CompareTag("BottleStrage"))
            {
                BottleStrage.Instance.GetBottle();
            }
            else
                GetItem();
        }
        else
            RemoveItem(transform.position.x, transform.position.y);
    }

    private void GetItem()
    {
        RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);

        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Item"))
        {
            CarryItem = Hit.collider.gameObject;
            CarryItem.transform.position = new Vector3(999, 999);
            string ItemName = CarryItem.gameObject.name;
            // アイテムが複製された場合用
            string[] ItemType = ItemName.Split('_');

            switch (ItemType[0])
            {
                case "Bucket":
                    Tool = ToolState.BucketEmpty;
                    Debug.Log("バケツを持った");
                    break;
                case "Shovel":
                    Tool = ToolState.ShovelEmpty;
                    Debug.Log("シャベルを持った");
                    break;
                case "Seed":
                    Tool = ToolState.Seed;
                    Debug.Log("種を持った");
                    break;
                case "Bottle":
                    Tool = ToolState.BottleFilled;
                    Debug.Log("ビンを持った");
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// プレイヤーのマスかその周囲4マスにホールド中のアイテムを置きます
    /// </summary>
    private void RemoveItem(float PlayerPosX, float PlayerPosY)
    {
        bool Still = true;// 自身のマスにアイテムを置くか周囲4マスに空きがなければ処理を終わる
        byte CheckCount = 0;// 0...真ん中, 1...右,2...上, 3...左, 4...下
        // 置き場所指定用
        float X = PlayerPosX;
        float Y = PlayerPosY;

        do
        {
            RaycastHit2D Hit = CheckCell(X, Y);

            // 調べたマスにアイテムが置けないなら
            if (Hit.collider != null)
            {
                CheckCount++;
                // 次に調べるマスを指定
                switch (CheckCount)
                {
                    case 1:
                        X = PlayerPosX + 1;
                        Y = PlayerPosY;
                        break;
                    case 2:
                        X = PlayerPosX - 1;
                        Y = PlayerPosY;
                        break;
                    case 3:
                        X = PlayerPosX;
                        Y = PlayerPosY + 1;
                        break;
                    case 4:
                        X = PlayerPosX;
                        Y = PlayerPosY - 1;
                        break;
                    default:
                        Still = false;
                        break;
                }
            }
            // 何もなければアイテムを置く
            else
            {
                Vector3 SetPosition = new Vector3(Mathf.RoundToInt(X)
                                    , Mathf.RoundToInt(Y));
                CarryItem.transform.position = SetPosition;
                CarryItem = null;

                switch (Tool)
                {
                    case ToolState.BucketEmpty:
                        Debug.Log("バケツを置いた");
                        break;
                    case ToolState.ShovelEmpty:
                        Debug.Log("シャベルを置いた");
                        break;
                    case ToolState.Seed:
                        Debug.Log("種を置いた");
                        break;
                    case ToolState.BottleFilled:
                        Debug.Log("ビンを置いた");
                        break;
                    default:
                        break;
                }
                Tool = ToolState.None;
                Still = false;
            }
        } while (Still);
    }
}