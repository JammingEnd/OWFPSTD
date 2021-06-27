using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{

    public float maxHP = 100;
    public float pointsGain = 100;
    //[HideInInspector]
    public float currentHP;
    private Scenemanager manager;

    /// <summary>
    /// sets the hp and retrieves the scenemanager scripts
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        manager = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scenemanager>();
    }

    // Update is called once per frame

    /// <summary>
    /// if HP in lower then 0 it destroys the object and adds points
    /// </summary>
    void Update()
    {
        if(currentHP <= 1)
        {
            Destroy(this.gameObject);
            manager.score += pointsGain;
            // instantiate death effect
        }
    }
}
