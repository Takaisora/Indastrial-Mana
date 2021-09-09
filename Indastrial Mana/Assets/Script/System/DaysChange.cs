using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaysChange : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text = null;

    // Update is called once per frame
    void Update()
    {
        _text.text = Day_1.Days + "Day";
    }
}
