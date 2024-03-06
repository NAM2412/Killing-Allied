using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    Vector2 moveInput;
    

    void Start()
    {
        moveStick.OnStickValueUpdated += MoveStick_OnStickValueUpdated ;
    }

    private void MoveStick_OnStickValueUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed);
    }
}
