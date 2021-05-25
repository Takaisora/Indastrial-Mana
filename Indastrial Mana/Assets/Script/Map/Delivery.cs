using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField]
    PlayerController PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Tool == PlayerController.ToolState.Bottle)
        {
            if (Bottle.IsManaFilled)
            {
                Bottle.IsManaFilled = false;
                Debug.Log("ƒ}ƒi‚ð”[•i‚µ‚½");
            }
        }
    }
}
