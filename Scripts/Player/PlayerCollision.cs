using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public bool isOnGround;
    [HideInInspector] public bool isOnObstacle; // This bool makes sure the player can only jump and not move side-to-side whilst on an obstacle
    [HideInInspector] public bool isInObstacle; // This bool is only here for the invincibility cheat

    [SerializeField] private bool isInvincible;
    [SerializeField] private GameObject scorePopupTextPrefab;
    [SerializeField] private GameObject gemPopupTextPrefab;
    private PlayerAnimations playerAnimations;
    private PlayerCurrentScore playerCurrentScore;
    private PlayerCurrentCoinAmount playerCurrentCoinAmount;
    private PlayerMovement playerMovement;
    private PlayerVFX playerVFX;
    private GameObject scoreText, coinText;
    private float onObstacleFalseDelay = 0.5f;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerCurrentScore = GetComponent<PlayerCurrentScore>();
        playerCurrentCoinAmount = GetComponent<PlayerCurrentCoinAmount>();
        playerMovement = GetComponent<PlayerMovement>();
        playerVFX = GetComponent<PlayerVFX>();
    }

    private void Start()
    {
        scoreText = GameGUIManager.Instance.scoreText;
        coinText = GameGUIManager.Instance.coinText;
        isInvincible = CheatManager.Instance.invincible;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerAnimations.Land();
            isOnGround = true;
        }

        if ((other.CompareTag("SingleLaneObs") || other.CompareTag("MultiLaneObs")) && !isInvincible)
        {
            if (GameController.Instance.gameActive)
            {
                GameController.Instance.GameOver(); // Prevents doubling up on game-overs if collider is still touching obstacle
                SoundController.Instance.PlaySound(SoundType.Hit);
                playerAnimations.Death();
                playerVFX.PlaySmokeExplosion();
            }

            if (playerMovement.IsMoving())
            {
                playerMovement.HitObstacleWhileMoving();
            }
        }

        if (other.CompareTag("Coin"))
        {
            playerCurrentScore.IncreaseScoreViaCoin();
            playerCurrentCoinAmount.IncreaseCoinAmount();
            SoundController.Instance.PlaySound(SoundType.Coin);

            Instantiate(scorePopupTextPrefab, scoreText.transform); // To change score text, edit the Score Popup prefab
            Instantiate(gemPopupTextPrefab, coinText.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("SingleLaneObs") || other.CompareTag("MultiLaneObs")) && isInvincible)
        {
            isInObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("SingleLaneObs") || other.CompareTag("MultiLaneObs")) && isInvincible)
        {
            isInObstacle = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Enables the ability for the player to jump off valid obstacles (if game is not over)
        if (collision.gameObject.CompareTag("SingleLaneObs") || collision.gameObject.CompareTag("MultiLaneObs"))
        {
            playerAnimations.Land();
            isOnObstacle = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SingleLaneObs") || collision.gameObject.CompareTag("MultiLaneObs"))
        {
            StartCoroutine(MakeIsOnObstacleFalse());
        }
    }

    private IEnumerator MakeIsOnObstacleFalse()
    {
        yield return new WaitForSeconds(onObstacleFalseDelay);
        isOnObstacle = false;
    }
}
