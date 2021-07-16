using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    public bool IsManaFilled = false;

    public Image image;
    private Sprite sprite;
    private Sprite sprite2;

    private void Start()
    {
        IsManaFilled = false;
        image = this.GetComponent<Image>();
        sprite = Resources.Load<Sprite>("05_400_bottle");
        sprite2 = Resources.Load<Sprite>("05_500_mana_bottle");
    }
    private void Update()
    {
        if (IsManaFilled == false)
        {
            image.sprite = sprite;
        }
        else
        {
            image.sprite = sprite2;
        }
    }
}

