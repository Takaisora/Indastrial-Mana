using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomArea : MonoBehaviour
{
// 2Dの場合のトリガー判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        Debug.Log("エリアに入りました");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 物体がトリガーに接触している間、常に呼ばれる
        Debug.Log("エリア内にいます");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる
        Debug.Log("エリアから離れました");
    }
}
