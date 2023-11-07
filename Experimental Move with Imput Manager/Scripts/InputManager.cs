using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFootAct;
    private PlayerMotor motor;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFootAct = playerInput.OnFoot;
        motor=GetComponent<PlayerMotor>();
        onFootAct.Jump.canceled += ctx => motor.Jump();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFootAct.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFootAct.Enable();
    }

    private void OnDisable()
    {
        onFootAct.Disable();
    }
}
