using UnityEngine;

public class Target : MonoBehaviour
{
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
}
