using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float range;
    public GameObject muzzleFX;
    public GameObject barrelObj;
    private GameObject enemy;
    private EnemyHP EnemyHealth;
    public GameObject station;
    public GameObject rotPart;
    public bool canUpdate = false;
    private float fireCountdown = 0;

    /// <summary>
    /// every 0.5 seconds detects for a closer / new enenmy
    /// </summary>
    void Start()
    {
        InvokeRepeating("TargetingUpdate", 0, 0.5f);

    }

   /// <summary>
   /// handles the fire rate if there is an enemy nearbyand damages it
   /// </summary>
    void Update()
    {
        if (enemy != null)
        {
            Shooting();
            if (fireCountdown <= 0)
            {
                EnemyHealth.currentHP -= damage;
              GameObject muzzle =  (GameObject)Instantiate(muzzleFX, this.barrelObj.transform);
              Destroy(muzzle, 1);
                fireCountdown = 1 / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
      
      

      
    }
    
    /// <summary>
    /// finds all enemies in the game, calculates the distance. if the distance is <= the targeting range then it gets its health script
    /// </summary>
    void TargetingUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortDistance <= range) { 

            enemy = nearestEnemy;
            EnemyHealth = nearestEnemy.GetComponent<EnemyHP>();
        }
        else
        {
            enemy = null;
        }

    }
    /// <summary>
    /// looks at the enemy
    /// </summary>
    void Shooting()
    {
        rotPart.transform.LookAt(enemy.transform);
    }
 


}
