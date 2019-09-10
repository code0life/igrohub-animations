using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rigidbody;
    Animator animator;
    CapsuleCollider capsule;

    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int TurningSpeed = Animator.StringToHash("TurningSpeed");
    static readonly int Grounded = Animator.StringToHash("Grounded");
    static readonly int Jump = Animator.StringToHash("Jump");
    static readonly int Crouch = Animator.StringToHash("Crouch");
    //static readonly int Free = Animator.StringToHash("Free");

    public GameObject MainCamera;

    private bool btnJump;
    private bool btnCrouch;

    private bool jumpState;
    private bool crouchState;
    private bool blockJump;

    private Vector3 moveVector;
    private bool isGrounded;

    public float JumpForce = 350;
    public LayerMask GroundLayer;
    public float GroundDistanceThreshold = 0.3f;

    const float half = 0.5f;
    float capsuleHeight;
    Vector3 capsuleCenter;

    private float slidingH;
    private float slidingV;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;
    }

    void FixedUpdate()
    {
        Vector2 smoothedInput;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        smoothedInput = SmoothInput(h, v);

        h = smoothedInput.x;
        v = smoothedInput.y;

        bool grounded = IsOnTheGround();

        Move(h, v, btnJump, btnCrouch);
    }

    private Vector2 SmoothInput(float targetH, float targetV)
    {
        float sensitivity = 3f;
        float deadZone = 0.001f;

        slidingH = Mathf.MoveTowards(slidingH,
                      targetH, sensitivity * Time.deltaTime);

        slidingV = Mathf.MoveTowards(slidingV,
                      targetV, sensitivity * Time.deltaTime);

        return new Vector2(
               (Mathf.Abs(slidingH) < deadZone) ? 0f : slidingH,
               (Mathf.Abs(slidingV) < deadZone) ? 0f : slidingV);
    }

    void Update()
    {

        btnJump = Input.GetButtonDown("Jump");
        btnCrouch = Input.GetKey(KeyCode.C);
        isGrounded = IsOnTheGround();
        //    m_Animator.SetBool(Grounded, grounded);

        //if(Input.GetButtonDown("Jump") && grounded)
        //{
        //    m_Rigidbody.AddForce(JumpForce * Vector3.up);
        //    m_Animator.SetTrigger(Jump);
        //}

        //bool crouched = IsCrouch();
        //m_Animator.SetBool(Crouch, crouched);

        //bool need_up = IsFrontEmpty();
        //animator.SetBool(Free, need_up);

    }

    bool IsOnTheGround()
    {
        Vector3 origin = transform.position + 0.1f * Vector3.forward;
        Vector3 direction = Vector3.down;
        return Physics.Raycast(origin, direction, GroundDistanceThreshold, GroundLayer);
    }

    bool IsCrouch()
    {
        bool flag = false;
        //bool grounded = IsOnTheGround();

        if (Input.GetButton("Crouch"))
        {
            flag = true;
        }

        return flag;
    }

    public void Move(float h, float v, bool jump, bool crouch)
    {

        moveVector = v * Vector3.forward + h * Vector3.right;
        CapsuleScale();
        JumpState();

        UpdateAnimation(moveVector);

    }

    void JumpState()
    {
        if (isGrounded & btnJump)
        {
            if (jumpState) return;
            //rigidbody.AddForce(JumpForce * Vector3.up);
            jumpState = true;
        }
        else
        {
            jumpState = false;
        }
    }

    void UpdateAnimation(Vector3 move)
    {
        //m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetBool("Crouch", m_Crouching);
        //m_Animator.SetBool("OnGround", m_IsGrounded);
        animator.SetFloat(Speed, move.z);
        animator.SetFloat(TurningSpeed, move.x);
        animator.SetBool(Crouch, crouchState);
        animator.SetBool(Jump, jumpState);
        animator.SetBool(Grounded, isGrounded);
    }

    void CapsuleScale()
    {
        if (isGrounded && btnCrouch)
        {
            if (crouchState) return;
            capsule.height = capsule.height / 2f;
            capsule.center = capsule.center / 2f;
            crouchState = true;
        }
        else
        {
            Ray cRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * half, Vector3.up);
            float cRayLength = capsuleHeight - capsule.radius * half;
            if (Physics.SphereCast(cRay, capsule.radius * half, cRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouchState = true;
                return;
            }
            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;
            crouchState = false;
        }
    }

}
