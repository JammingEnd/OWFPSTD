using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public float damage = 10;
    public float attackCooldown = 100;
    public float enemyAttackRange;
    public float detectionRadius = 1000;

    private GameObject player;
    public GameObject building;
    private Buildings attackingBuild;
    private bool canAttack = true;
    private int seek;
    private int goElse;
    private float distancePlayerEnemy;
    private bool isAttackingBuilding = false;
    private bool isAttackingPlayer = false;
    private float playerdamage;
    // Start is called before the first frame update

    /// <summary>
    /// finds the player in the game
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerdamage = damage;
    }

    // Update is called once per frame

    /// <summary>
    /// moves to the player, if a building is in the way while not attacking player, attack the building
    /// </summary>
    private void Update()
    {
        if (player != null && isAttackingBuilding == false)
        {
            gameObject.transform.LookAt(player.transform);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (player == null && building != null)
        {
            gameObject.transform.LookAt(building.transform);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if(distancePlayerEnemy > enemyAttackRange + 10)
        {
            playerdamage = 0;
        }
        else
        {
            playerdamage = damage;
        }
        if (building == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, enemyAttackRange))
            {
                if (hit.collider.gameObject.tag == "Building")
                {
                    building = hit.collider.gameObject;
                    attackingBuild = building.GetComponent<Buildings>();
                    isAttackingBuilding = true;
                }
                if (hit.collider.gameObject.tag == "Player")
                {
                    isAttackingPlayer = true;
                }
            }
        }
        if (seek == 1)
        {
            isAttackingBuilding = false;
        }
        GetDistance();

        /*  if(distancePlayerEnemy > detectionRadius)
          {
              building = GameObject.FindGameObjectWithTag("Building");
              if(seek == 1)
              {
                  goElse = Random.Range(1, 20);
              }
          }

  */
    }

    /// <summary>
    /// get distance to player
    /// </summary>
    private void GetDistance()
    {
        distancePlayerEnemy = (gameObject.transform.position - player.transform.position).sqrMagnitude;
    }


    /// <summary>
    /// handles the attack cooldown on a fixed update
    /// </summary>
    private void FixedUpdate()
    {
        if (attackingBuild != null)
        {
            if (isAttackingBuilding == true)
            {
                if (canAttack == true)
                {
                    attackingBuild.currentHP -= damage;
                    canAttack = false;
                    if (distancePlayerEnemy >= enemyAttackRange)
                    {
                        isAttackingPlayer = false;
                    }
                }
                if (canAttack == false)
                {
                    attackCooldown -= 1;
                }
                if (attackCooldown <= 0)
                {
                    canAttack = true;
                    attackCooldown = 100;
                }
            }
        }
        else
        {
            isAttackingBuilding = false;
        }

        if (player != null)
        {
            if (isAttackingPlayer == true)
            {
                if (canAttack == true)
                {
                    float currentDMG = damage;
                    player.GetComponent<PlayerStats>().currentHp -= playerdamage;
                    canAttack = false;
                    if (distancePlayerEnemy <= detectionRadius)
                    {
                        seek = Random.Range(1, 4);
                    }
                    if (distancePlayerEnemy < enemyAttackRange)
                    {
                        playerdamage = damage;
                    }

                }
                if (canAttack == false)
                {
                    attackCooldown -= 1;
                }
                if (attackCooldown <= 0)
                {
                    canAttack = true;
                    attackCooldown = 100;
                }
            }
        }
        else
        {
            isAttackingPlayer = false;
        }
    }

    /// <summary>
    /// draws a visivle sphere to indicate the attack range
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionRadius);
        Gizmos.color = Color.red;
    }
}