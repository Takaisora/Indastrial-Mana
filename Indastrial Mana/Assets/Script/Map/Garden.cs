using UnityEngine;
using UnityEngine.UI;

public class Garden : MonoBehaviour
{
    [SerializeField]
    public GameObject WaterGauge;
    [SerializeField]
    public GameObject FertGauge;
    [SerializeField]
    private GameObject _Canvas;
    [SerializeField]
    private GameObject _DefaultPlants = null;
    
    public bool IsPlanted = false;
    private GameObject _Player = null;
    public GameObject MyPlants = null;

    private void Start()
    {
        _Player = GameObject.Find("Player");

        if (_DefaultPlants != null && _Canvas != null)
        {
            MyPlants = Instantiate(_DefaultPlants, transform.position, Quaternion.identity);
            MyPlants.transform.SetParent(_Canvas.transform);
            MyPlants.GetComponent<PlantBase>().Plant(gameObject);
        }
    }

    private void Update()
    {
        if (IsPlanted)
            return;

        Vector3 CellPosition = new Vector3(Mathf.RoundToInt(_Player.transform.position.x)
                                    , Mathf.RoundToInt(_Player.transform.position.y));
        // 種を持ったプレイヤーがこの花壇に重なったら
        if (CellPosition == transform.position && PlayerController.Instance.Tool == PlayerController.ToolState.Seed ||
            CellPosition == transform.position && PlayerController.Instance.Tool == PlayerController.ToolState.ObsSeed)
        {
            PlayerController.Instance.CarryItem.transform.position = transform.position;
            PlayerController.Instance.CarryItem.GetComponent<PlantBase>().Plant(gameObject);
            MyPlants = PlayerController.Instance.CarryItem;
        }
    }
}
