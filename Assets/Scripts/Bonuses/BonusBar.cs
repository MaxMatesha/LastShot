using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BonusBar : MonoBehaviour
{
    public Transform[] BonusTime;
    public Transform[] Bonuses;
    private float currenAmount = 1f;

    public bool useRun = false;
    private float timeRun = 0;
    public bool doubleUseRun = false;

    public bool useDamage = false;
    private float timeDamage = 0;
    public bool doubleUseDamage = false;

    public bool useJump = false;
    private float timeJump = 0;
    public bool doubleUseJump = false;

    public bool useReload = false;
    private float timeReload = 0;
    public bool doubleUseReload = false;

    // Update is called once per frame
    void Update()
    {
        //BonusRun
        if (useRun == true && doubleUseRun == false && BonusTime[0].GetComponent<Image>().fillAmount != 0)
        { timeRun += Time.deltaTime/10; BonusTime[0].GetComponent<Image>().fillAmount = currenAmount- timeRun; Bonuses[0].gameObject.SetActive(true);}
        else if (doubleUseRun == true) { timeRun = 0; BonusTime[0].GetComponent<Image>().fillAmount = 1; doubleUseRun = false; }     
        else { timeRun = 0; useRun = false; BonusTime[0].GetComponent<Image>().fillAmount = 1; Bonuses[0].gameObject.SetActive(false); }

        //BonusDamage
        if (useDamage == true && doubleUseDamage == false && BonusTime[1].GetComponent<Image>().fillAmount != 0)
        { timeDamage += Time.deltaTime/10; BonusTime[1].GetComponent<Image>().fillAmount = currenAmount- timeDamage; Bonuses[1].gameObject.SetActive(true);}
        else if (doubleUseDamage == true) { timeDamage = 0; BonusTime[1].GetComponent<Image>().fillAmount = 1; doubleUseDamage = false; }     
        else { timeDamage = 0; useDamage = false; BonusTime[1].GetComponent<Image>().fillAmount = 1; Bonuses[1].gameObject.SetActive(false); }

        //BonusJump
        if (useJump == true && doubleUseJump == false && BonusTime[2].GetComponent<Image>().fillAmount != 0)
        { timeJump += Time.deltaTime/10; BonusTime[2].GetComponent<Image>().fillAmount = currenAmount- timeJump; Bonuses[2].gameObject.SetActive(true);}
        else if (doubleUseJump == true) { timeJump = 0; BonusTime[2].GetComponent<Image>().fillAmount = 1; doubleUseJump = false; }     
        else { timeJump = 0; useJump = false; BonusTime[2].GetComponent<Image>().fillAmount = 1; Bonuses[2].gameObject.SetActive(false); }


        //BonusReload
        if (useReload == true && doubleUseReload == false && BonusTime[3].GetComponent<Image>().fillAmount != 0)
        { timeReload += Time.deltaTime/10; BonusTime[3].GetComponent<Image>().fillAmount = currenAmount- timeReload; Bonuses[3].gameObject.SetActive(true);}
        else if (doubleUseReload == true) { timeReload = 0; BonusTime[3].GetComponent<Image>().fillAmount = 1; doubleUseReload = false; }     
        else { timeReload = 0; useReload = false; BonusTime[3].GetComponent<Image>().fillAmount = 1; Bonuses[3].gameObject.SetActive(false); }

        



    }
}
