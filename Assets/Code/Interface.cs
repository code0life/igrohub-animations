using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mainInterface;
    //public GameObject pointDelivery;
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
    }

    public void Delivery(GameObject _obj)
    {
        Vector3 pos_start = _obj.transform.position;
        //Vector3 pos_end = pointDelivery.transform.position;

        Vector3 thePosition;
        thePosition = transform.TransformDirection(pos_start);
        //Vector3 theEndPosition;
        //theEndPosition = transform.TransformDirection(pos_end);
        //theEndPosition = pos_end;

        clone = Instantiate(simpleObj, simpleObj.transform.position, transform.rotation);
        clone.SetActive(true);
        clone.transform.SetParent(mainInterface.transform, true);
        clone.gameObject.GetComponent<Delivery>().GoToCount();
    }

}
