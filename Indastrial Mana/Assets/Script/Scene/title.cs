using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;              // LoadScene���g�����߂ɕK�v

public class title : MonoBehaviour              //�t�@�C��
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Mainmenu");     //�ړ���

    }

}
