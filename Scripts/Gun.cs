using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Gun", menuName = "Guns/Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public GameObject gunPrefab;
    public bool isAutomatic;

    public GameObject hitEffect;
    [Header("Stats")] 
    public int minimunDamage;
    public int maximumDamage;
    public float maximumRange;

    [Header("Firerate is * 60 / 360")]
    public float fireRate;
    public float currentFR;
    public bool canfire = true;

    /// <summary>
    /// for automatic guns, damages enemies every CurrentFR seconds
    /// </summary>
    /// <param name="cameraPos"></param>
    /// <returns></returns>
    public IEnumerator FireAble(Transform cameraPos)
    {
        while(canfire == true)
        {
            yield return cameraPos;
            Fire(cameraPos);
            canfire = false;
            yield return new WaitForSeconds(currentFR);
            canfire = true;
        }

        
    }

   
   /// <summary>
   /// damages when the mouse is clicked down
   /// </summary>
   /// <param name="cameraPos"></param>
    public virtual void OnLeftMouseDown(Transform cameraPos)
    {
        Fire(cameraPos);
    }
   /// <summary>
   /// fires a raycast, detects if it collides with an enenmyand damages with range dropofff
   /// </summary>
   /// <param name="cameraPos"></param>
    public void Fire(Transform cameraPos)
    {
        RaycastHit whatIHit;
        if (Physics.Raycast(cameraPos.position, cameraPos.transform.forward, out whatIHit, Mathf.Infinity))
        {
            GameObject Effect;
            Effect = Instantiate(hitEffect, whatIHit.point, Quaternion.identity);
            Destroy(Effect, 1);
            GameObject hitEnemy = whatIHit.transform.gameObject;
            if (hitEnemy != null && hitEnemy.tag == "Enemy")
            {
               
                float normalizedDistance = whatIHit.distance / maximumRange;
                if (normalizedDistance <= 1)
                {
                    hitEnemy.GetComponent<EnemyHP>().currentHP -= DealDamage(normalizedDistance);
                    
                }
            }
        }
    }
    /// <summary>
    /// lerps the damage over range
    /// </summary>
    /// <param name="normalizedDistance"></param>
    /// <returns></returns>
    float DealDamage(float normalizedDistance) {

       float totalDamage = Mathf.Lerp(maximumDamage, minimunDamage, normalizedDistance);

        return totalDamage;
    }




}
