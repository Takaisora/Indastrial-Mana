using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffField : ObstructionPlant_3
{
    private float _EffectTime = 3;//�t�B�[���h�̕\������
    void Start()
    {
        _EffectTime = 3;
    }


    void Update()
    {
        _EffectTime -= Time.deltaTime;
        if(_EffectTime <= 0)
        {
            Destroy(this);
        }
    }
}
