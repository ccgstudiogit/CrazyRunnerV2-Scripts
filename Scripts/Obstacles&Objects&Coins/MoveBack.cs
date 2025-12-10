using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private void Update()
    {
        if (GameController.Instance.gameActive)
        {
            transform.Translate(Vector3.back * SpeedManager.Instance.moveBackSpeed * Time.deltaTime);
        }
    }
}
