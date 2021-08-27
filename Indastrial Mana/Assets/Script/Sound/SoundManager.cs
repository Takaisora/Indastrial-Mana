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


    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
