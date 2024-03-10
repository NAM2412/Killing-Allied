using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 10f;

    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCamera;
    CameraController cameraController;
    Animator animator;
    #endregion

    #region Monobahaviour
    void Start()
    {
        moveStick.OnStickValueUpdated += MoveStick_OnStickValueUpdated;
        aimStick.OnStickValueUpdated += AimStick_OnStickValueUpdated;

        mainCamera = Camera.main; 
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        MoveAndAim();
        UpdateCamera();
    }
    #endregion
    #region OnStickValueUpdate Event
    private void AimStick_OnStickValueUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
    }

    private void MoveStick_OnStickValueUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }
    #endregion
    private void MoveAndAim()
    {
        Vector3 moveDir = StickInputToWorldDirection(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        UpdateAim(moveDir);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            aimDir = StickInputToWorldDirection(aimInput);
        }
        RotateToward(aimDir);
    }

    private void UpdateCamera()
    {
        // Player is moving but not aiming
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateToward(Vector3 aimDir)
    {
        if (aimDir.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);
        }
    }

    private Vector3 StickInputToWorldDirection (Vector2 inputValue)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);

        Vector3 worldDir = rightDir * inputValue.x + upDir * inputValue.y;

        return worldDir;
    }
}
