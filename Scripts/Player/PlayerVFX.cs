using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    // PlayerCollision calls smokeExplosion on player death
    [SerializeField] private ParticleSystem smokeExplosion;

    public void PlaySmokeExplosion()
    {
        smokeExplosion.Play();
    }
}
