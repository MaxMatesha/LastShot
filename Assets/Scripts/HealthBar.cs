using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
////    добавить здоровья
//      HealthBar.AdjustCurrentValue(10);
////    убавить здоровье
//      HealthBar.AdjustCurrentValue(-10);
////    получить текущее значение
//      Debug.Log("Здоровье персонажа на данный момент:" + HealthBar.currentValue);
    public float maxValue = 100f;
    public Color color = Color.red;
    public int width = 4;
    public Slider slider;
    public bool isRight;

    private static float current;

    void Start()
    {
        slider.fillRect.GetComponent<Image>().color = color;

        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = maxValue;

        UpdateUI();
    }

    public static float currentValue
    {
        get { return current; }
    }

    void Update()
    {

        if (current <= 0)
        { Cursor.lockState = CursorLockMode.None; SceneManager.LoadScene(0, LoadSceneMode.Single); }
        if (current > maxValue) current = maxValue;
        slider.value = current;
    }

    void UpdateUI()
    {
        RectTransform rect = slider.GetComponent<RectTransform>();

        int rectDeltaX = 200;
        float rectPosX = 0;

        if (isRight)
        {
            rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.RightToLeft;
        }
        else
        {
            rectPosX = rect.position.x + (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.LeftToRight;
        }

        rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
        rect.position = new Vector3(rectPosX, rect.position.y, rect.position.z);
    }

    public static void AdjustCurrentValue(float adjust)
    {
        current += adjust;
    }
}