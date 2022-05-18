using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] WeaponController weaponController;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    PlayerControls.MenusActions menus;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        menus = controls.Menus;

        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
        groundMovement.Attack.performed += _ => weaponController.OnAttackPressed();
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        menus.Pause.performed += _ => PauseMenu.isPaused = !PauseMenu.isPaused;
    }

    private void FixedUpdate()
    {
        movement.RecieveInput(horizontalInput);
        mouseLook.RecieveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
