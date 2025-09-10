using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] int health = 10;
    public int Health => health;
    public bool vulnerable = true;
    [SerializeField] bool isDummy;
    [SerializeField] TextMeshProUGUI damageDone;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath onPlayerDeath;

    public void takeDamage(int damage)
    {
        if (isDummy)
        {
            Debug.Log("dummy took " + damage + " damage");
            if (damageDone != null)
            {
                damageDone.enabled = true;
                damageDone.text = damage.ToString();
            }
            return;
        }
        if (vulnerable)
        {
            
            health -= damage;
            if (health <= 0)
            {
                onPlayerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
    public void heal(int hp)
    {
        health += hp;
    }
}
