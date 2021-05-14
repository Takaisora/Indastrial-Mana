using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                  // LoadSceneを使うために必要

public class result : MonoBehaviour               //ファイル
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Title");     //移動先

    }
}
