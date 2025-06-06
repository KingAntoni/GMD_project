using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public int arrowDamage = 1;
    public Vector2 direction = Vector2.right;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        MageEnemy mageEnemy = collision.GetComponent<MageEnemy>();
        if (enemy)
        {
            enemy.TakeDamage(arrowDamage);
            Destroy(gameObject);
        }
        if (mageEnemy)
        {
            mageEnemy.TakeDamage(arrowDamage);
            Destroy(gameObject);
        }
    }
}
