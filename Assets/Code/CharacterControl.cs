using UnityEngine;

public class CharacterControl : MonoBehaviour
{
  Animator animator;
  Rigidbody rigidbody;
  
  static readonly int Speed = Animator.StringToHash("Speed");
  static readonly int TurningSpeed = Animator.StringToHash("TurningSpeed");
  static readonly int Grounded = Animator.StringToHash("Grounded");
  static readonly int Jump = Animator.StringToHash("Jump");

  public float JumpForce = 500;
  public LayerMask GroundLayer;
  public float GroundDistanceThreshold = 0.3f;

  void Start()
  {
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    var v = Input.GetAxis("Vertical");
    var h = Input.GetAxis("Horizontal");
    animator.SetFloat(Speed, v);
    animator.SetFloat(TurningSpeed, h);

    bool grounded = IsOnTheGround();
    animator.SetBool(Grounded, grounded);

    if(Input.GetButtonDown("Jump") && grounded)
    {
      rigidbody.AddForce(JumpForce * Vector3.up);
      animator.SetTrigger(Jump);
    }
  }

  bool IsOnTheGround()
  {
    Vector3 origin = transform.position + 0.1f*Vector3.up;
    Vector3 direction = Vector3.down;
    return Physics.Raycast(origin, direction, GroundDistanceThreshold, GroundLayer);
  }
}
