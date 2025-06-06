using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 10f; // duration of invul potion

    public HealthUI healthUI;

    private SpriteRenderer spriteRenderer;

    public static event Action OnPlayedDied;

    void Start()
    {
        ResetHealth();

        spriteRenderer = GetComponent<SpriteRenderer>();
        GameController.OnReset += ResetHealth;
        HealthItem.OnHealthCollect += Heal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    public void TakeDamage(int damage)
{
    if (isInvulnerable) return;

    currentHealth -= damage;
    healthUI.UpdateHearts(currentHealth);

    StartCoroutine(FlashRed());

    if (currentHealth <= 0)
    {
        OnPlayedDied?.Invoke();
    }
}


    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthUI.UpdateHearts(currentHealth);
    }

    public void ActivateInvulnerability()
    {
    StartCoroutine(InvulnerabilityCoroutine());
    }

private IEnumerator InvulnerabilityCoroutine()
{
    isInvulnerable = true;

    float elapsed = 0f;

    // Optional: Flash color during invulnerability
    while (elapsed < invulnerabilityDuration)
    {
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        elapsed += 0.2f;
    }

    isInvulnerable = false;
    spriteRenderer.color = Color.white;
}


    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
