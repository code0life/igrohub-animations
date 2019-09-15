using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mainInterface;
    public Canvas canvas;
    public GameObject pointDelivery;
    public GameObject simpleObj;
    public GameObject persObj;

    Vector3 Offset = Vector3.zero;

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
        clone = Instantiate(simpleObj, _obj.transform.position, Quaternion.identity);
        clone.SetActive(true);

        clone.transform.SetParent(mainInterface.transform, true);
        clone.transform.position = worldToUISpace(canvas, _obj.transform.position);
        Vector3 viewOrigSize = _obj.transform.localScale;
        Vector3 viewScreenSize = mainCamera.WorldToScreenPoint(_obj.transform.position);

        float size = GetWidth(clone.GetComponent<RectTransform>()) + 20;

        float getOffsetCamera = persObj.GetComponent<CameraFollow>().GetOffset();
        Vector3 newSize = new Vector3( ( ( viewScreenSize.x / viewScreenSize.z ) / size ) / getOffsetCamera,( ( viewScreenSize.y / viewScreenSize.z) / size) / getOffsetCamera, ( viewScreenSize.z / viewScreenSize.z ) / getOffsetCamera);

        clone.transform.localScale = newSize;

        Offset = clone.transform.position - worldToUISpace(canvas, _obj.transform.position);

        clone.gameObject.GetComponent<Delivery>().GoToCount();
    }

    public Vector3 worldToUISpace(Canvas canvas, Vector3 worldPos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);
        return canvas.transform.TransformPoint(movePos);
    }

    public float GetWidth(RectTransform trans)
    {
        return trans.rect.width;
    }

}
