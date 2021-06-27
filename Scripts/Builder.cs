using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings = new List<GameObject>();
    public GameObject buildUI;
    public float buildRange = 200;
    private Scenemanager manager;

    private GameObject Playercam;
    private GameObject Player;
    private GameObject buildingPrefab = null;
    private bool isBuild = false;
    private bool isBuildMode = false;

    /// <summary>
    /// finds all components needed
    /// </summary>
    private void Start()
    {
        buildUI.SetActive(false);
        Playercam = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scenemanager>();
    }

    /// <summary>
    /// detects for key inputs and if a prefab is selected updates the buildmode function
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            buildUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(buildingPrefab != null)
        {
            
            if (buildingPrefab.GetComponent<Buildings>().cost <= manager.resourceCount)
            {
                BuildMode();
            }

        }
       
    }

    /// <summary>
    /// gets the position you aim at, if you click while in buildmode you instantiate the building that faces you 
    /// </summary>
    private void BuildMode()
    {
        Vector3 targetPostition = new Vector3(Player.transform.position.x, buildingPrefab.transform.position.y, Player.transform.position.z);
        if (isBuildMode == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(Playercam.transform.position, Playercam.transform.forward, out hit, buildRange))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject newBuild = Instantiate(buildingPrefab, hit.point, Quaternion.Euler(targetPostition));
                    manager.resourceCount -= buildingPrefab.GetComponent<Buildings>().cost;
                    newBuild.transform.LookAt(targetPostition);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buildingPrefab = null;
                isBuildMode = false;
            }
        }
    }
    /// <summary>
    /// building functions for the 4 buttons
    /// </summary>
    public void BuildBuilding1()
    {
        if (isBuild != true)
        {
            isBuild = true;
            buildingPrefab = buildings[0].gameObject;
            isBuildMode = true;
            buildUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
      
    }

    public void BuildBuilding2()
    {
        buildingPrefab = buildings[2].gameObject;
        isBuildMode = true;
        buildUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BuildBuilding3()
    {
        buildingPrefab = buildings[2].gameObject;
        isBuildMode = true;
        buildUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BuildBuilding4()
    {
        buildingPrefab = buildings[3].gameObject;
        isBuildMode = true;
        buildUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}