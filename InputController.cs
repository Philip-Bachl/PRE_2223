using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed = 7;
    [SerializeField]
    float jumpForce = 7;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    LayerMask teleportLayer;


    Camera camera;
    Vector3 mousePos;
    Vector2 moveInput;

    Controls controls;
    private void Awake()
    {
        controls = new Controls();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput.x * speed, player.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.PlayerMove.performed += OnPlayerMove;
        controls.Gameplay.PlayerMove.canceled += OnPlayerMove;
        controls.Gameplay.PlayerJump.performed += OnPlayerJump;
        controls.Gameplay.MouseMove.performed += OnMouseMove;
        controls.Gameplay.MouseClick.performed += OnMouseClick;
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnPlayerJump(InputAction.CallbackContext context)
    {
        RaycastHit2D groundCheck = Physics2D.BoxCast(player.transform.position,player.transform.lossyScale, 0, new Vector2(0, -1), 0.2f, groundLayer);
        if (groundCheck.transform)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpForce);    
        }
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 10));
    }
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        RaycastHit2D grappleCheck = Physics2D.Raycast(player.transform.position, new Vector2(mousePos.x - player.transform.position.x, mousePos.y - player.transform.position.y), Mathf.Infinity, teleportLayer);
        if (grappleCheck.collider)
        {
            if (grappleCheck.collider.gameObject.tag == "TeleportSwitch")
            {
                Vector3 playerPosition = player.transform.position;
                player.transform.position = grappleCheck.collider.gameObject.transform.position;
                grappleCheck.collider.gameObject.transform.position = playerPosition;
            }
            else
            {
                player.transform.position = grappleCheck.point;
            }
        }
    }
}
