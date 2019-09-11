using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset_real = new Vector3(0, 0, 0);

    Vector3 offset = new Vector3(0, 2, -3);
    Vector3 offset_tunnel = new Vector3(0, 1, -3);
    Vector3 offset_tunnel_enter = new Vector3(0, 0.5f, -3);

    float fieldOfViewMax = 60;
    public Camera cam;

    public float smooth = 0.3f;
    //public GameObject target;//объект сопровождения

    public bool is_tunnel;
    public bool is_crouch;

    void Start()
    {
        offset_real = offset;
        cam.transform.position = transform.position +
                                    offset_real.z * transform.forward +
                                    offset_real.y * transform.up;

    }


    void LateUpdate()
    {
    }
    void FixedUpdate()
    {
        is_crouch = GetComponent<CharacterControl>().crouchState;

        Vector3 pos = Vector3.zero;

        if (is_tunnel)
        {
            if (is_crouch)
            {
                offset_real = offset_tunnel_enter;
                fieldOfViewMax = 30;
            }
            else
            {
                offset_real = offset_tunnel;
                fieldOfViewMax = 70;
            }

        }
        else
        {
            offset_real = offset;
            fieldOfViewMax = 60;
        }
        
        pos = transform.position +
                                    offset_real.z*transform.forward +
                                    offset_real.y*transform.up;

        cam.transform.position = Vector3.Lerp(cam.transform.position, pos, smooth);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fieldOfViewMax, smooth);

        cam.transform.LookAt(transform);
    }

    public void GoToTunnel()
    {
        is_tunnel = true;
    }

    public void ExitTunnel()
    {
        is_tunnel = false;
    }

}
