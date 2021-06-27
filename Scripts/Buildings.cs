using UnityEngine;

public class Buildings : MonoBehaviour
{
    /// <summary>
    /// base script for all buildings, sets health and other attributes and destroys the gameobject if health <= 0
    /// </summary>
    public float buildingHP;
    public float cost;
    public float currentHP;

    [Header("Collector Tab")]
    public float collectSpeed;

    public float yield;

    [Header("Base Tab")]
    public string temp = "temporaily";

    [Header("Wall Tab")]
    public float damageReduction;

    [Header("bools")]
    public bool isBase = false;

    public bool isWall = false;
    public bool isTurret = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (isWall == true)
        {
            currentHP = buildingHP * ((100 + damageReduction) / 100);
        }
        else
        {
            currentHP = buildingHP;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHP <= 0)
        {
            if (isBase == true)
            {
                Debug.Log("Base destroyed");

            }
            if (isWall == true)
            {
               //options for on death
            }
            if (isTurret == true)
            {

                //options for more stuff on death

            }
            Destroy(gameObject);

        }



    }
}