using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameObject mainInterface;
    public GameObject pointDelivery;
    float time_passed = 0.0f;
    float duration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (time_passed >= duration)
        {
            GameObject.FindObjectOfType<CountPanel>().PulseCount();
            Destroy(gameObject);
        }
    }

    public void GoToCount()
    {
        StartCoroutine(AnimateGet());
    }

    IEnumerator AnimateGet()
    {

        duration = 1.5f;
        time_passed = 0.0f;

        //var height = 3.2;
        //var speed = 2.0;
        ////var timingOffset = 0.0;
        //var count = 0;

        Transform tfmEnd = pointDelivery.transform;
        Transform tfmStart = transform;

        Transform tfm = transform;

        while (time_passed < duration)
        {
            float t = time_passed / duration;
            float tEasing = Easing.CircularEaseIn(t);
            float tEasingPar = Easing.BackEaseInOut(tEasing);

            time_passed += Time.deltaTime;

            //var offset = Mathf.Sin((Time.time * speed) + timingOffset) * height / 2;

            //count++;

            //tfm.position = new Vector3(count, offset, 0);

            Vector3 pos = tfm.position;

            //tfmEnd.position.y = Mathf.Sin(t);
            //pos = pos + Vector3.up * Mathf.Sin(Time.deltaTime * 20f) * 0.5f;


            pos.x = Mathf.Lerp(tfmStart.position.x, tfmEnd.position.x, tEasing);
            //pos.y = Mathf.Lerp(tfmStart.position.y, tfmEnd.position.y + Mathf.Sin(Time.time) * Time.deltaTime, tEasing);
            pos.y = Mathf.Lerp(tfmStart.position.y, tfmEnd.position.y, tEasing);
            pos.z = Mathf.Lerp(tfmStart.position.z, tfmEnd.position.z, tEasing);

            //tfm.position = pos + transform.up * Mathf.Sin(Time.time * 20f) * 0.5f; 
            //Debug.Log("tfmEnd.position.y - " + tfmEnd.position.y);
            pos = pos + Vector3.up * Mathf.Sin(Time.deltaTime * -20f) * 20.0f;
            tfm.position = pos;

            yield return null;
        }
    }
}
