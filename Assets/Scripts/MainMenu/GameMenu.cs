using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public float timer;
    public bool ispuse;
    public bool guipuse;
    public Camera Camera;

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Camera.rect = new Rect(0f, 0f, 0f, 0f);
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            Camera.rect = new Rect(0f, 0f, 1f, 1f);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }
    public void OnGUI()
    {
        if (guipuse == true)
        {
            if (GUI.Button(new Rect((float)(Screen.width/2-70), (float)(Screen.height/2) - 150f, 150f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Camera.rect = new Rect(0f, 0f, 1f, 1f);

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2-70), (float)(Screen.height / 2), 150f, 45f), "В Меню"))
            {
                ispuse = false;
                timer = 0;
                SceneManager.LoadScene(0, LoadSceneMode.Single);

            }
        }
    }
}
