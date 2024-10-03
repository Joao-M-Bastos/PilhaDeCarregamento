using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    static DataBase instance;

    #region Pa Carregadeira
    float[] valoresPaCarregadeira;
    #endregion

    float[] valoresTrem;
    float[] valoresManager;

    private void Awake()
    {

        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    public static DataBase GetInstance()
    {
        return instance;
    }

    public void SetPaCarregadoraValues(float[] novosValores)
    {
        valoresPaCarregadeira = novosValores;
    }

    public float[] GetPaCarregadoraValues()
    {
        return valoresPaCarregadeira;
    }

    public void SetTremValues(float[] novosValores)
    {
        valoresTrem = novosValores;
    }

    public float[] GetTremValues()
    {
        return valoresTrem;
    }

    public void SetManagerValues(float[] novosValores)
    {
        valoresManager = novosValores;
    }

    public float[] GetManegerValues()
    {
        return valoresManager;
    }
}