using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BonusDamage : MonoBehaviour
{
    public GameObject ACR;
    public GameObject M16;
    public GameObject FirstPerson;
    [SerializeField] private float bonusDamage = 10f;

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<FirstPersonController>())
        {
            if (ACR.activeInHierarchy) ACR.GetComponent<ACR>().BonusDamage(bonusDamage);
            else if (M16.activeInHierarchy) M16.GetComponent<M16>().BonusDamage(bonusDamage);
            if (FirstPerson.GetComponent<BonusBar>().useDamage == false) { FirstPerson.GetComponent<BonusBar>().useDamage = true; }
            else FirstPerson.GetComponent<BonusBar>().doubleUseDamage = true;
            Destroy(gameObject);
        }
      
    }


}
