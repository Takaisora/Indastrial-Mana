using UnityEngine;

public class PlayerController : MonoBehaviour
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
    private Study _MyStudy = null;
    private Rigidbody2D rb = null;

    public Joystick joystick;

    public enum ToolState : byte
    {
        None,
        Shovel,
        Bucket,
        Seed,
        Bottle
    }

    private void Start()
    {
        MoveRatio = 1;
        _PlayerScale = transform.localScale;
        GameObject Study = GameObject.Find("StudyArea");
        _MyStudy = Study.GetComponent<Study>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #region 移動処理
#if UNITY_EDITOR
        _MoveSpeed = 5;
#endif
        _MoveX = 0;
        _MoveY = 0;
        _DeltaMove = _MoveSpeed * MoveRatio * Time.deltaTime;

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

        //JoyStick

        Vector3 MoveVecter = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        if(MoveVecter != Vector3.zero)
        {
            transform.Translate(MoveVecter * _MoveSpeed * Time.deltaTime, Space.World);
        }

        if(joystick.Horizontal <= 0)
        {
            transform.localScale = new Vector3(_PlayerScale.x, _PlayerScale.y);
        }
        else
        {
            transform.localScale = new Vector3(-_PlayerScale.x, _PlayerScale.y);
        }

#if UNITY_EDITOR
        transform.Translate(_MoveX, _MoveY, 0);// PCが重いのでこっち
#endif
#if UNITY_IOS
        Vector2 move = new Vector2(_MoveX, _MoveY);// 最終的にはこっち
        rb.velocity = move;
#endif
        #endregion

        #region アニメーション
        if (_MoveX != 0 || _MoveY != 0 || MoveVecter != Vector3.zero)
        {
            //if (Tool == ToolState.None)
            _Animator.SetInteger("PlayerState", 1);
            //else 
        }
        else
        {
            _Animator.SetInteger("PlayerState", 0);
        }

        //if (_MoveX != 0 || _MoveY != 0|| MoveVecter != Vector3.zero)
        //{
        //    _Animator.SetInteger("PlayerState", 3);
        //}
        //else
        //{
        //    _Animator.SetInteger("PlayerState", 2);
        //}
        #endregion

        #region アクションボタン
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);
        //    // アイテムを持っていないなら
        //    if (Tool == ToolState.None)
        //    {
        //        if (Hit.collider != null && Hit.collider.gameObject.CompareTag("Study"))
        //            _MyStudy.Studying();
        //        else
        //            GetItem();
        //    }
        //    else
        //        RemoveItem(transform.position.x, transform.position.y);
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
                _MyStudy.Studying();
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
                    Tool = ToolState.Bucket;
                    Debug.Log("バケツを持った");
                    break;
                case "Shovel":
                    Tool = ToolState.Shovel;
                    Debug.Log("シャベルを持った");
                    break;
                case "Seed":
                    Tool = ToolState.Seed;
                    Debug.Log("種を持った");
                    break;
                case "Bottle":
                    Tool = ToolState.Bottle;
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
                    case ToolState.Bucket:
                        Debug.Log("バケツを置いた");
                        break;
                    case ToolState.Shovel:
                        Debug.Log("シャベルを置いた");
                        break;
                    case ToolState.Seed:
                        Debug.Log("種を置いた");
                        break;
                    case ToolState.Bottle:
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