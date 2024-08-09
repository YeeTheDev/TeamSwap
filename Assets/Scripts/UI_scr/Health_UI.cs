using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSwap.Collisions;

public class Health_UI : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
    [SerializeField] float sizePerHeart;
    [SerializeField] Collisioner collisioner;

    private void OnEnable() => collisioner.OnTakeDamage += UpdateHealthUI;
    private void OnDisable() => collisioner.OnTakeDamage -= UpdateHealthUI;
    
    private void UpdateHealthUI(int currentHealth)
    {
        Vector2 newSizeDelta = healthBar.sizeDelta;
        newSizeDelta.x = currentHealth * sizePerHeart;
        healthBar.sizeDelta = newSizeDelta;
    }
}
