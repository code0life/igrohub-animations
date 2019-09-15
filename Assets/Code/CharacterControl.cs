using UnityEngine;
using System.Collections;

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

    public GameObject MainCamera;

    public bool btnJump;
    public bool btnCrouch;

    public bool jumpState;
    public bool crouchState;
    public bool blockJump;

    private Vector3 moveVector;
    private bool isGrounded;
    private Vector3 moveGroundNormal;

    public float JumpForce = 10f;
    public LayerMask GroundLayer;
    public float ConstGroundDistanceThreshold = 0.3f;
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

    }

    bool IsOnTheGround()
    {
        RaycastHit hit;
        bool is_ground;

        Vector3 origin = transform.position + (Vector3.up * 0.1f);
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(origin, direction, out hit, GroundDistanceThreshold))
        {
            moveGroundNormal = hit.normal;
            is_ground = true;
            animator.applyRootMotion = true;
        }
        else
        {
            is_ground = false;
            moveGroundNormal = Vector3.up;
            animator.applyRootMotion = false;
        }

        return is_ground;
    }

    bool IsCrouch()
    {
        bool flag = false;

        if (Input.GetButton("Crouch"))
        {
            flag = true;
        }

        return flag;
    }

    public void Move(float h, float v, bool jump, bool crouch)
    {

        moveVector = v * Vector3.forward + h * Vector3.right;
        moveVector = Vector3.ProjectOnPlane(moveVector, moveGroundNormal);
        CapsuleScale();
        JumpState();

        UpdateAnimation(moveVector);

    }

    void JumpState()
    {

        if (isGrounded & !blockJump)
        {
            if (btnJump && (animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion") || animator.GetCurrentAnimatorStateInfo(0).IsName("Crouch")) )
            {
                rigidbody.AddForce(JumpForce * Vector3.up);
                jumpState = true;
            }
            else
            {
                Vector3 extraGravityForce = (Physics.gravity * 2f) - Physics.gravity;
                rigidbody.AddForce(extraGravityForce);
                jumpState = false;
            }
        }

    }

    void UpdateAnimation(Vector3 move)
    {
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
                blockJump = true;
                return;
            }
            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;
            crouchState = false;
            blockJump = false;
        }
    }

}
