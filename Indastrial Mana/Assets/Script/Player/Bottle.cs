using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    public bool IsManaFilled = false;
    private SpriteRenderer _SR = null;
    private Sprite _BottleEmpty = null;
    private Sprite _BottleFilled = null;
    private Renderer _Renderer = null;
    private Shader _DefaultShader = null;
    private Shader _OutLineShader = null;

    private void Start()
    {
        IsManaFilled = false;
        _SR = GetComponent<SpriteRenderer>();
        _BottleEmpty = Resources.Load<Sprite>("05_400_bottle");
        _BottleFilled = Resources.Load<Sprite>("05_500_mana_bottle");
        _Renderer = GetComponent<Renderer>();
        _DefaultShader = _Renderer.material.shader;
        _OutLineShader = Shader.Find("UnityCommunity/Sprites/Outline");
    }

    private void Update()
    {
        if (IsManaFilled)
            _SR.sprite = _BottleFilled;
        else
            _SR.sprite = _BottleEmpty;
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