using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Day_1 : MonoBehaviour
{
    public static int Days = 1;

    public int RiquiredManaBottle = 0;//クリアに必要なマナボトル

    public static int ManaBottle = 0;//マナボトル

    public float DayTime = 0;//一日分の時間

    public bool Start_Flag = false;//一日の始終

    public bool Result_Flag = false;//リザルト判定

    public bool Success_Flag = false;//クリア判定

    public static bool Crazy_Flag = false;//ボトル数が見えなくなる判定

    [SerializeField]
    int RiquiredManaBottle1 = 3;//1日目の目標数

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

    public bool BottonDown;

    Touch touch;

    public float CrazyTime = 0;//狂気時間計測

    // Start is called before the first frame update
    public void Start()
    {
        ManaBottle = 0;

        RiquiredManaBottle = RiquiredManaBottle1;//目標数設定

        Day_1_Start script = Day_Start.GetComponent<Day_1_Start>();
        Crazy_Flag = false;//判定のリセット
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
        
        if (Start_Flag == true)
        {
            DayStart();
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

        if(Result_Flag == true)
        {
            Day_Result.SetActive(true);

            ResultDay.text = Days + "Days";

            ResultMoney.text = " x " + PlayerController.Money;

            ResultManaBottle.text = " x " + ManaBottle + " / " + RiquiredManaBottle;

            if(Success_Flag == true)
            {
                ResultSuccess.text = "Success";

                if (BottonDown)
                {
                    SceneManager.LoadScene("Result");

                }

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
            }

            else
            {
                ResultSuccess.text = "Fail";
            }


        }
        else
        {
            Day_Result.SetActive(false);
        }

        Day.text = Days +"Day";

        Day1Money.text =" x " + PlayerController.Money;

        Day1ManaBottle.text = " x " +ManaBottle + " / " + RiquiredManaBottle;
        if (!Crazy_Flag)
        {
            Day1ManaBottle.text = "マナ瓶" + ManaBottle + "/" + RiquiredManaBottle;
        }else if (Crazy_Flag)
        {
            Day1ManaBottle.text = ".@:]/,<>.+;[@[@]/..[";
            CrazyTime += Time.deltaTime;
            if(CrazyTime >= 10)
            {
                Crazy_Flag = false;
                CrazyTime = 0;
            }
        }

        Day1Time.text = time.ToString("F0");

        time = LimitTime - DayTime;

        if (time <= 0)
        {
            Day1Time.text = "End";
        }


    }

    public void SFlag()
    {
        Start_Flag = true;
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void DayStart()
    {
        DayTime += Time.deltaTime;
    }

    public void DayEnd()
    {
        DayTime = 90;
        SoundManager.Instance.EndSound();
        Invoke("Result", 3f);
    }
    public void Result()
    {

        if (ManaBottle < RiquiredManaBottle) //Fail
        {
            Result_Flag = true;

            Success_Flag = false;
            SoundManager.Instance.LoseSound();
        }
        else
        {
            Result_Flag = true;

            Success_Flag = true;
            SoundManager.Instance.WinSound();

        }
    }

    public void ResultClose()
    {
        Result_Flag = false;
    }

    public void buttont()
    {
        BottonDown = true;
    }

    public void buttonf()
    {
        BottonDown = false;
    }
}
