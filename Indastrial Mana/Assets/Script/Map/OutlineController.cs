using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Renderer _Renderer = null;
    private Shader _DefaultShader = null;
    private Shader _OutLineShader = null;
    public bool IsHit = false;
    [SerializeField]
    private ObjectType Type = ObjectType.None;
    private enum ObjectType : byte
    {
        None,
        Item,
        BottleStrage,
        StudyArea
    }

    private void Start()
    {
        _Renderer = GetComponent<Renderer>();
        _DefaultShader = _Renderer.material.shader;
        _OutLineShader = Shader.Find("UnityCommunity/Sprites/Outline");
    }

    private void Update()
    {
        if (PlayerController.Instance.Tool != PlayerController.ToolState.None || Study.Instance.IsStudying)
        {
            _Renderer.material.shader = _DefaultShader;
            return;
        }

        switch(Type)
        {
            case ObjectType.Item:
                if (IsHit)
                    _Renderer.material.shader = _OutLineShader;
                else
                    _Renderer.material.shader = _DefaultShader;
                break;
            case ObjectType.BottleStrage:
                if (PlayerController.Instance.IsEnterBottleStrage && PlayerController.Instance.HitItems.Count == 0)
                    _Renderer.material.shader = _OutLineShader;
                else
                    _Renderer.material.shader = _DefaultShader;
                break;
            case ObjectType.StudyArea:
                if(PlayerController.Instance.IsEnterStudyArea && PlayerController.Instance.HitItems.Count == 0)
                    _Renderer.material.shader = _OutLineShader;
                else
                    _Renderer.material.shader = _DefaultShader;
                break;
            default:
                break;
        }
    }
}
