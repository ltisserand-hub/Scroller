using System;
using UnityEngine;
using System.Collections;
public class PlayerHealthController : MonoBehaviour
{
    [Header("Player Stats")]
    [Range(0, 60)]public float maxHealth = 20f;
    [Range(0, 60)]public float health = 20f;
    [Range(0, 100)]public float defense;
    [Range(0, 100)]public float damageResistance;
    [Header("Immunity Frame Settings")]
    public float immunityDuration = 0.5f;
    public bool immune = false;
    [Header("Respawn Settings")]
    public Vector2 respawnPosition;
    public float respawnTime = 5f;
    [Header("Visual Settings")]
    [SerializeField] private Color _deathTint;
    public Gradient _immunityGradient;
    [SerializeField] private HeartManager _heartManager;
    
    private Transform _transform;
    private PlayerControler _playerControler;
    private SpriteRenderer _spriteRenderer;

    public void Start()
    { 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerControler = GetComponent<PlayerControler>();
        _transform = GetComponent<Transform>();
        StartCoroutine(HealthRenderUpdater());
    }

    public void FixedUpdate()
    {
        if (health <= 0f && _playerControler.isActive )
        {
            StartCoroutine(Death());
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    
    public IEnumerator Death()
    {
        //play death animation
        _spriteRenderer.color = _deathTint;
        _playerControler.isActive = false;
        
        yield return new WaitForSeconds(respawnTime);
        
        health = maxHealth;
        _spriteRenderer.color = Color.white;
        _transform.position = respawnPosition;
        _playerControler.isActive = true;
    }
    
    public void TakeDamage(float _damage, bool _trueDamage = false ) //normal damage formula 
    {
        if (!immune && _playerControler.isActive)
        {
            if (_trueDamage)
            {
                health -= _damage;
            }
            else
            {
                health -= Mathf.Clamp(_damage - defense, 0,10000 )*(1-(damageResistance/100));
            }
            _heartManager.UpdateHealth((int)MathF.Ceiling(health), (int)MathF.Ceiling(maxHealth));
            if (health > 0)
            {
                StartCoroutine(ImmunityFrames());
            }
        }
    }
    
    private IEnumerator ImmunityFrames()
    {
        immune = true;
        float _time = 0f;
        while (_time < immunityDuration)
        {
            _time += Time.deltaTime;
            _spriteRenderer.color = _immunityGradient.Evaluate(Mathf.PingPong(_time,0.25f));
            yield return new WaitForEndOfFrame();
        }
        _spriteRenderer.color = Color.white;
        immune = false;
    }

    private IEnumerator HealthRenderUpdater()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _heartManager.UpdateHealth((int)MathF.Ceiling(health), (int)MathF.Ceiling(maxHealth));
        }
    }
}
