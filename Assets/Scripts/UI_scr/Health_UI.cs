using TSwap.Collisions.Interactions;
using UnityEngine;

public class Health_UI : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
    [SerializeField] float sizePerHeart;
    [SerializeField] DamageTaker collisioner;

    private void OnEnable() => collisioner.OnTakeDamage += UpdateHealthUI;
    private void OnDisable() => collisioner.OnTakeDamage -= UpdateHealthUI;
    
    private void UpdateHealthUI(int currentHealth)
    {
        Vector2 newSizeDelta = healthBar.sizeDelta;
        newSizeDelta.x = currentHealth * sizePerHeart;
        healthBar.sizeDelta = newSizeDelta;
    }
}
