using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchingSpeed = 5.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private bool canJump = true;

    private Animator _animator;
    
    private bool _isCrouching;
    private static readonly int Crouching = Animator.StringToHash("Crouching");

    public bool Crouch
    {
        set
        {
            _isCrouching = value;
            canJump = !_isCrouching;
            _animator.SetBool(Crouching, _isCrouching);
        }
    }
    
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float baseSpeed = (_isCrouching ? crouchingSpeed : (isRunning ? runningSpeed : walkingSpeed));
        float curSpeedX = canMove ? baseSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? baseSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && canJump)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }


    private bool dead = false;

    public void Die(Transform respawnPoint = null)
    {
        if (dead) return;
        StartCoroutine(DieCoroutine(respawnPoint));
    }
    
    IEnumerator DieCoroutine(Transform respawnPoint = null)
    {
        dead = true;
        canMove = false;
        _animator.SetTrigger("Die");
        
        float fonduIn = 1f;
        float fonduOut = 1f;
        float blackDuration = 2f;

        for (float d = 0; d < fonduIn; d += Time.deltaTime)
        {
            GameManager.singleton.mainCanvas.blackImage.alpha = d / fonduIn;
            yield return 0;
        }

        if (respawnPoint == null) respawnPoint = GameManager.singleton.spawnPoint;

        characterController.enabled = false;
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
        characterController.enabled = true;
        
        yield return new WaitForSeconds(blackDuration);

        for (float d = 0; d < fonduOut; d += Time.deltaTime)
        {
            GameManager.singleton.mainCanvas.blackImage.alpha = 1 - d / fonduOut;
            yield return 0;
        }

        canMove = true;
        dead = false;
    }
}