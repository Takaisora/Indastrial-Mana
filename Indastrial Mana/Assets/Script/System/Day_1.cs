using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Day_1 : MonoBehaviour
{
    public int RiquiredManaBottle = 0;//�N���A�ɕK�v�Ȃ���

    public static int ManaBottle = 0;//�}�i�{�g��

    public float DayTime = 0;//������̎���

    public bool Start_Flag = false;//����̎n�I

    public bool Result_Flag = false;//�N���A����

    [SerializeField]
    int RiquiredManaBottle1 = 3;//1���ڂ̖ڕW��

    [SerializeField]
    public GameObject Day_Start;//�I�u�W�F�N�g���u��

    [SerializeField]
    public GameObject Day_Time;//�I�u�W�F�N�g���u��

    [SerializeField]
    public GameObject Day_Money;//�I�u�W�F�N�g���u��

    [SerializeField]
    public GameObject Day_ManaBottle;

    public Text Day1ManaBottle;

    public Text Day1Money;//Text

    public Text Day1Time;//Text

    public float time = 0;//�\������鎞��

    public float LimitTime = 90;//��������

    // Start is called before the first frame update
    public void Start()
    {
        RiquiredManaBottle = RiquiredManaBottle1;//�ڕW���ݒ�
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

        Day1Money.text = "����" + " x " + PlayerController.Money;

        Day1ManaBottle.text = "�}�i�r" + ManaBottle + "/" + RiquiredManaBottle;

        Day1Time.text = time.ToString("F0");

        time = LimitTime - DayTime;

        if (time <= 0)
        {
            Day1Time.text = "�I��";
        }


    }

    public void SFlag()//Day_Start����󂯎��
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
