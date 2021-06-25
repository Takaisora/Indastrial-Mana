using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffField : MonoBehaviour
{
    private float _EffectTime = 0;//フィールドの表示時間
    [SerializeField] float deleteTime = 3;


    void Update()
    {
        _EffectTime += Time.deltaTime;
        if(deleteTime <= _EffectTime)
        {
            Destroy(this.gameObject);
        }
    }
}
