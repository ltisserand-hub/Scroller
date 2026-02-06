using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _isTrueDamage;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            try
            {
                collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(_damage, _isTrueDamage);
            }
            catch
            {
                Debug.LogWarning("Collision With Player with no player health controller");
            }
        }
    }
}
