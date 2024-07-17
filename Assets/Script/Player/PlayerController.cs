using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseSingleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Knockback playerKnockback;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        transform.position = GameDataManager.Instance.CurrentLocation;
    }
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        //PlayerHealth.onPlayerDeath += DisableMovement;
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    public void DisableMovement() {
        playerControls.Disable();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        if  (this.gameObject != null) {
            GameDataManager.Instance.CurrentLocation = this.transform.position;
        }
        GameDataManager.Instance.LastWave = SceneManager.GetActiveScene().buildIndex;
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if(playerKnockback.gettingKnockedBack || PlayerHealth.Instance.isDead) {return ;}
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }
    private void Dash()
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = 2f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        Debug.Log("Start Dash CD");
        yield return new WaitForSeconds(dashCD);
        Debug.Log("Start Dash CD");
        isDashing = false;
    }
}
