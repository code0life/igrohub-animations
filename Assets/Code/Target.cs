using UnityEngine;

public class Target : MonoBehaviour
{
   Animator animator;
   public GameObject deadPfx;
   public GameObject idlePfx;

   //GameObject idleObjPfx;

    void OnTriggerEnter(Collider other)
  {
    bool is_player = other.gameObject.GetComponent<CharacterControl>() != null;
    if(is_player)
    {
      AddScore();
      GetObject();
    }
  }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("SpawnTarget");
            //idleObjPfx = Instantiate(idlePfx, transform.position, transform.rotation);

        }

    }

    void Update()
    {
    }

    void AddScore()
    {
        GameObject.FindObjectOfType<ScoreCounter>().Add(1);
    }

    void GetObject()
    {
        GameObject.FindObjectOfType<Interface>().Delivery(gameObject);
        Destroy(gameObject);
    }

}
