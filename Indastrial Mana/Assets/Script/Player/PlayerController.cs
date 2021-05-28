using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator _Animator;
    [SerializeField, Header("移動の速さ")]
    float _MoveSpeed;//移動量の為の変数
    public static float MoveRatio = 1;// デバフなどに使用
    private float _DeltaMove;// MoveSpeed * MoveRatio * Time.DeltaTime;
    private float _MoveX;//左右移動の為の変数
    private float _MoveY;//前後移動の為の変数
    private Vector3 _PlayerScale;
    public GameObject CarryItem = null;
    public ToolState Tool = ToolState.None;

    // アイテム所持状態
    public enum ToolState : byte
    {
        None,
        Shovel,
        Bucket,
        Seed,
        Bottle
    }

    // プレイヤーのエリア判定
    public enum AreaState : byte
    {
        None,
        Garden,
        WaterStrage,
        FertStrage,
        StudyArea,
        DeliveryArea,
        BottoleArea
    }


    private void Start()
    {
        MoveRatio = 1;
        _PlayerScale = transform.localScale;
    }

    void Update()
    {
        #region 移動処理
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

        transform.Translate(_MoveX, _MoveY, 0);
        #endregion


        #region アニメーション
        if (_MoveX != 0 || _MoveY != 0)
        {
            _Animator.SetInteger("PlayerState", 1);
        }
        else
        {
            _Animator.SetInteger("PlayerState", 0);
        }
        #endregion

        #region アクションボタン
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // アイテムを持っていないなら
            if (Tool == ToolState.None)
            {
                GetItem();
            }
            else
                RemoveItem();
        }
        #endregion

        // 種を植える処理
        if(Tool == ToolState.Seed)
        {
            RaycastHit2D hit = CheckCell();
            if(hit.collider != null && hit.collider.gameObject.tag == "Garden")
            {
                GameObject Garden = hit.collider.gameObject;
                Garden g = Garden.GetComponent<Garden>();
                if (!g.IsPlanted)
                {
                    Vector3 SetPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                , Mathf.RoundToInt(transform.position.y));
                    CarryItem.transform.position = SetPosition;
                    CarryItem.GetComponent<PlantBase>().Plant(g.WaterGauge, g.FertGauge);
                    CarryItem = null;
                    Tool = ToolState.None;
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーのマスに何があるかを調べます
    /// </summary>
    /// <returns>コライダー情報</returns>
    private RaycastHit2D CheckCell()
    {
        // 座標を四捨五入で整数に(偶数丸めなので不具合起こるかも？)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                         , Mathf.RoundToInt(transform.position.y));
        //Debug.Log(CellPosition);

        // 足元に何があるかを判別
        RaycastHit2D hit = Physics2D.Raycast(CellPosition, new Vector3(0, 0, 1), 100);

        return hit;
    }

    /// <summary>
    /// プレイヤーのマスにアイテムがあれば拾います
    /// </summary>
    private void GetItem()
    {
        RaycastHit2D hit = CheckCell();

        if (hit.collider != null && hit.collider.gameObject.tag == "Item")
        {
            CarryItem = hit.collider.gameObject;
            CarryItem.transform.position = new Vector3(999, 999);
            string ItemName = CarryItem.gameObject.name;
            // オブジェクトが複製された場合
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
    /// プレイヤーのマスにアイテムを置きます
    /// </summary>
    private void RemoveItem()
    {
        RaycastHit2D hit = CheckCell();
        Vector3 SetPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                        , Mathf.RoundToInt(transform.position.y));

        // 何もない場所にのみ置ける（修正予定）
        if (hit.collider == null)
        {
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
        }
        // 自動で横に置く処理を書く
    }
}