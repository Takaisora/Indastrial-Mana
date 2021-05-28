using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator _Animator;
    [SerializeField, Header("�ړ��̑���")]
    float _MoveSpeed;//�ړ��ʂׂ̈̕ϐ�
    public static float MoveRatio = 1;// �f�o�t�ȂǂɎg�p
    private float _DeltaMove;// MoveSpeed * MoveRatio * Time.DeltaTime;
    private float _MoveX;//���E�ړ��ׂ̈̕ϐ�
    private float _MoveY;//�O��ړ��ׂ̈̕ϐ�
    private Vector3 _PlayerScale;
    public GameObject CarryItem = null;
    public ToolState Tool = ToolState.None;

    // �A�C�e���������
    public enum ToolState : byte
    {
        None,
        Shovel,
        Bucket,
        Seed,
        Bottle
    }

    // �v���C���[�̃G���A����
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
        #region �ړ�����
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


        #region �A�j���[�V����
        if (_MoveX != 0 || _MoveY != 0)
        {
            _Animator.SetInteger("PlayerState", 1);
        }
        else
        {
            _Animator.SetInteger("PlayerState", 0);
        }
        #endregion

        #region �A�N�V�����{�^��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �A�C�e���������Ă��Ȃ��Ȃ�
            if (Tool == ToolState.None)
            {
                GetItem();
            }
            else
                RemoveItem();
        }
        #endregion

        // ���A���鏈��
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
    /// �v���C���[�̃}�X�ɉ������邩�𒲂ׂ܂�
    /// </summary>
    /// <returns>�R���C�_�[���</returns>
    private RaycastHit2D CheckCell()
    {
        // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                         , Mathf.RoundToInt(transform.position.y));
        //Debug.Log(CellPosition);

        // �����ɉ������邩�𔻕�
        RaycastHit2D hit = Physics2D.Raycast(CellPosition, new Vector3(0, 0, 1), 100);

        return hit;
    }

    /// <summary>
    /// �v���C���[�̃}�X�ɃA�C�e��������ΏE���܂�
    /// </summary>
    private void GetItem()
    {
        RaycastHit2D hit = CheckCell();

        if (hit.collider != null && hit.collider.gameObject.tag == "Item")
        {
            CarryItem = hit.collider.gameObject;
            CarryItem.transform.position = new Vector3(999, 999);
            string ItemName = CarryItem.gameObject.name;
            // �I�u�W�F�N�g���������ꂽ�ꍇ
            string[] ItemType = ItemName.Split('_');

            switch (ItemType[0])
            {
                case "Bucket":
                    Tool = ToolState.Bucket;
                    Debug.Log("�o�P�c��������");
                    break;
                case "Shovel":
                    Tool = ToolState.Shovel;
                    Debug.Log("�V���x����������");
                    break;
                case "Seed":
                    Tool = ToolState.Seed;
                    Debug.Log("���������");
                    break;
                case "Bottle":
                    Tool = ToolState.Bottle;
                    Debug.Log("�r����������");
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// �v���C���[�̃}�X�ɃA�C�e����u���܂�
    /// </summary>
    private void RemoveItem()
    {
        RaycastHit2D hit = CheckCell();
        Vector3 SetPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                        , Mathf.RoundToInt(transform.position.y));

        // �����Ȃ��ꏊ�ɂ̂ݒu����i�C���\��j
        if (hit.collider == null)
        {
            CarryItem.transform.position = SetPosition;
            CarryItem = null;

            switch (Tool)
            {
                case ToolState.Bucket:
                    Debug.Log("�o�P�c��u����");
                    break;
                case ToolState.Shovel:
                    Debug.Log("�V���x����u����");
                    break;
                case ToolState.Seed:
                    Debug.Log("���u����");
                    break;
                case ToolState.Bottle:
                    Debug.Log("�r����u����");
                    break;
                default:
                    break;
            }

            Tool = ToolState.None;
        }
        // �����ŉ��ɒu������������
    }
}