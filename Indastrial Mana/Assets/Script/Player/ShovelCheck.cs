using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool InPlayer2 { get; private set; } = false;

    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            InPlayer2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            InPlayer2 = false;
        }
    }

    public void ActiveItem(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public void ShovelMove(Vector3 v)
    {
        Vector3 sUpdate = new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), 0);
        transform.position = sUpdate;
    }
}
