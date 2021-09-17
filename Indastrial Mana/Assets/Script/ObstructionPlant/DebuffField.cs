using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffField : MonoBehaviour
{
    private float _EffectTime = 0;//フィールドの表示時間
    [SerializeField] float deleteTime = 3;


    private void Start()
    {
        _EffectTime = 0;
    }

    void Update()
    {
        _EffectTime += Time.deltaTime;
        if(deleteTime <= _EffectTime)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Study.Instance.AddMad();
            //Debug.Log(Study.Madness);
        }
    }
}
