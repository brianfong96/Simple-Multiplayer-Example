using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public const int MAX_HEALTH = 100;
    public int CurrentHealth = MAX_HEALTH;
    public RectTransform HealthBar;

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("Dead!");
        }
        HealthBar.sizeDelta = new Vector2(CurrentHealth, HealthBar.sizeDelta.y);
    }
}
