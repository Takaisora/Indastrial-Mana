using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField]
    private Animator _Animator = null;
    [SerializeField, Header("��������")]
    public static ushort Money = 500;// (�����܂����ň����p��)
    [SerializeField, Header("�ړ��̑���")]
    private float _MoveSpeed = 0;//�ړ��ʂׂ̈̕ϐ�
    public float MoveRatio = 1;// �f�o�t�ȂǂɎg�p
    public GameObject CarryItem = null;// �������^��ł���A�C�e��
    public ToolState Tool = ToolState.None;// �������^��ł���A�C�e���̎�ނ�\�����
    private float _DeltaMove = 0;// MoveSpeed * MoveRatio * Time.DeltaTime;
    private float _MoveX = 0;//���E�ړ��ׂ̈̕ϐ�
    private float _MoveY = 0;//�O��ړ��ׂ̈̕ϐ�
    private Vector3 _PlayerScale = Vector3.zero;
    private Rigidbody2D _RB = null;
    public List<GameObject> HitItems = new List<GameObject>();// �ڐG���Ă���A�C�e��
    public bool IsEnterBottleStrage = false;
    public bool IsEnterStudyArea = false;
    private Study _MyStudy = null;
    private Rigidbody2D rb = null;
    public static bool Buff = false;
    public static float BuffTime = 0;

    public AudioClip ItemCarry;
    AudioSource audioSource;

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
        ObsSeed,
    }

    private void Start()
    {
        MoveRatio = 1;
        _PlayerScale = transform.localScale;
        Tool = ToolState.None;
        GameObject Study = GameObject.Find("StudyArea");
        _MyStudy = Study.GetComponent<Study>();
        rb = GetComponent<Rigidbody2D>();
        Buff = false;
        BuffTime = 0;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        #region ���A���鏈��
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
                    // �A�������̏�ɐA����
                    CarryItem.transform.position = SetPosition;
                    // �Q�[�W�N��
                    CarryItem.GetComponent<PlantBase>().Plant(Garden);
                    CarryItem = null;
                    Tool = ToolState.None;
                    Tutorial_Text.Planted = true;
                }
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region �ړ�����
#if UNITY_EDITOR
        _MoveSpeed = 5;
#endif
        _MoveX = 0;
        _MoveY = 0;
        _DeltaMove = _MoveSpeed * MoveRatio * Time.deltaTime;

        if (Buff)
        {
            BuffTime -= Time.deltaTime;
            if (BuffTime <= 0)
            {
                MoveRatio = 1;
                Buff = false;
            }
        }

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
#if UNITY_EDITOR
        transform.Translate(_MoveX, _MoveY, 0);// PC���d���̂ł�����
#endif
#if UNITY_IOS
        Vector2 move = new Vector2(_MoveX, _MoveY);// �ŏI�I�ɂ͂�����
        rb.velocity = move;
