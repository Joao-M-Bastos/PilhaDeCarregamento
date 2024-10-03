using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class valueMenu : MonoBehaviour
{
    [SerializeField] GameObject[] canvases;
    [SerializeField] TMP_InputField[] paInputFilds;
    [SerializeField] TMP_InputField[] tremInputFilds;
    [SerializeField] TMP_InputField[] managerInputFilds;

    public TMP_InputField[] PaFild => paInputFilds;
    public TMP_InputField[] TremFild => tremInputFilds;
    public TMP_InputField[] ManagerFild => managerInputFilds;

    int currentCanvas;
    


    public void OpenSimulationScene()
    {
        SceneManager.LoadScene(1);
    }

    public void NextCanvas()
    {
        currentCanvas++;
        foreach (GameObject go in canvases)
        {
            go.SetActive(false);
        }

        if(currentCanvas >= canvases.Length)
            OpenSimulationScene();
        else
            canvases[currentCanvas].SetActive(true);
    }


    public void SetPaCarregadoraValues()
    {
        float a;
        float[] newValues = new float [paInputFilds.Length];
        for(int i = 0; i< newValues.Length; i++)
        {
            bool result = float.TryParse(paInputFilds[i].text, out a);

            if (result)
                newValues[i] = float.Parse(paInputFilds[i].text);
            else
                SceneManager.LoadScene(0);
        }
        
        DataBase.GetInstance().SetPaCarregadoraValues(newValues);
    }

    public void SetTremValues()
    {
        float a;
        float[] newValues = new float[tremInputFilds.Length];
        for (int i = 0; i < newValues.Length; i++)
        {
            bool result = float.TryParse(tremInputFilds[i].text, out a);

            if (result)
                newValues[i] = float.Parse(tremInputFilds[i].text);
            else
                SceneManager.LoadScene(1);
        }

        DataBase.GetInstance().SetTremValues(newValues);
    }

    public void SetManagerValues()
    {
        float a;
        float[] newValues = new float[managerInputFilds.Length];
        for (int i = 0; i < newValues.Length; i++)
        {
            bool result = float.TryParse(managerInputFilds[i].text, out a);

            if (result)
                newValues[i] = float.Parse(managerInputFilds[i].text);
            else
                SceneManager.LoadScene(0);
        }

        DataBase.GetInstance().SetManagerValues(newValues);
    }
}
