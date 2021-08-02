using UnityEngine;

public class Shovel : SingletonMonoBehaviour<Shovel>
{
    public bool IsFertFilled = false;
    private SpriteRenderer _SR = null;
    private Sprite _ShovelEmpty = null;
    private Sprite _ShovelFilled = null;
    private Renderer _Renderer = null;
    private Shader _DefaultShader = null;
    private Shader _OutLineShader = null;

    private void Start()
    {
        _SR = GetComponent<SpriteRenderer>();
        _ShovelEmpty = Resources.Load<Sprite>("05_200_shovel");
        _ShovelFilled = Resources.Load<Sprite>("05_300_fertilizer_shovel");
        _Renderer = GetComponent<Renderer>();
        _DefaultShader = _Renderer.material.shader;
        _OutLineShader = Shader.Find("UnityCommunity/Sprites/Outline");
    }

    private void Update()
    {
        if (IsFertFilled)
            _SR.sprite = _ShovelFilled;
        else
            _SR.sprite = _ShovelEmpty;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _Renderer.material.shader = _OutLineShader;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _Renderer.material.shader = _DefaultShader;
    }
}