#endif

        //JoyStick

        Vector3 MoveVecter = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        if (MoveVecter != Vector3.zero)
        {
            transform.Translate(MoveVecter * _DeltaMove, Space.World);
        }

        if (joystick.Horizontal < 0)
        {
            transform.localScale = new Vector3(_PlayerScale.x, _PlayerScale.y);
            SoundManager.delayKeyWalk = true;
        }
        else if (joystick.Horizontal > 0)
        {
            transform.localScale = new Vector3(-_PlayerScale.x, _PlayerScale.y);
            SoundManager.delayKeyWalk = true;
        }
        else
        {
            SoundManager.delayKeyWalk = false;
            SoundManager.WalkCount = 0;
        }
        #endregion

        #region �A�j���[�V����
        byte AnimationState = (byte)Tool;
        if (_MoveX != 0 || _MoveY != 0 || MoveVecter != Vector3.zero)
            AnimationState += 100;// �����Ă���Ȃ�+100
        _Animator.SetInteger("PlayerState", AnimationState);
        #endregion
    }

    /// <summary>
    /// �w��̃}�X�ɉ������邩�𒲂ׂ܂�
    /// </summary>
    /// <param name="X">���ׂ���x���W</param>
    /// <param name="Y">���ׂ���y���W</param>
    /// <returns></returns>
    private RaycastHit2D CheckCell(float X, float Y)
    {
        // ���W���l�̌ܓ��Ő�����(�����ۂ߂Ȃ̂ŕs��N���邩���H)
        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(X)
                                         , Mathf.RoundToInt(Y));
        // �����ɉ������邩�𔻕�
        RaycastHit2D Hit = Physics2D.Raycast(CellPosition, new Vector3(0, 0, 1), 100);

        return Hit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            HitItems.Add(collision.gameObject);
            HitItems[0].GetComponent<OutlineController>().IsHit = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BottleStrage"))
            IsEnterBottleStrage = true;
        else if(collision.gameObject.CompareTag("Study"))
            IsEnterStudyArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            HitItems.Remove(collision.gameObject);
            collision.gameObject.GetComponent<OutlineController>().IsHit = false;

            if (HitItems.Count > 0)
                HitItems[0].GetComponent<OutlineController>().IsHit = true;
        }
        else if (collision.gameObject.CompareTag("BottleStrage"))
            IsEnterBottleStrage = false;
        else if (collision.gameObject.CompareTag("Study"))
            IsEnterStudyArea = false;
    }

    /// <summary>
    /// �v���C���[�̃}�X�ɃA�C�e��������ΏE���܂�
    /// </summary>
    public void ActionBotton()
    {
        // �A�C�e���������Ă��Ȃ��Ȃ�
        if (Tool == ToolState.None)
        {
            if (HitItems.Count > 0)
            {
                GetItem();
                SoundManager.Instance.ItemCarrySound();
            }
            else if (IsEnterStudyArea)
            {
                Study.Instance.Studying();
                Tutorial_Text.Stady = true;
            }
            else if (IsEnterBottleStrage)
            {
                SoundManager.Instance.ItemCarrySound();
                BottleStrage.Instance.GetBottle();
            }
        }
        else
            RemoveItem(transform.position.x, transform.position.y);
    }

    private void GetItem()
    {
        CarryItem = HitItems[0];
        CarryItem.transform.position = new Vector3(999, 999);
        string ItemName = CarryItem.gameObject.name;
        // �A�C�e�����������ꂽ�ꍇ�p
        string[] ItemType = ItemName.Split('_');

        switch (ItemType[0])
        {
            case "Bucket":
                audioSource.PlayOneShot(ItemCarry);
                if (Bucket.Instance.IsWaterFilled)
                    Tool = ToolState.BucketFilled;
                else
                    Tool = ToolState.BucketEmpty;
                Debug.Log("�o�P�c��������");
                TextLog.Instance.Insert("�o�P�c��������");
                break;
            case "Shovel":
                if (Shovel.Instance.IsFertFilled)
                    Tool = ToolState.ShovelFilled;
                else
                    Tool = ToolState.ShovelEmpty;
                Debug.Log("�V���x����������");
                TextLog.Instance.Insert("�V���x����������");
                break;
            case "Seed":
                Tool = ToolState.Seed;
                Debug.Log("���������");
                TextLog.Instance.Insert("���������");
                break;
            case "ObsSeed":
                Tool = ToolState.ObsSeed;
                Debug.Log("���������");
                TextLog.Instance.Insert("���������");
                break;
            case "Bottle":
                if (CarryItem.GetComponent<Bottle>().IsManaFilled)
                    Tool = ToolState.BottleFilled;
                else
                    Tool = ToolState.BottleEmpty;
                Debug.Log("�r����������");
                TextLog.Instance.Insert("�r����������");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �v���C���[�̃}�X�����̎���4�}�X�Ƀz�[���h���̃A�C�e����u���܂�
    /// </summary>
    private void RemoveItem(float PlayerPosX, float PlayerPosY)
    {
        bool Still = true;// ���g�̃}�X�ɃA�C�e����u��������4�}�X�ɋ󂫂��Ȃ���Ώ������I���
        byte CheckCount = 0;// 0...�^��, 1...�E,2...��, 3...��, 4...��
        // �u���ꏊ�w��p
        float X = PlayerPosX;
        float Y = PlayerPosY;

        do
        {
            RaycastHit2D Hit = CheckCell(X, Y);

            // ���ׂ��}�X�ɃA�C�e�����u���Ȃ��Ȃ�
            if (Hit.collider != null)
            {
                CheckCount++;
                // ���ɒ��ׂ�}�X���w��
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
            // �����Ȃ���΃A�C�e����u��
            else
            {
                Vector3 SetPosition = new Vector3(Mathf.RoundToInt(X)
                                    , Mathf.RoundToInt(Y));
                CarryItem.transform.position = SetPosition;
                CarryItem = null;

                switch (Tool)
                {
                    case ToolState.BucketEmpty:
                        Debug.Log("�o�P�c��u����");
                        TextLog.Instance.Insert("�o�P�c��u����");
                        break;
                    case ToolState.ShovelEmpty:
                        Debug.Log("�V���x����u����");
                        TextLog.Instance.Insert("�V���x����u����");
                        break;
                    case ToolState.Seed:
                        Debug.Log("���u����");
                        TextLog.Instance.Insert("���u����");
                        break;
                    case ToolState.BottleEmpty:
                        Debug.Log("�r����u����");
                        TextLog.Instance.Insert("�r����u����");
                        break;
                    case ToolState.BottleFilled:
                        Debug.Log("�r����u����");
                        TextLog.Instance.Insert("�r����u����");
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