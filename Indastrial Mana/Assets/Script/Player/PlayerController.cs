using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator Animator;
    private GameObject Bucket;
    private GameObject Shovel;
    [SerializeField, Header("�ړ��̑���")]
    public float MoveSpeed;//�ړ��ʂׂ̈̕ϐ�
    private float DeltaMove;// MoveSpeed * Time.DeltaTime;
    private float MoveX;//���E�ړ��ׂ̈̕ϐ�
    private float MoveY;//�O��ړ��ׂ̈̕ϐ�
    private Vector3 PlayerScale;
    public ToolState Tool;
    public AreaState Area;

    // �A�C�e���������
    public enum ToolState : byte
    {
        None,
        Shovel,
        Bucket
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
        Bucket = GameObject.Find("Bucket");
        Shovel = GameObject.Find("Shovel");
        PlayerScale = transform.localScale;
    }

    void Update()
    {
        #region �ړ�����
        MoveX = 0;
        MoveY = 0;
        DeltaMove = MoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            MoveX = -DeltaMove;
            transform.localScale = new Vector3(PlayerScale.x, PlayerScale.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveX = DeltaMove;
            transform.localScale = new Vector3(-PlayerScale.x, PlayerScale.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            MoveY = DeltaMove;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveY = -DeltaMove;
        }

        transform.Translate(MoveX, MoveY, 0);
        #endregion


        #region �A�j���[�V����
        if (MoveX != 0 || MoveY != 0)
        {
            Animator.SetInteger("PlayerState", 1);
        }
        else
        {
            Animator.SetInteger("PlayerState", 0);
        }
        #endregion

        #region �A�N�V�����{�^��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �A�C�e���������Ă��Ȃ��Ȃ�
            if (Tool == ToolState.None)
                GetItem();
            else if (Tool == ToolState.Bucket || Tool == ToolState.Shovel)
                RemoveItem();
        }
        #endregion

        switch ((int)Area)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }
    }

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

    private void GetItem()
    {
        RaycastHit2D hit = CheckCell();

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Bucket")
            {
                Tool = ToolState.Bucket;
                Bucket.transform.position = new Vector3(999, 999);
            }
            else if (hit.collider.gameObject.tag == "Shovel")
            {
                Tool = ToolState.Shovel;
                Shovel.transform.position = new Vector3(999, 999);
            }
        }
    }

    private void RemoveItem()
    {
        RaycastHit2D hit = CheckCell();
        Vector3 SetPosition = new Vector3(Mathf.RoundToInt(transform.position.x)
                                        , Mathf.RoundToInt(transform.position.y));

        if (hit.collider == null)
        {
            if (Tool == ToolState.Bucket)
            {
                Bucket.transform.position = SetPosition;
                Tool = ToolState.None;
            }
            else if (Tool == ToolState.Shovel)
            {
                Shovel.transform.position = SetPosition;
                Tool = ToolState.None;
            }
        }

        // �����ŉ��ɒu������������
    }
}