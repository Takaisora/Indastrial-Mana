using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    //[SerializeField]
    //PlayerController _PlayerController;
    private const byte _REWORD = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Tool == PlayerController.ToolState.Bottle)
        {
            if(PlayerController.CarryItem.GetComponent<Bottle>().IsManaFilled)
            {
                PlayerController.CarryItem.transform.position = new Vector3(999, 999);
                PlayerController.CarryItem = null;
                PlayerController.Tool = PlayerController.ToolState.None;

                PlayerController.Money += _REWORD;
                Day_1.ManaBottle += 1;
                Debug.Log("É}ÉiÇî[ïiÇµÇΩ");
                Debug.Log("éëã‡Ç™" + _REWORD + "ëùÇ¶ÇΩ");
            }
        }
    }
}
