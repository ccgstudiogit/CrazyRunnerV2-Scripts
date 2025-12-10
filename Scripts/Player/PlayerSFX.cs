using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void PlayFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f) { SoundController.Instance.PlaySound(SoundType.Footstep); }
    }

    public void PlayLand()
    {
        SoundController.Instance.PlaySound(SoundType.Land);
    }

    public void PlayRoll()
    {
        SoundController.Instance.PlaySound(SoundType.Roll);
    }
}
