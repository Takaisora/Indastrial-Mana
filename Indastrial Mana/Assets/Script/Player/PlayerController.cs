using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator = null;
    [SerializeField, Header("��������")]
    public static ushort Money = 500;// (�����܂����ň����p��)
    [SerializeField, Header("�ړ��̑���")]
    private float _MoveSpeed = 0;//�ړ��ʂׂ̈̕ϐ�
    public static float MoveRatio = 1;// �f�o�t�ȂǂɎg�p
    public GameObject CarryItem = null;// �������^��ł���A�C�e��
    public ToolState Tool = ToolState.None;// �������^��ł���A�C�e���̎�ނ�\�����
    private float _DeltaMove = 0;// MoveSpeed * MoveRatio * Time.DeltaTime;
    private float _MoveX = 0;//���E�ړ��ׂ̈̕ϐ�
    private float _MoveY = 0;//�O��ړ��ׂ̈̕ϐ�
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
        #region �ړ�����
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
        transform.Translate(_MoveX, _MoveY, 0);// PC���d���̂ł�����
#endif
#if UNITY_IOS
        Vector2 move = new Vector2(_MoveX, _MoveY);// �ŏI�I�ɂ͂�����
        rb.velocity = move;
#endif
        #endregion

        #region �A�j���[�V����
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

        #region �A�N�V�����{�^��
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);
        //    // �A�C�e���������Ă��Ȃ��Ȃ�
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
                    CarryItem.GetComponent<PlantBase>().Plant(Gardens.WaterGauge, Gardens.FertGauge);
                    CarryItem = null;
                    Tool = ToolState.None;
                }
            }
        }
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

    /// <summary>
    /// �v���C���[�̃}�X�ɃA�C�e��������ΏE���܂�
    /// </summary>

    public void ActionBotton()
    {
        RaycastHit2D Hit = CheckCell(transform.position.x, transform.position.y);
        // �A�C�e���������Ă��Ȃ��Ȃ�
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
            // �A�C�e�����������ꂽ�ꍇ�p
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
                Still = false;
            }
        } while (Still);
    }
}