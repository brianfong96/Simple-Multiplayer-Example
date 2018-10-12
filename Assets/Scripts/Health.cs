using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {


    public const int MAX_HEALTH = 100;    
    public RectTransform HealthBar;
    public bool DestroyOnDeath;

    //[SyncVar(hook ="OnChangeHealth")]
    [SyncVar]
    public int CurrentHealth = MAX_HEALTH;

    public void TakeDamage(int amount)
    {
        if (!isServer) return;
        
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            if (DestroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                CurrentHealth = MAX_HEALTH;
                RpcRespawn();
            }            
        }        
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
    
    private void Update()
    {
        HealthBar.sizeDelta = new Vector2(CurrentHealth, HealthBar.sizeDelta.y);
    }

    //void OnChangeHealth(int health)
    //{
    //    HealthBar.sizeDelta = new Vector2(health, HealthBar.sizeDelta.y);
    //}

}
