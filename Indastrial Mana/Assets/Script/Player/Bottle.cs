using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    public bool IsManaFilled = false;

    private SpriteRenderer _SR = null;
    private Sprite _BottleEmpty = null;
    private Sprite _BottleFilled = null;

    //private void Start()
    //{
    //    IsManaFilled = false;
    //    _SR = GetComponent<SpriteRenderer>();
    //    _BottleEmpty = Resources.Load<Sprite>("05_400_bottle");
    //    _BottleFilled = Resources.Load<Sprite>("05_500_mana_bottle");
    //}

    //private void Update()
    //{
    //    if (IsManaFilled)
    //        _SR.sprite = _BottleFilled;
    //    else
    //        _SR.sprite = _BottleEmpty;
    //}
}