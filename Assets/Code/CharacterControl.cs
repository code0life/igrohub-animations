using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Animator m_Animator;
    CapsuleCollider m_Capsule;

    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int TurningSpeed = Animator.StringToHash("TurningSpeed");
    static readonly int Grounded = Animator.StringToHash("Grounded");
    static readonly int Jump = Animator.StringToHash("Jump");
    static readonly int Crouch = Animator.StringToHash("Crouch");
    static readonly int Free = Animator.StringToHash("Free");

    public Camera MainCamera;

    public float JumpForce = 500;
    public LayerMask GroundLayer;
    public float GroundDistanceThreshold = 0.3f;
    //public float CubeDistanceThreshold = 0.3f;

    private float slidingH;
    private float slidingV;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        Vector2 smoothedInput;

        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");

        smoothedInput = SmoothInput(h, v);

        h = smoothedInput.x;
        v = smoothedInput.y;


        m_Animator.SetFloat(Speed, v);
        m_Animator.SetFloat(TurningSpeed, h);
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


    bool grounded = IsOnTheGround();
        m_Animator.SetBool(Grounded, grounded);

    if(Input.GetButtonDown("Jump") && grounded)
    {
        m_Rigidbody.AddForce(JumpForce * Vector3.up);
        m_Animator.SetTrigger(Jump);
    }

    bool crouched = IsCrouch();
    m_Animator.SetBool(Crouch, crouched);

    //bool need_up = IsFrontEmpty();
    //animator.SetBool(Free, need_up);

    }

  bool IsOnTheGround()
  {
    Vector3 origin = transform.position + 0.1f*Vector3.forward;
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

}
