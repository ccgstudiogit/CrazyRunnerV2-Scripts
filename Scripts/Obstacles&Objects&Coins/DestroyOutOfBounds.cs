using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float zBound = -20f;

    private void LateUpdate()
    {
        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }
}
