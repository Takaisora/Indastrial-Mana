using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField]
    PlayerController _PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _PlayerController.Tool == PlayerController.ToolState.Bottle)
        {
            if(_PlayerController.CarryItem.GetComponent<Bottle>().IsManaFilled)
            {
                _PlayerController.CarryItem.transform.position = new Vector3(999, 999);
                _PlayerController.CarryItem = null;
                _PlayerController.Tool = PlayerController.ToolState.None;
                Debug.Log("ƒ}ƒi‚ð”[•i‚µ‚½");
            }
        }
    }
}
