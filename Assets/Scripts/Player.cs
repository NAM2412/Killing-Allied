using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 10f;

    Vector2 moveInput;

    Camera mainCamera;
    CameraController cameraController;

    void Start()
    {
        moveStick.OnStickValueUpdated += MoveStick_OnStickValueUpdated;
        mainCamera = Camera.main; 
        cameraController = FindObjectOfType<CameraController>();
    }

    private void MoveStick_OnStickValueUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up); 

        Vector3 moveDir = rightDir * moveInput.x + upDir * moveInput.y;

        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        if (moveInput.magnitude != 0) // player is moving
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), turnLerpAlpha);
            if (cameraController != null)
            {
                cameraController.AddYawInput(moveInput.x);
            }

        }


    }
}
