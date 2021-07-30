 using UnityEngine;

public class FertStrage : MonoBehaviour
{
    [SerializeField]
    PlayerController PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Tool == PlayerController.ToolState.Shovel)
        {
            if(!Shovel.IsFertFilled)
            {
                Shovel.IsFertFilled = true;
                Debug.Log("”ì—¿‚ð‹d‚Á‚½");
                
            }
        }
    }
}