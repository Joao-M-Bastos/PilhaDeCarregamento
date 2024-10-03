using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pilha : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] float quantidadeDaPilha;
    [SerializeField] float capacidadePilha;
    // Start is called before the first frame update
    void Start()
    {
        capacidadePilha = capacidadePilha * ManagerScript.instance.GetMultiplier();
        quantidadeDaPilha = 0;
        UpdateText();
    }

    public float PorcentagemDeEnchimento()
    {
        return quantidadeDaPilha / capacidadePilha;
    }

    public float ReturnValue()
    {
        return quantidadeDaPilha;
    }

    public void AddValue(float valueToAdd)
    {
        quantidadeDaPilha += valueToAdd;
        UpdateText();
    }

    public void RemoveValue(float valueToRemove)
    {
        quantidadeDaPilha -= valueToRemove;
        UpdateText();
    }

    public void UpdateText()
    {
        quantityText.text = quantidadeDaPilha.ToString();
    }
}
