using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shovel : MonoBehaviour
{
    public static bool IsFertFilled = false;

    public Image image;
    private Sprite sprite;
    private Sprite sprite2;

    private void Start()
    {
        IsFertFilled = false;
        image = this.GetComponent<Image>();
        sprite = Resources.Load<Sprite>("05_200_shovel");
        sprite2 = Resources.Load<Sprite>("05_300_fertilizer_shovel");
    }
    private void Update()
    {
        if(IsFertFilled == false)
        {
            image.sprite = sprite;
        }
        else
        {
            image.sprite = sprite2;
        }
    }
}
