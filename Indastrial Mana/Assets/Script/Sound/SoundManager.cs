using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public AudioClip ItemCarry;//アイテムを拾った時
    public AudioClip Money;//お金に変動があった時
    public AudioClip PlayerMove;//キャラが動いた時
    public AudioClip Mana;//マナを納品した時
    public AudioClip Water;//水を上げた時
    public AudioClip End;//一日が終了したとき
    public AudioClip Lose;//目標達成できなかったとき
    public AudioClip Win;//目標達成できた時
    public AudioClip fertilizer;//肥料を上げた時
    public AudioClip Wither;//植物が枯れた時

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
