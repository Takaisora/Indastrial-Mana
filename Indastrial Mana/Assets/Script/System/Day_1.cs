using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Day_1 : MonoBehaviour
{
    public static int Days = 1;

    public int RiquiredManaBottle = 0;//�N���A�ɕK�v�ȃ}�i�{�g��

    public static int ManaBottle = 0;//�}�i�{�g��

    public float DayTime = 0;//������̎���

    public bool Start_Flag = false;//����̎n�I

    public bool Result_Flag = false;//���U���g����

    public bool Success_Flag = false;//�N���A����

    public static bool Crazy_Flag = false;//�{�g�����������Ȃ��Ȃ锻��

    [SerializeField]
    int RiquiredManaBottle1 = 3;//1���ڂ̖ڕW��

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

    public bool BottonDown;

    Touch touch;

    public float CrazyTime = 0;//���C���Ԍv��

    // Start is called before the first frame update
    public void Start()
    {
        ManaBottle = 0;

        RiquiredManaBottle = RiquiredManaBottle1;//�ڕW���ݒ�

        Day_1_Start script = Day_Start.GetComponent<Day_1_Start>();
        Crazy_Flag = false;//����̃��Z�b�g
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

                //if (BottonDown)
                //{
                //    SceneManager.LoadScene("Result");

                //}

                if (BottonDown)
                {
                    DayTime = 0;

                    Result_Flag = false;

                    Days += 1;

                    ManaBottle = 0;

                    Day_Start.SetActive(true);

                    Day_Start.GetComponent<Day_1_Start>().ReStart();

                }
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
            Day1ManaBottle.text = "x" + ManaBottle + "/" + RiquiredManaBottle;
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
