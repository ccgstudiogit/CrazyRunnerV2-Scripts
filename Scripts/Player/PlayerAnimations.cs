using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Animator convention -- use ints (ran into problems with bools): 0 = off, 1 = play animation (once), 2 = set as current animation
    private Animator playerAnimator;
    private float floatDelay = 0.33f;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameController.Instance.OnGameActiveChanged += OnGameActiveChanged;
    }

    public void Jump()
    {
        playerAnimator.SetInteger("jump_int", 1);
        StartCoroutine(SetIntToZero("jump_int", floatDelay));
    }

    public void Land()
    {
        playerAnimator.SetInteger("land_int", 1);
        StartCoroutine(SetIntToZero("land_int", floatDelay));
    }

    public void Roll()
    {
        playerAnimator.SetInteger("roll_int", 1);
        StartCoroutine(SetIntToZero("roll_int", floatDelay));
    }

    public void Death()
    {
        playerAnimator.SetInteger("dead_int", 2);
        playerAnimator.SetTrigger("hit_trig");
    }

    private void BeginRunning()
    {
        playerAnimator.SetInteger("running_int", 2);
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameActiveChanged -= OnGameActiveChanged;
    }

    private void OnGameActiveChanged()
    {
        if (GameController.Instance.gameActive)
        {
            BeginRunning();
        }
    }

    private IEnumerator SetIntToZero(string parameterName, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerAnimator.SetInteger(parameterName, 0);
    }
}
