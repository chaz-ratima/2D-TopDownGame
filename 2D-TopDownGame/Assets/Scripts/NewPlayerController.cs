using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Input script using the input system.
public class NewPlayerController : MonoBehaviour
{
    #region ListOfVariables
    // Get playerInput c#
    private PlayerInput playerControls;
    public SwordAttackHorizontal swordAttackH;
    public SwordAttackVertical swordAttackV;

    Rigidbody2D rb;
    Vector2 movementInput;
    private Vector3 moveDir;
    Animator animator;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float dashSpeed = 20f;
    public float moveSpeed;
    public bool isSprinting;
    private bool canDash = true;
    private bool n_canMove = true;
    public bool canAttack = true;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    // creates list of collisions and will create new based on collisions
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    #endregion

    #region PlayerStates
    public enum PlayerStates
    {
        IDLE,
        WALK,
        ATTACK
    }

    // State of player through animations
    PlayerStates CurrentState
    {
        set
        {
            if(n_stateLock == false)
            {
                n_currentState = value;

                switch (n_currentState)
                {
                    case PlayerStates.IDLE:
                        animator.Play("PlayerIdle");
                        n_canMove = true;
                        break;
                    case PlayerStates.WALK:
                        animator.Play("PlayerWalk");
                        n_canMove = true;
                        break;
                    case PlayerStates.ATTACK:
                        animator.Play("PlayerAttack");
                        n_stateLock = true;
                        n_canMove = false;
                        break;
                }
            }
        }
    }

    PlayerStates n_currentState;
    // when this is true, the state should not change on the character
    bool n_stateLock = false;
    #endregion

    #region Awake / Start / Update / FixedUpdate
    private void Awake()
    {
        playerControls = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        //playerControls.Player.Sprint.performed += x => SprintPressed();
        //playerControls.Player.Sprint.canceled += x => SprintReleased();
    }

    void Start()
    {
        // normalizes  -- not sure if can remove from here and install at better place.
        movementInput = movementInput.normalized;
        animator = GetComponent<Animator>();
        moveDir = new Vector3(movementInput.x, movementInput.y).normalized;
    }

    void FixedUpdate()
    {
        CheckForMovement();
    }
    #endregion

    #region Enable / Disable
    // Enable the playerControls and disables -- need to do or nothing happens
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion

    #region Movement / DirectionFacing
    void CheckForMovement()
    {
        // if can move and pressing buttons then move
        // if not successful, will see if movement up and down is possible, then check left and right.
        if (n_canMove && movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }
            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Raycasting based on direction
        // will get the collision and set a number of how many collisions are in direction of player
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime * collisionOffset);
        // if no collisions then move
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    // do when moving
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        // if canMove = true and movementInput > 0 || < 0 set direction to the x and y of player.
        if (n_canMove != false && movementInput != Vector2.zero){
            CurrentState = PlayerStates.WALK;
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
        } else { CurrentState = PlayerStates.IDLE; }
    }

    void OnSprint()
    {
        if (canDash)
        {
            moveSpeed = dashSpeed;
            canDash = false;
            StartDashTimer();
        }
    }

    void StartDashTimer()
    {
        StartCoroutine(DashTimer());
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(0.2f);
        moveSpeed = walkSpeed;
        yield return new WaitForSeconds(1);
        canDash = true;
    }

    // two scripts that are called when shift is pressed to change value of moveSpeed
    /*void SprintPressed(){
        moveSpeed = runSpeed;
    }

    void SprintReleased(){
        moveSpeed = walkSpeed;
    }*/
    #endregion

    #region Attacking
    // Attack whenever the space button is pressed
    void OnMeleeAttack(){
        if (canAttack)
        {
            CurrentState = PlayerStates.ATTACK;
        }

    }

    //Returns control of the player state to the player controller
    void OnAttackFinished(){
        n_stateLock = false;
        n_canMove = true;

        if (swordAttackV.attacking || swordAttackH.attacking)
        {
            swordAttackV.attacking = false;
            swordAttackH.attacking = false;
            swordAttackV.swordColliderVertical.enabled = false;
            swordAttackH.swordColliderHorizontal.enabled = false;
        }

        // check if moving and change state based on result
        if (movementInput != Vector2.zero)
        {
            CurrentState = PlayerStates.WALK;
        } else { CurrentState = PlayerStates.IDLE; }

        StartAttackDelay();
    }

    void StartAttackDelay()
    {
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
    }

    // directional attacking funtions
    void AttackUpCaller()
    {
        swordAttackV.AttackUp();
    }

    void AttackDownCaller()
    {
        swordAttackV.AttackDown();
    }

    void AttackLeftCaller()
    {
        swordAttackH.AttackLeft();
    }

    void AttackRightCaller()
    {
        swordAttackH.AttackRight();
    }

    #endregion
}

