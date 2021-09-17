using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Text : MonoBehaviour
{
    public GameObject TutorialUI;

    public Text TutorialText;

    public static bool Stady = false;

    public static bool Planted = false;

    public static bool Fert = false;

    public static bool Water = false;

    public static bool Mana = false;

    public static bool Bottle = false;

    public static bool Delivery = false;

    float time;

    public GameObject Stya;

    public GameObject Fertya;

    public GameObject Shovelya;

    public GameObject Waterya;

    public GameObject Bucketya;

    public GameObject Bottleya;

    public GameObject Deliya;

    // Update is called once per frame
    void Start()
    {
        Stya.SetActive(true);
        Fertya.SetActive(false);
        Shovelya.SetActive(false);
        Waterya.SetActive(false);
        Bucketya.SetActive(false);
        Bottleya.SetActive(false);
        Deliya.SetActive(false);
    }

    void Update()
    {
        if (Mana == true)
        {
            time += Time.deltaTime;
            if (time >= 3)
                Bottle = true;
        }

        if(Delivery == true)
        {
            Bottleya.SetActive(false);
            Deliya.SetActive(true);
            TutorialText.text = ("�[�i���悤�I");
        }
        else if(Bottle == true)
        {
            Bottleya.SetActive(true);
            TutorialText.text = ("�r�������Ă���\n�l�߂悤");
        }
        else if(Mana == true)
        {
            Waterya.SetActive(false);
            Bucketya.SetActive(false);
            TutorialText.text = ("�}�i�����Y���ꂽ��");
        }
        else if(Water == true)
        {
            Waterya.SetActive(false);
            Bucketya.SetActive(false);
            TutorialText.text = ("��������܂ő҂Ƃ�");
        }
        else if(Fert == true)
        {
            Fertya.SetActive(false);
            Shovelya.SetActive(false);
            Waterya.SetActive(true);
            Bucketya.SetActive(true);
            TutorialText.text = ("�o�P�c���g����\n���������悤");
        }
        else if(Planted == true)
        {
            Stya.SetActive(false);
            Fertya.SetActive(true);
            Shovelya.SetActive(true);
            TutorialText.text = ("�V���x�����g����\n�엿�������悤");
        }
        else if(Stady == true)
        {
            Stya.SetActive(false);
            TutorialText.text = ("���A���悤");
        }
    }
}
