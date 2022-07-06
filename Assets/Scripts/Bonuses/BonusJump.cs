using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BonusJump : MonoBehaviour
{
    public GameObject FirstPerson;
    [SerializeField] private float bonusJump = 10f;

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<FirstPersonController>())
        {
            FirstPerson.GetComponent<FirstPersonController>().BonusJump(bonusJump);
            if (FirstPerson.GetComponent<BonusBar>().useJump == false) { FirstPerson.GetComponent<BonusBar>().useJump = true; }
            else FirstPerson.GetComponent<BonusBar>().doubleUseJump = true;
            Destroy(gameObject);
        }
    }

}