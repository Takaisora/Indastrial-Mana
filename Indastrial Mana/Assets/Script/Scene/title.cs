using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;              // LoadSceneを使うために必要

public class title : MonoBehaviour              //ファイル
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Mainmenu");     //移動先

    }

}
