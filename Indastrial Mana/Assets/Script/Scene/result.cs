using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                  // LoadScene���g�����߂ɕK�v

public class result : MonoBehaviour               //�t�@�C��
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Title");     //�ړ���

    }
}
