 using UnityEngine;

public class FertStrage : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Instance.Tool == PlayerController.ToolState.ShovelEmpty)
        {
            PlayerController.Instance.Tool = PlayerController.ToolState.ShovelFilled;
            Shovel.Instance.IsFertFilled = true;
            Debug.Log("”ì—¿‚ð‹d‚Á‚½");
            SoundManager.Instance.fertilizerSound();
        }
    }
}