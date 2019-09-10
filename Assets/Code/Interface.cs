using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mainInterface;
    public GameObject pointDelivery;
    public GameObject simpleObj;

    GameObject clone;

    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (animator != null)
        //{
        //    if (animator.GetBool(Animated))
        //    {
        //        animator.SetBool(Animated, false);
        //    }

        //}
    }

    public void Delivery(GameObject _obj)
    {
        Transform pos_start = _obj.transform;
        //Vector3 pos_end = pointDelivery.transform.position;

        Vector3 thePosition;
        thePosition = pos_start.InverseTransformPoint(mainInterface.transform.position + Vector3.up * 120);

        clone = Instantiate(simpleObj, thePosition, transform.rotation);
        clone.SetActive(true);
        clone.transform.SetParent(mainInterface.transform, true);
        clone.gameObject.GetComponent<Delivery>().GoToCount();
    }

}
