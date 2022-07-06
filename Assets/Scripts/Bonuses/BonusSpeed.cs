using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BonusSpeed : MonoBehaviour
{
    public GameObject FirstPerson;
    [SerializeField] private float bonusSpeed = 10f;

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<FirstPersonController>())
        {
            FirstPerson.GetComponent<FirstPersonController>().BonusSpeed(bonusSpeed);
            if(FirstPerson.GetComponent<BonusBar>().useRun == false) {FirstPerson.GetComponent<BonusBar>().useRun = true;  }
            else FirstPerson.GetComponent<BonusBar>().doubleUseRun = true;
            Destroy(gameObject);
        }
       
    }

}
