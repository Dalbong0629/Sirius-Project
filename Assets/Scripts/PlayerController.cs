using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float walkSpeed = 1f;
    public float runSpeed = 10f;

    [Header("점프 높이")]
    public float jumpHeight = 3f;
    public float doubleJumpHeight = 2f;

    [Header("달리기 조건")]
    public float doubleTapTime = 0.6f;   // 더블탭 허용 시간
    private float lastKeyTime = -1f;
    private KeyCode lastKey;
    private float currentSpeed;          
    private bool isRunning = false;      

    [Header("상호작용 키")]
    public KeyCode interactKey = KeyCode.E;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded = true;
    private bool canDoubleJump = true;

    [Header("UI 연결")]
    public GameObject inventoryPanel;
    public GameObject mapPanel;
    public GameObject menuPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed; 

        // UI 초기 비활성화
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (mapPanel != null) mapPanel.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(false);
    }

    void Update()
    {
        HandleInput();
       
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        horizontalInput = 0f;
        verticalInput = 0f;

        // 좌/우 이동
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
            HandleRun(KeyCode.A);
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
            HandleRun(KeyCode.D);
        }

        // Z축 이동
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
            HandleRun(KeyCode.W); 
        }
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f;

        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                Jump();
            else if (canDoubleJump)
                DoubleJump();
        }

        // 상호작용
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }

        // 인벤토리
        if (Input.GetKeyDown(KeyCode.I) && inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

        // 지도
        if (Input.GetKeyDown(KeyCode.Tab) && mapPanel != null)
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }

        // 메뉴
        if (Input.GetKeyDown(KeyCode.Escape) && menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }

        // 능력 1
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseAbility1();
        }

        // 마우스 좌클릭 (공격)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // 마우스 우클릭 (낚시)
        if (Input.GetMouseButtonDown(1))
        {
            CastFishing();
        }

        // 마우스 휠
        if (Input.mouseScrollDelta.y > 0)
        {
            UseAbility3();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            UseAbility2();
        }
    }

    void Move()
    {
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput).normalized * currentSpeed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        canDoubleJump = true;
        isGrounded = false;
    }

    void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, doubleJumpHeight, rb.velocity.z);
        canDoubleJump = false;
    }

    void HandleRun(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            // 더블탭 체크
            if (lastKey == key && (Time.time - lastKeyTime) <= doubleTapTime)
            {
                isRunning = true;
            }

            lastKey = key;
            lastKeyTime = Time.time;
        }

        // 키 누르고 있는 동안
        if (Input.GetKey(key))
        {
            if (isRunning)
                currentSpeed = runSpeed;
            else
                currentSpeed = walkSpeed;
        }

        // 키 뗐을 때 → 바로 걷기로 복귀
        if (Input.GetKeyUp(key))
        {
            isRunning = false;      // 달리기 상태 해제
            currentSpeed = walkSpeed;
        }
    }

    void Interact()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Interactable"))
            {
                hit.GetComponent<Interactable>()?.Interact();
            }
        }
    }

    void UseAbility1() { Debug.Log("능력 1 사용"); }
    void UseAbility2() { Debug.Log("능력 2 사용"); }
    void UseAbility3() { Debug.Log("능력 3 사용"); }
    void Attack() { Debug.Log("공격"); }
    void CastFishing() { Debug.Log("낚시"); }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
