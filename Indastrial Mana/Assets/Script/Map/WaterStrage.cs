using UnityEngine;

public class WaterStrage : SingletonMonoBehaviour<WaterStrage>
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerController.Instance.Tool == PlayerController.ToolState.BucketEmpty)
        {
            PlayerController.Instance.Tool = PlayerController.ToolState.BucketFilled;
            Bucket.Instance.IsWaterFilled = true;
            Debug.Log("水を汲んだ");
            TextLog.Instance.Insert("水を汲んだ");
            SoundManager.Instance.WaterSound();
        }
    }
}
