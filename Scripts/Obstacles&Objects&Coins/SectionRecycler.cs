using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionRecycler : MonoBehaviour
{
    [SerializeField] private int minZ = -22;
    [SerializeField] private int resetToZ = 242;

    private void LateUpdate()
    {
        if (transform.position.z <= minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, resetToZ);
        }
    }
}
