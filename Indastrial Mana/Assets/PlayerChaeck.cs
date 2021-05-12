using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaeck : MonoBehaviour
{
    [HideInInspector] public bool inPlayer = false;
    private string playerTag = "Player";
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            inPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            inPlayer = false;
        }
    }
}
