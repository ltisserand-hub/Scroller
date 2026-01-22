using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public bool isActive = true;
    [SerializeField] private MonoBehaviour script;
    private Vector2 respawnPos;

    void Start()
    {
        respawnPos = GetComponent<Transform>().position;
    }

    void Update()
    {
        if (script.enabled && player.GetComponent<DeathRespawn>().respawnPosition != respawnPos )
        {
            script.enabled = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            print("toggle off");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (player.GetComponent<DeathRespawn>().respawnPosition != respawnPos)
        {
            if (collision.gameObject.tag == "Player" || isActive) 
            {
                player.GetComponent<DeathRespawn>().respawnPosition = new Vector3(respawnPos.x, respawnPos.y, 0);
                //play animation
                script.enabled = true;
            }
        }
    }
}
