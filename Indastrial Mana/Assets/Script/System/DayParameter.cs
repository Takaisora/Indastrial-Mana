using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayParameter : SingletonMonoBehaviour<DayParameter>
{
    [SerializeField, Header("�e�����̐��E�엿�̌����{��(Day1�`7)")]
    public float[] DecreaseRatios = new float[7];
}
