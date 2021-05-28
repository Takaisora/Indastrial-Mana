using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Day_1 : MonoBehaviour
{
    public int RiquiredManaBottle = 0;//クリアに必要なお金

    public static int ManaBottle = 0;//マナボトル

    public float DayTime = 0;//一日分の時間

    public bool Start_Flag = false;//一日の始終

    public bool Result_Flag = false;//クリア判定

    [SerializeField]
    int RiquiredManaBottle1 = 3;//1日目の目標数

    [SerializeField]
    public GameObject Day_Start;//オブジェクト仮置き

    [SerializeField]
    public GameObject Day_Time;//オブジェクト仮置き

    [SerializeField]
    public GameObject Day_Money;//オブジェクト仮置き

    [SerializeField]
    public GameObject Day_ManaBottle;

    public Text Day1ManaBottle;

    public Text Day1Money;//Text

    public Text Day1Time;//Text

    public float time = 0;//表示される時間

    public float LimitTime = 90;//制限時間

    // Start is called before the first frame update
    public void Start()
    {
        RiquiredManaBottle = RiquiredManaBottle1;//目標数設定
    }

    // Update is called once per frame
    public void Update()
    {


        //Debug.Log(DayTime);
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Start_Flag = !Start_Flag;
            DayTime = 85;
        }

        if (Start_Flag == true)
        {
            DayStart();
            //Time.timeScale = 1f;
        }

        if (Start_Flag == false)
        {
            //Time.timeScale = 0f;
        }

        if (DayTime >= 90)
        {
            DayEnd();

            Start_Flag = false;

            DayTime = 90;
        }

        Day1Money.text = "お金" + " x " + PlayerController.Money;

        Day1ManaBottle.text = "マナ瓶" + ManaBottle + "/" + RiquiredManaBottle;

        Day1Time.text = time.ToString("F0");

        time = LimitTime - DayTime;

        if (time <= 0)
        {
            Day1Time.text = "終了";
        }


    }

    public void SFlag()//Day_Startから受け取る
    {
        Start_Flag = true;
    }

    public void DayStart()
    {
        DayTime += Time.deltaTime;
    }

    public void DayEnd()
    {
        DayTime = 90;

        Invoke("Result", 3f);
    }
    public void Result()
    {
        if (ManaBottle < RiquiredManaBottle)
        {
            Result_Flag = false;

            SceneManager.LoadScene("Result");
        }
        else
        {

        }

        
    }
}
