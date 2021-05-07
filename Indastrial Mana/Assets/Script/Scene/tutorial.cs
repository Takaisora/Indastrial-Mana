using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                  // LoadSceneを使うために必要

public class tutorial : MonoBehaviour               //ファイル
{
        public void OnClickStartButton()
        {
            SceneManager.LoadScene("Tutorial");     //移動先

        }
}
