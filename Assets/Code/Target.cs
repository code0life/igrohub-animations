using UnityEngine;

public class Target : MonoBehaviour
{
   Animator animator;

  void OnTriggerEnter(Collider other)
  {
    bool is_player = other.gameObject.GetComponent<CharacterControl>() != null;
    if(is_player)
    {
      AddScore();
      Destroy(gameObject);
    }
  }


    void AddScore()
    {
    GameObject.FindObjectOfType<ScoreCounter>().Add(1);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
            //i//f (animator.runtimeAnimatorController != null)
            //{
                animator.Play("SpawnTarget");
                Debug.Log("Start");
            //}

        }

        //transform.Rotate(Vector3.forward * Random.Range(10.0f, 30.0f) * Time.deltaTime);
    }

    void Update()
    {
        //transform.Rotate(Vector3.forward * Random.Range(10.0f, 30.0f) * Time.deltaTime);
        //transform.Rotate(Vector3.left * Random.Range(10.0f, 30.0f) * Time.deltaTime);
    }
}
