using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private EnemyData enemyData;

    private const string playerBodyTag = "PlayerBody";
    
    private float attack = 20.0f;
    private float speed = 1.0f;
    private bool bIsXDirIsPositive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (enemyData != null)
        {
            attack = enemyData.Attack;
            speed = enemyData.Spped;
            bIsXDirIsPositive = enemyData.IsXDirPositive;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            float xDir = ( bIsXDirIsPositive ) ? 1.0f : -1.0f;
            rb.linearVelocityX = xDir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerBodyTag))
        {
            PlayerController player = collision.transform.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HP -= attack;
            }

            Debug.Log("Current Player HP : " + player.HP);
        }
    }
}
