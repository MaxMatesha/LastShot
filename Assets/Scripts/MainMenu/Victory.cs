using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject Text;
    public IEnumerator Passed()
    {
        Text.SetActive(true);
        yield return new WaitForSeconds(10f);
        Text.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public int mission = 0;
    
    void Start()
    {
        
    }


    void Update()
    {
        if(mission >= 10)
        {
            StartCoroutine(Passed());
        }

    }
}
