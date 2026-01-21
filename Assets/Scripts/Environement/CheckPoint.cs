using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isActive = true;
    private Vector3 respawnPos;

    void Start()
    {
        respawnPos = GetComponent<Transform>().position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (player.GetComponent<DeathRespawn>().respawnPosition != respawnPos)
        {
            if (collision.gameObject.tag == "Player" || isActive) 
            {
                player.GetComponent<DeathRespawn>().respawnPosition = new Vector3(respawnPos.x, respawnPos.y, 0);
                //play animation
            }
        }
        else
        {
            
        }
    }
}
