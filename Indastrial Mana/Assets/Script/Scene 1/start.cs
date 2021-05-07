using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;              // LoadSceneを使うために必要

public class start : MonoBehaviour              //ファイル
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("1Day");         //移動先

    }

}
