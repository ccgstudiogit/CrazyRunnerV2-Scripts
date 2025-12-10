using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private float heightIncrease = 5;
    [SerializeField] private float moveSpeed = 15f;

    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    private MoveBack moveBackScript;
    private float startAlpha;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        moveBackScript = GetComponentInParent<MoveBack>();

        startAlpha = meshRenderer.material.color.a;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sphereCollider != null)
                sphereCollider.enabled = false;

            if (moveBackScript != null)
                moveBackScript.enabled = false;
            
            StartCoroutine(FadeAway());
        }
    }

    private IEnumerator FadeAway()
    {
        float startingY = transform.position.y;
        float targetY = transform.position.y + heightIncrease;

        while (transform.position.y < targetY)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

            float progress = (transform.position.y - startingY) / heightIncrease;

            float alpha = Mathf.Lerp(startAlpha, 0, progress);
            meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, alpha);
            
            yield return null;
        }

        Destroy(transform.parent.gameObject, 0.1f); // Destroys gem after 0.1 seconds
    }
}
