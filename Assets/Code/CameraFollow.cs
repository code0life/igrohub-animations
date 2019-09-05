using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Vector3 offset = new Vector3(0, 2, -3);
  public Camera cam;

  void LateUpdate()
  {
    cam.transform.position = transform.position + 
                             offset.z*transform.forward + 
                             offset.y*transform.up;
    cam.transform.LookAt(transform);
  }
}
