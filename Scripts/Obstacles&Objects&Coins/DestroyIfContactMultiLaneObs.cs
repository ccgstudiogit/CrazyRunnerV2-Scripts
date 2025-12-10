using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfContactMultiLaneObs : MonoBehaviour
{
    // This scripts belongs on all obstacle (except multi-lane obs) and coin prefabs

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MultiLaneObs"))
        {
            // Destroys this gameObject if it comes into contact with a multi-lane obstacle
            Destroy(gameObject);
        }
    }
}
