using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SelectionWeapons : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] weap;
    public new bool collider = false;
    public new string name;
    private int numberWeap;
    // Start is called before the first frame update

    public void Search()
    {
        name = transform.name;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].name == name)
            {
                numberWeap = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Search();

        if (collider == true && Input.GetKey("e"))
        {

            for (int i = 0; i < weapons.Length; i++)
            {
                if(weap[i]==true)
                {
                    weapons[i].SetActive(false); 
                    weap[i] = false;
                    
                    weapons[numberWeap].SetActive(true);
                    weap[numberWeap] = true;
                    Destroy(gameObject);
                }
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonController>())
        {
            collider = true;


            if (weapons[numberWeap].activeInHierarchy&&weapons[numberWeap].name=="M16")
            {
                weapons[numberWeap].GetComponent<M16>().bulletLeft += 30;
                Destroy(gameObject);
            }
            if (weapons[numberWeap].activeInHierarchy && weapons[numberWeap].name == "ACR")
            {
                weapons[numberWeap].GetComponent<ACR>().bulletLeft += 30;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<FirstPersonController>())
        {
            collider = false;
        }
            
    }

}
