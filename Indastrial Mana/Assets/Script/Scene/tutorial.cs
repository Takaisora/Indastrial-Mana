using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                  // LoadScene���g�����߂ɕK�v

public class tutorial : MonoBehaviour               //�t�@�C��
{
        public void OnClickStartButton()
        {
            SceneManager.LoadScene("Tutorial");     //�ړ���

        }
}
