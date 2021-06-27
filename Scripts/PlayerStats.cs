using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float adjustHP;
    private float maxHP = 100;
    public float currentHp;

    // Start is called before the first frame update

    /// <summary>
    /// sets the players health
    /// </summary>
    void Start()
    {
        adjustHP = 0;
        maxHP = maxHP + adjustHP;
        currentHp = maxHP;

    }

   
}
