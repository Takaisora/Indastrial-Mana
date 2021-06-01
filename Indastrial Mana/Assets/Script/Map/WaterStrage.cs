using UnityEngine;

public class WaterStrage : MonoBehaviour
{
    [SerializeField]
    PlayerController PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Tool == PlayerController.ToolState.Bucket)
        {
            if(!Bucket.IsWaterFilled)
            {
                Bucket.IsWaterFilled = true;
                Debug.Log("êÖÇãÇÇÒÇæ");
            }
        }
    }
}
