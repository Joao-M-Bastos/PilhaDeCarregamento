using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    float countDown = 5;

    [SerializeField]valueMenu valuemenu;
    private void Start()
    {
        RewritePaFilds();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                countDown = 5;
                CloseApp();
            }
        }
        else
        {
            countDown = 5;
        }

        
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void RewritePaFilds()
    {
        if (DataBase.GetInstance().GetPaCarregadoraValues() == null)
            return;

        for (int i = 0;i < DataBase.GetInstance().GetPaCarregadoraValues().Length; i++)
        {
            valuemenu.PaFild[i].text = DataBase.GetInstance().GetPaCarregadoraValues()[i].ToString();
        }
    }

    public void RewriteTremFilds()
    {
        if (DataBase.GetInstance().GetTremValues() == null)
            return;

        for (int i = 0; i < DataBase.GetInstance().GetTremValues().Length; i++)
        {
            valuemenu.TremFild[i].text = DataBase.GetInstance().GetTremValues()[i].ToString();
        }
    }

    public void RewriteManagerFilds()
    {
        if (DataBase.GetInstance().GetManegerValues() == null)
            return;

        for (int i = 0; i < DataBase.GetInstance().GetManegerValues().Length; i++)
        {
            valuemenu.ManagerFild[i].text = DataBase.GetInstance().GetManegerValues()[i].ToString();
        }
    }

}
