using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public AudioClip ItemCarry;//�A�C�e�����E������
    public AudioClip Money;//�����ɕϓ�����������
    public AudioClip PlayerMove;//�L��������������
    public AudioClip Mana;//�}�i��[�i������
    public AudioClip Water;//�����グ����
    public AudioClip End;//������I�������Ƃ�
    public AudioClip Lose;//�ڕW�B���ł��Ȃ������Ƃ�
    public AudioClip Win;//�ڕW�B���ł�����
    public AudioClip fertilizer;//�엿���グ����
    public AudioClip Wither;//�A�����͂ꂽ��

    public static int WalkCount = 0;
    public static bool delayKeyWalk = false;
    private float delayTimeWalk = 0;
    private int LoseCount = 0;
    public static bool delayKeyLose = false;
    private float delayTimeLose = 0;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        WalkCount = 0;
        delayKeyWalk = false;
        delayKeyLose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayKeyWalk)
        {
            if (WalkCount == 0)
            {
                PlayerMoveSound();
                WalkCount++;
            }
            else
            {
                delayTimeWalk += Time.deltaTime;
                if(delayTimeWalk > 0.4)
                {
                    PlayerMoveSound();
                    delayTimeWalk = 0;
                }
            }
        }

        if (delayKeyLose)
        {
            if(LoseCount == 0)
            {
                LoseSound();
                LoseCount++;
            }
            else
            {
                delayTimeLose += Time.deltaTime;
                if(delayTimeLose > 2.6)
                {
                    LoseSound();
                    delayTimeLose = 0;
                }
            }
        }
    }

    public void ItemCarrySound()
    {
        audioSource.PlayOneShot(ItemCarry);
    }

    public void MoneySound()
    {
        audioSource.PlayOneShot(Money);
    }

    public void PlayerMoveSound()
    {
        audioSource.PlayOneShot(PlayerMove);
    }

    public void ManaSound()
    {
        audioSource.PlayOneShot(Mana);
    }

    public void WaterSound()
    {
        audioSource.PlayOneShot(Water);
    }

    public void EndSound()
    {
        audioSource.PlayOneShot(End);
    }

    public void LoseSound()
    {
        audioSource.PlayOneShot(Lose);
    }

    public void WinSound()
    {
        audioSource.PlayOneShot(Win);
    }

    public void fertilizerSound()
    {
        audioSource.PlayOneShot(fertilizer);
    }

    public void WitherSound()
    {
        audioSource.PlayOneShot(Wither);
    }
}
