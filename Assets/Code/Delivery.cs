using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameObject mainInterface;
    public GameObject pointDelivery;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToCount()
    {
        StartCoroutine(AnimateGet());
    }

    IEnumerator AnimateGet()
    {

        const float duration = 2.5f;
        float time_passed = 0.0f;

        Transform tfmEnd = pointDelivery.transform;
        Transform tfmStart = transform;

        Transform tfm = transform;

        while (time_passed < duration)
        {
            float t = time_passed / duration;
            t = Easing.CubicEaseOut(t);

            Vector3 pos = tfm.position;

            pos = Vector3.Lerp(tfmStart.position, tfmEnd.position, t);
            tfm.position = pos;

            time_passed += Time.deltaTime;
            yield return null;
        }
    }
}
