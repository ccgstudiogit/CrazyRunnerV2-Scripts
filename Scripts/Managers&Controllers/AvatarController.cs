using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private GameObject[] avatars;
    private int[] avatarIndex;

    private void Awake()
    {
        avatarIndex = GetAvatarIndex();
    }

    private void Start()
    {
        ActivateSelectedAvatar();

        AvatarManager.Instance.avatarChanged += ActivateSelectedAvatar;
    }

    private void OnDisable()
    {
        AvatarManager.Instance.avatarChanged -= ActivateSelectedAvatar;
    }

    private void ActivateSelectedAvatar()
    {
        DeactivateAvatars();

        for (int i = 0; i < avatarIndex.Length; i++)
        {
            if (avatarIndex[i] == PlayerPrefs.GetInt("avatar", 0))
            {
                avatars[i].SetActive(true);
            }
        }
    }

    private void DeactivateAvatars()
    {
        foreach (GameObject avatar in avatars)
        {
            avatar.SetActive(false);
        }
    }

    private int[] GetAvatarIndex()
    {
        int[] index = new int[avatars.Length];

        for (int i = 0; i < avatars.Length; i++)
        {
            index[i] = i;
        }

        return index;
    }
}
