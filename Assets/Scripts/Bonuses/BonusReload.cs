using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BonusReload : MonoBehaviour
{
    public GameObject ACR;
    public GameObject M16;
    public GameObject FirstPerson;
    [SerializeField] private float bonusReload = 3f;

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<FirstPersonController>())
        {
            if(ACR.activeInHierarchy)ACR.GetComponent<ACR>().BonusReload(bonusReload);
            else if(M16.activeInHierarchy)M16.GetComponent<M16>().BonusReload(bonusReload);
            if (FirstPerson.GetComponent<BonusBar>().useReload == false) { FirstPerson.GetComponent<BonusBar>().useReload = true; }
            else FirstPerson.GetComponent<BonusBar>().doubleUseReload = true;
            Destroy(gameObject);
        }
    }
}