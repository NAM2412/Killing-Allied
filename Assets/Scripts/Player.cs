using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    Vector2 moveInput;

    Camera mainCamera;

    void Start()
    {
        moveStick.OnStickValueUpdated += MoveStick_OnStickValueUpdated;
        mainCamera = Camera.main; 
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
        characterController.Move((rightDir * moveInput.x + upDir * moveInput.y) * Time.deltaTime * moveSpeed);
    }
}
