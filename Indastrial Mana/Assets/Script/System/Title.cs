using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Select");
        }

#endif

#if UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("Select");
            }
        } 
#endif
    }
}
