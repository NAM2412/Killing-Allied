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
    #endregion

    #region Monobahaviour
    void Start()
    {
        moveStick.OnStickValueUpdated += MoveStick_OnStickValueUpdated;
        aimStick.OnStickValueUpdated += AimStick_OnStickValueUpdated;

        mainCamera = Camera.main; 
        cameraController = FindObjectOfType<CameraController>();
    }
    
    void Update()
    {
        Vector3 moveDir = StickInputToWorldDirection(moveInput);
        Vector3 aimDir = moveDir;

        if (aimInput.magnitude != 0) 
        {
            aimDir = StickInputToWorldDirection(aimInput);
        }

        if (aimDir.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);
        }

        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        if (moveInput.magnitude != 0) // player is moving
        { 
            if (cameraController != null)
            {
                cameraController.AddYawInput(moveInput.x);
            }
        }
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

    Vector3 StickInputToWorldDirection (Vector2 inputValue)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);

        Vector3 worldDir = rightDir * inputValue.x + upDir * inputValue.y;

        return worldDir;
    }
}
