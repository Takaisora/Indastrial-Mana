using UnityEngine;

public class Shovel : SingletonMonoBehaviour<Shovel>
{
    public bool IsFertFilled = false;
    private SpriteRenderer _SR = null;
    private Sprite _ShovelEmpty = null;
    private Sprite _ShovelFilled = null;

    private void Start()
    {
        _SR = GetComponent<SpriteRenderer>();
        _ShovelEmpty = Resources.Load<Sprite>("05_200_shovel");
        _ShovelFilled = Resources.Load<Sprite>("05_300_fertilizer_shovel");
    }

    private void Update()
    {
        if (IsFertFilled)
            _SR.sprite = _ShovelFilled;
        else
            _SR.sprite = _ShovelEmpty;
    }
}
