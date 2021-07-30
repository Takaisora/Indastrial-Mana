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

    // Update is called once per frame
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
            TutorialText.text = ("�[�i���悤�I");
        }
        else if(Bottle == true)
        {
            TutorialText.text = ("�r�������Ă���\n�l�߂悤");
        }
        else if(Mana == true)
        {
            TutorialText.text = ("�}�i�����Y���ꂽ��");
        }
        else if(Water == true)
        {
            TutorialText.text = ("��������܂ő҂Ƃ�");
        }
        else if(Fert == true)
        {
            TutorialText.text = ("���������悤");
        }
        else if(Planted == true)
        {
            TutorialText.text = ("�엿�������悤");
        }
        else if(Stady == true)
        {
            TutorialText.text = ("���A���悤");
        }
    }
}
