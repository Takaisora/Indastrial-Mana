using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player = null;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y);
    }
}
