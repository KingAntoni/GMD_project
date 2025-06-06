using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : MonoBehaviour
{
    [Header("Combat")]
    public int damage = 1;
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color ogColor;

    [Header("Fireball Attack")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float shootInterval = 4f;
    public float fireballSpeed = 8f;
    public float castDelay = 2f;

    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ogColor = spriteRenderer.color;
        currentHealth = maxHealth;

        InvokeRepeating(nameof(StartCasting), 1f, shootInterval);
    }

    void Update()
    {
        if (!player) return;

        // Flip mage to face the player
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void StartCasting()
    {
        StartCoroutine(CastFireballWithDelay());
    }

    IEnumerator CastFireballWithDelay()
    {
        // Optional: play cast animation here
        yield return new WaitForSeconds(castDelay);
        ShootFireball();
    }

    void ShootFireball()
    {
        if (!player) return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * fireballSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = ogColor;
    }

    void Die()
    {
        foreach (LootItem lootItem in lootTable)
        {
            if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
        }

        Destroy(gameObject);
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
            droppedLoot.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
