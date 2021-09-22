using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public enum Days : byte
    {
        None,
        Day0,
        Ended,
    }

    public int Tu = 0;

    public GameObject TutorialUI;

    public GameObject TutorialBotton;

    public Text Tutorial_Text;


    public static Days day = Days.Day0;

    public int RiquiredManaBottle = 0;//クリアに必要なマナボトル

    public static int ManaBottle = 0;//マナボトル

    public float DayTime = 0;//一日分の時間

    public bool Start_Flag = false;//一日の始終

    public bool Result_Flag = false;//リザルト判定

    public bool Success_Flag = false;//クリア判定

    [SerializeField]
    int RiquiredManaBottle1 = 1;//1日目の目標数

    public GameObject Day_Start;//オブジェクト仮置き

    Day_1_Start script;

    public GameObject Day_Time;//オブジェクト仮置き

    public GameObject Day_Money;//オブジェクト仮置き

    public GameObject Day_ManaBottle;

    public GameObject Day_Result;

    public GameObject Player;

    public Text ResultDay;

    public Text ResultMoney;

    public Text ResultManaBottle;

    public Text ResultSuccess;

    public Text Day;

    public Text Day1ManaBottle;

    public Text Day1Money;//Text

    public Text Day1Time;//Text

    public float time = 0;//表示される時間

    public float LimitTime = 90;//制限時間

    int SF = 0;

    public Text Tutorial_Text_Botton;

    public GameObject Stick_Back;

    public GameObject yazirusi;

    bool ButtonDown = false;

    Touch touch;

    // Start is called before the first frame update
    public void Start()
    {
        RiquiredManaBottle = RiquiredManaBottle1;//目標数設定

        Day_1_Start script = Day_Start.GetComponent<Day_1_Start>();

        TutorialBotton.SetActive(true);
    }

    // Update is called once per frame
    public void Update()
    {
        #region デバッグ用
        //Debug.Log(DayTime);
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Start_Flag = !Start_Flag;
            DayTime = 85;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            ManaBottle += 1;
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            ManaBottle -= 1;
        }
        #endregion


        if (ManaBottle >= RiquiredManaBottle)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                Day_Result.SetActive(true);

                ResultDay.text = "0Days";

                ResultMoney.text = " x " + PlayerController.Money;

                ResultManaBottle.text = " x " + ManaBottle + " / " + RiquiredManaBottle;

                ResultSuccess.text = "チュートリアル完了";

                Player.GetComponent<PlayerController>().enabled = false;

                if (ButtonDown)
                {
                    PlayerController.Money = 500;
                    SceneManager.LoadScene("Day");

                }


            }

        }

        if (Start_Flag == true)
        {
            TutorialBotton.SetActive(false);
        }
        else
        {

        }

        if (DayTime >= LimitTime)
        {
            Result();

            Start_Flag = false;

            Player.GetComponent<PlayerController>().enabled = false;
        }

//        if(Result_Flag == true)
//        {
//            Day_Result.SetActive(true);

//            ResultDay.text = "0Days";

//            ResultMoney.text = " x " + PlayerController.Money;

//            ResultManaBottle.text = " x " + ManaBottle + " / " + RiquiredManaBottle;

//            if(Success_Flag == true)
//            {
//                ResultSuccess.text = "Success";


//#if UNITY_EDITOR

//                if (Input.GetMouseButtonDown(0))
//                {
//                    DayTime = 0;

//                    Result_Flag = false;

//                    Days += 1;

//                    ManaBottle = 0;

//                    Day_Start.SetActive(true);

//                    Day_Start.GetComponent<Day_1_Start>().ReStart();
                    
//                }

//#endif

////#if UNITY_IOS
////                if (Input.touchCount > 0)
////                {
////                    Touch touch = Input.GetTouch(0);
////                }

////                if (touch.phase == TouchPhase.Began)
////                {
////                    DayTime = 0;

////                    Result_Flag = false;

////                    Days += 1;

////                    ManaBottle = 0;

////                    Day_Start.SetActive(true);

////                    Day_Start.GetComponent<Day_1_Start>().ReStart();
////                }
////#endif
//            }

//            else
//            {
//                ResultSuccess.text = "Fail";
//            }


//        }
//        else
//        {
//            Day_Result.SetActive(false);
//        }

        Day.text = "0Day";

        Day1Money.text =" x " + PlayerController.Money;

        Day1ManaBottle.text = " x " +ManaBottle + " / " + RiquiredManaBottle;

        Day1Time.text = "Tuto";//time.ToString("F0");


    }

    public void SFlag()
    {
        Tutorial_Text_Botton.text = ("近くの物に\nアクションを行うよ") ;
        Stick_Back.SetActive(false);
        yazirusi.SetActive(true);

        SF += 1;
        if(SF == 2)
        Start_Flag = true;
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void DayStart()
    {
        DayTime += Time.deltaTime;
    }

    public void Result()
    {

        if (ManaBottle < RiquiredManaBottle) //Fail
        {
            Result_Flag = true;

            Success_Flag = false;
        }
        else
        {
            Result_Flag = true;

            Success_Flag = true;
        }
    }

    public void ResultClose()
    {
        Result_Flag = false;
    }

    public void buttont()
    {
        ButtonDown = true;
    }

    public void buttonf()
    {
        ButtonDown = false;
    }
}
