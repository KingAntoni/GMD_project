using UnityEngine;

public class RapidFirePotion : MonoBehaviour, IItem
{
    public void Collect()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerShoot playerShoot = collision.GetComponent<PlayerShoot>();
        if (playerShoot)
        {
            playerShoot.ActivateUnlimitedArrows();
            Destroy(gameObject);
        }
    }
}
