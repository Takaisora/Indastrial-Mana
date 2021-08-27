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
