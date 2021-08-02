using UnityEngine;

public class Bucket : SingletonMonoBehaviour<Bucket>
{
    public bool IsWaterFilled = false;
    private SpriteRenderer _SR = null;
    private Sprite _BucketEmpty = null;
    private Sprite _BucketFilled = null;
    private Renderer _Renderer = null;
    private Shader _DefaultShader = null;
    private Shader _OutLineShader = null;

    private void Start()
    {
        _SR = GetComponent<SpriteRenderer>();
        _BucketEmpty = Resources.Load<Sprite>("05_000_bucket");
        _BucketFilled = Resources.Load<Sprite>("05_100_water_bucket");
        _Renderer = GetComponent<Renderer>();
        _DefaultShader = _Renderer.material.shader;
        _OutLineShader = Shader.Find("UnityCommunity/Sprites/Outline");
    }

    private void Update()
    {
        if (IsWaterFilled)
            _SR.sprite = _BucketFilled;
        else
            _SR.sprite = _BucketEmpty;
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
