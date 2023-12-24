using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator PlayerAnim;

    [SerializeField]private PlayerController player;
    private const string IS_WALKING="isWalking";

    private void Awake()
    {
        PlayerAnim=GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerAnim.SetBool(IS_WALKING,player.isWalking());
    }
}
