using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : SingletonMonoBehaviour<Delivery>
{
    [SerializeField]
    PlayerController _PlayerController;
    private const byte _REWORD = 100;

    private Animator animator;

    private const string _Delyvery = "Delyvery";

    private int Delay = 2;//ディレイ用
    private float _DelayTime = 0;
    private bool DelayFlag = false;//ディレイ用のフラグ

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DelayFlag)
        {
            _DelayTime += Time.deltaTime;
            if (Delay <= _DelayTime)
            {
                Debug.Log("資金が" + _REWORD + "増えた");
                TextLog.Instance.Insert($"資金が{_REWORD}増えた");
                SoundManager.Instance.MoneySound();
                DelayFlag = false;
                _DelayTime = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _PlayerController.Tool == PlayerController.ToolState.BottleFilled)
        {
            if(_PlayerController.CarryItem.GetComponent<Bottle>().IsManaFilled)
            {
                animator.SetTrigger(_Delyvery);
                _PlayerController.CarryItem.transform.position = new Vector3(999, 999);
                _PlayerController.CarryItem = null;
                _PlayerController.Tool = PlayerController.ToolState.None;

                PlayerController.Money += _REWORD;
                Day_1.ManaBottle += 1;
                Tutorial.ManaBottle += 1;
                Debug.Log("マナを納品した");
                TextLog.Instance.Insert("マナを納品した");
                SoundManager.Instance.ManaSound();
                DelayFlag = true;
                
            }
        }
    }
}
