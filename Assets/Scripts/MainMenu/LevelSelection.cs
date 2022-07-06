using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private int level = 0;
    public void OneLevel()
    {
        level = 1;
    }
    public void TwoLevel()
    {
        level = 2;
    }

    public void Play()
    {
        switch (level)
        {
            case 1:
                { SceneManager.LoadScene("Wasteland"); }
                break;
            case 2:
                { SceneManager.LoadScene("WastelandNight"); }
                break;
        }
    }
}
