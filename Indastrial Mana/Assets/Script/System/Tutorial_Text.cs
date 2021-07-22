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
            TutorialText.text = ("納品しよう！");
        }
        else if(Bottle == true)
        {
            TutorialText.text = ("瓶を持ってきて\n詰めよう");
        }
        else if(Mana == true)
        {
            TutorialText.text = ("マナが生産されたね");
        }
        else if(Water == true)
        {
            TutorialText.text = ("成長するまで待とう");
        }
        else if(Fert == true)
        {
            TutorialText.text = ("水をあげよう");
        }
        else if(Planted == true)
        {
            TutorialText.text = ("肥料をあげよう");
        }
        else if(Stady == true)
        {
            TutorialText.text = ("種を畑に持っていこう");
        }
    }
}
