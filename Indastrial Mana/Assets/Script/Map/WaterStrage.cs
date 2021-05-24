using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStrage : MonoBehaviour
{
    [SerializeField]
    PlayerController PlayerController;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Tool == PlayerController.ToolState.Bucket)
        {
            Bucket.IsWaterFilled = true;
            Debug.Log("charged");
        }
    }
}
