using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDeposit : MonoBehaviour
{ 
    public float baseYield = 50;
    public float yieldMult = 1;
    public GameObject drill;
    public GameObject drillPrefab;
    public GameObject notificationUI;
    public float buildCost = 1500;
    private float totalYield;
    private bool isDrilling;
    private Scenemanager manager;
    private Transform player;
    private float distanceToPlayer;
    
    [HideInInspector]
    public bool canBuild = false;


    /// <summary>
    /// retreives the scripts / objects
    /// </summary>
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scenemanager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notificationUI.SetActive(false);
    }

    /// <summary>
    /// gets the distance to playerl, if lower then 50 units it displays a UI. 
    /// also checks id youre within 50 units and you press E, it builds a drill and starts adding recourse points 
    /// </summary>
    private void Update()
    {
        GetDistance();
        if(distanceToPlayer <= 50)
        {
            notificationUI.SetActive(true);
            canBuild = true;
        }
        else
        {
            notificationUI.SetActive(false);
        }

        if(canBuild != false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isDrilling)
                {
                    if(manager.resourceCount >= buildCost){
                        buildDrill();

                    }
                    else
                    {
                        // message thatyou cant afford it
                    }

                }
            }
        }
        if(drill == null)
        {
            isDrilling = false;
            canBuild = true;
        }
        if(isDrilling == true)
        {
            notificationUI.SetActive(false);
        }
        RotateUI();
        if(isDrilling == false)
        {
            StopAllCoroutines();
        }

    }

    /// <summary>
    /// UI looks at the player in-game
    /// </summary>
    void RotateUI()
    {
        notificationUI.transform.LookAt(player);
    }
    /// <summary>
    /// calc distance to player
    /// </summary>
    void GetDistance()
    {
        distanceToPlayer = (gameObject.transform.position - player.position).sqrMagnitude;
    }
    
    /// <summary>
    /// build the prefab on the same position as the ore and enables drilling
    /// </summary>
    public void buildDrill()
    {
        drill = drillPrefab;
        isDrilling = true;
        StartCoroutine(collectInterval(baseYield, yieldMult));
        manager.resourceCount -= buildCost;
        drill = Instantiate(drill, transform.position, transform.rotation);
    }

    /// <summary>
    /// every 6 seconds add recourse points to the player
    /// </summary>
    /// <param name="yield"></param>
    /// <param name="yieldMP"></param>
    /// <returns></returns>
    IEnumerator collectInterval(float yield, float yieldMP)
    {

        yield return totalYield = yield * yieldMP;
        manager.resourceCount += totalYield;
        yield return new WaitForSeconds(6);
        StartCoroutine(collectInterval(baseYield, yieldMult));
        
    }
}
