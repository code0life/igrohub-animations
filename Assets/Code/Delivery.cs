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

        Transform tfmEnd = pointDelivery.transform;
        tfmEnd.localScale.Set(1, 1, 1);

        Transform tfmStart = transform;

        Transform tfm = transform;

        while (time_passed < duration)
        {
            float t = time_passed / duration;
            float tEasing = Easing.CircularEaseIn(t);
            float tEasingPar = Easing.BackEaseInOut(tEasing);

            time_passed += Time.deltaTime;

            Vector3 pos = tfm.position;
            Vector3 scale = tfm.localScale;

            pos.x = Mathf.Lerp(tfmStart.position.x, tfmEnd.position.x, tEasing);
            pos.y = Mathf.Lerp(tfmStart.position.y, tfmEnd.position.y, tEasing);
            pos.z = Mathf.Lerp(tfmStart.position.z, tfmEnd.position.z, tEasing);

            scale.x = Mathf.Lerp(tfmStart.localScale.x, tfmEnd.localScale.x, tEasing);
            scale.y = Mathf.Lerp(tfmStart.localScale.y, tfmEnd.localScale.y, tEasing);
            scale.z = Mathf.Lerp(tfmStart.localScale.z, tfmEnd.localScale.z, tEasing);

            pos = pos + Vector3.up * Mathf.Sin(Time.deltaTime * -20f) * 20.0f;
            tfm.position = pos;
            tfm.localScale = scale;

            yield return null;
        }
    }
}
