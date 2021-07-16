using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    public static bool IsWaterFilled = false;

    public Image image;
    private Sprite sprite;
    private Sprite sprite2;

    private void Start()
    {
        IsWaterFilled = false;
        image = this.GetComponent<Image>();
        sprite = Resources.Load<Sprite>("05_000_bucket");
        sprite2 = Resources.Load<Sprite>("05_100_water_bucket");
    }
private void Update()
{
    if (IsWaterFilled == false)
    {
        image.sprite = sprite;
    }
    else
    {
        image.sprite = sprite2;
    }
}
}
