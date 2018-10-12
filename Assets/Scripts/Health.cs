using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {


    public const int MAX_HEALTH = 100;    
    public RectTransform HealthBar;

    [SyncVar(hook ="OnChangeHealth")]
    public int CurrentHealth = MAX_HEALTH;

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("Dead!");
        }
        
    }

    void OnChangeHealth(int health)
    {
        HealthBar.sizeDelta = new Vector2(health, HealthBar.sizeDelta.y);
    }

}
