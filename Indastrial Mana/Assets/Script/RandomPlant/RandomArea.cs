using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomArea : MonoBehaviour
{
// 2D�̏ꍇ�̃g���K�[����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���̂��g���K�[�ɐڐG���Ƃ��A�P�x�����Ă΂��
        Debug.Log("�G���A�ɓ���܂���");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���̂��g���K�[�ɐڐG���Ă���ԁA��ɌĂ΂��
        Debug.Log("�G���A���ɂ��܂�");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���̂��g���K�[�Ɨ��ꂽ�Ƃ��A�P�x�����Ă΂��
        Debug.Log("�G���A���痣��܂���");
    }
}
