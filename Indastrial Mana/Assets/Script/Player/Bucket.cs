using UnityEngine;

public class Bucket : SingletonMonoBehaviour<Bucket>
{
    public bool IsWaterFilled = false;
    private SpriteRenderer _SR = null;
    private Sprite _BucketEmpty = null;
    private Sprite _BucketFilled = null;

    private void Start()
    {
        _SR = GetComponent<SpriteRenderer>();
        _BucketEmpty = Resources.Load<Sprite>("05_000_bucket");
        _BucketFilled = Resources.Load<Sprite>("05_100_water_bucket");
    }

    private void Update()
    {
        if (IsWaterFilled)
            _SR.sprite = _BucketFilled;
        else
            _SR.sprite = _BucketEmpty;
    }
}
