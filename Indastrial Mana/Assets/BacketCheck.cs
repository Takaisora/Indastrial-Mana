using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacketCheck : MonoBehaviour
{
    public bool InPlayer { get; private set; } = false;

    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            InPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            InPlayer = false;
        }
    }

    public void ActiveItem(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void BacketMove(Vector3 v)
    {
        Vector3 bUpdate = new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), 0);
        transform.position = bUpdate;
    }
}
