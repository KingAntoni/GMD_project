using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float arrowSpeed = 10f;

    private bool unlimitedArrows = false;
    public float unlimitedDuration = 10f;

     public int maxShots = 5;
    public float cooldownDuration = 2f;

    private int shotsRemaining;
    private bool isOnCooldown = false;


    void Start()
    {
        shotsRemaining = maxShots;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryShootArrow();
        }
    }

    void TryShootArrow()
{
    if (isOnCooldown && !unlimitedArrows)
        return;

    if (unlimitedArrows || shotsRemaining > 0)
    {
        ShootArrow();

        if (!unlimitedArrows)
        {
            shotsRemaining--;

            if (shotsRemaining == 0)
            {
                StartCoroutine(StartCooldown());
            }
        }
    }
}


    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        Arrow arrowScript = arrow.GetComponent<Arrow>();

        // Flip arrow direction based on player facing direction
        bool facingRight = transform.localScale.x > 0;
        arrowScript.direction = facingRight ? Vector2.right : Vector2.left;

        // Flip arrow sprite if needed
        if (!facingRight)
        {
            Vector3 scale = arrow.transform.localScale;
            scale.x *= -1;
            arrow.transform.localScale = scale;
        }

        Destroy(arrow, 4f);

    }

    public void ActivateUnlimitedArrows()
    {
        StartCoroutine(UnlimitedArrowCoroutine());
    }

private System.Collections.IEnumerator UnlimitedArrowCoroutine()
    {
        unlimitedArrows = true;
        Debug.Log("Unlimited arrows activated!");

        yield return new WaitForSeconds(unlimitedDuration);

        unlimitedArrows = false;
        shotsRemaining = maxShots; // reset to full after
        Debug.Log("Unlimited arrows ended.");
    }

    System.Collections.IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        shotsRemaining = maxShots;
        isOnCooldown = false;
    }

}
