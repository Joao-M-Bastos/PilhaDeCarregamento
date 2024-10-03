using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    [SerializeField] float valueMultiplier, timeMultiplier, dencidadeMinerio;

    [SerializeField]float countDown = 5;
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        float[] values = DataBase.GetInstance().GetManegerValues();

        dencidadeMinerio = values[0];
        timeMultiplier = values[1];
        valueMultiplier = values[2];

        Time.timeScale = timeMultiplier;

        text.text = "Tempo Atual: " + Time.timeScale;
    }

    

    public float GetMultiplier()
    {
        return valueMultiplier;
    }

    public float GetDensity()
    {
        return dencidadeMinerio;
    }

    // Update is called once per frame3
    void Update()
    {
        

        if (Input.GetKey(KeyCode.Escape))
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                countDown = 5;
                ReturnToStarterScene();
            }
        }
        else
        {
            countDown = 5;
        }
    }

    public void ReturnToStarterScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SpeedUp()
    {
        if (Time.timeScale > 99)
            Time.timeScale = 100;
        else
            Time.timeScale++;

        text.text = "Tempo Atual: " + Time.timeScale;
    }

    public void SpeedDown()
    {
        if (Time.timeScale < 1)
            Time.timeScale = 0;
        else
            Time.timeScale--;

        text.text = "Tempo Atual: " + Time.timeScale;
    }
}
