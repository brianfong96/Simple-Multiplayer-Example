using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    private NetworkStartPosition[] spawnPoints;

    public const int MAX_HEALTH = 100;    
    public RectTransform HealthBar;
    public bool DestroyOnDeath;

    //[SyncVar(hook ="OnChangeHealth")]
    [SyncVar]
    public int CurrentHealth = MAX_HEALTH;

    private void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

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
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick a spawn point at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
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
