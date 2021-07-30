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
            TutorialText.text = ("”[•i‚µ‚æ‚¤I");
        }
        else if(Bottle == true)
        {
            TutorialText.text = ("•r‚ğ‚Á‚Ä‚«‚Ä\n‹l‚ß‚æ‚¤");
        }
        else if(Mana == true)
        {
            TutorialText.text = ("ƒ}ƒi‚ª¶Y‚³‚ê‚½‚Ë");
        }
        else if(Water == true)
        {
            TutorialText.text = ("¬’·‚·‚é‚Ü‚Å‘Ò‚Æ‚¤");
        }
        else if(Fert == true)
        {
            TutorialText.text = ("…‚ğ‚ ‚°‚æ‚¤");
        }
        else if(Planted == true)
        {
            TutorialText.text = ("”ì—¿‚ğ‚ ‚°‚æ‚¤");
        }
        else if(Stady == true)
        {
            TutorialText.text = ("í‚ğA‚¦‚æ‚¤");
        }
    }
}
