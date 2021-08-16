using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public int Tu = 0;

    public GameObject TutorialUI;

    public GameObject TutorialBotton;

    public Text Tutorial_Text;


    public static int Days = 1;

    public int RiquiredManaBottle = 0;//�N���A�ɕK�v�ȃ}�i�{�g��

    public static int ManaBottle = 0;//�}�i�{�g��

    public float DayTime = 0;//������̎���

    public bool Start_Flag = false;//����̎n�I

    public bool Result_Flag = false;//���U���g����

    public bool Success_Flag = false;//�N���A����

    [SerializeField]
    int RiquiredManaBottle1 = 1;//1���ڂ̖ڕW��

    public GameObject Day_Start;//�I�u�W�F�N�g���u��

    Day_1_Start script;

    public GameObject Day_Time;//�I�u�W�F�N�g���u��

    public GameObject Day_Money;//�I�u�W�F�N�g���u��

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

    public float time = 0;//�\������鎞��

    public float LimitTime = 90;//��������

    int SF = 0;

    public Text Tutorial_Text_Botton;

    public GameObject Stick_Back;

    public GameObject yazirusi;

    bool ButtonDown = false;

    Touch touch;

    // Start is called before the first frame update
    public void Start()
    {
        RiquiredManaBottle = RiquiredManaBottle1;//�ڕW���ݒ�

        Day_1_Start script = Day_Start.GetComponent<Day_1_Start>();

        TutorialBotton.SetActive(true);
    }

    // Update is called once per frame
    public void Update()
    {
        #region �f�o�b�O�p
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

                ResultSuccess.text = "�`���[�g���A������";

                Player.GetComponent<PlayerController>().enabled = false;

                if (ButtonDown)
                {
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
        Tutorial_Text_Botton.text = ("�߂��̕���\n�A�N�V�������s����") ;
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