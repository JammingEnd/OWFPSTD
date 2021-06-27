using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Gun> guns = new List<Gun>();
    

    private Gun currentGun;
    private Transform cameraTransform;
    private GameObject currentGunPrefab;

    /// <summary>
    /// gets the camera position, and spawns gun with index 0
    /// </summary>
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        currentGunPrefab = Instantiate(guns[0].gunPrefab, this.transform);
        currentGun = guns[0];

    }
    /// <summary>
    /// attaches numbers to prefabs, if you press 2 you get gun number 1 aka the pistol
    /// </summary>
    private void Update()
    {
        CheckForShooting();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(currentGunPrefab);
            currentGunPrefab = Instantiate(guns[0].gunPrefab, this.transform);
            currentGun = guns[0];

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(currentGunPrefab);
            currentGunPrefab = Instantiate(guns[1].gunPrefab, this.transform);
            currentGun = guns[1];
            currentGun.canfire = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(currentGunPrefab);
            currentGunPrefab = Instantiate(guns[2].gunPrefab, this.transform);
            currentGun = guns[2];

        }


       currentGun.currentFR = currentGun.fireRate * 60 / 360;
    }

    /// <summary>
    /// if you press left mouse you shoot. if  you hold you start the coroutine for the automatic weapons
    /// </summary>
    private void CheckForShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentGun.isAutomatic == true)
            {
                if (currentGun.canfire == true)
                {
                    StartCoroutine(currentGun.FireAble(cameraTransform));
                    currentGun.canfire = true;

                }
            }
           

        }
        if (Input.GetMouseButton(0))
        {
            if (!currentGun.isAutomatic)
            {
                if(currentGun.canfire == true)
                {
                    currentGun.OnLeftMouseDown(cameraTransform);
                    currentGun.canfire = false;
                }
            
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!currentGun.isAutomatic)
            {
                currentGun.canfire = true;

            }

            
            if (currentGun.isAutomatic == true)
            {
                StopAllCoroutines();
                currentGun.canfire = true;

            }
        }
        
        if (Input.GetMouseButton(1))
        {
           // currentGun.OnRightMouseDown();
        }
    }



}
