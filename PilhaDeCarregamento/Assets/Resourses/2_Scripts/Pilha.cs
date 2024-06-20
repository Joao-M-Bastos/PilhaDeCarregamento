using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilha : MonoBehaviour
{
    [SerializeField] float quantidadeDaPilha;
    // Start is called before the first frame update
    void Start()
    {
        quantidadeDaPilha = 0;
    }

    public void AddValue(float valueToAdd)
    {
        quantidadeDaPilha += valueToAdd;
    }

    public void RemoveValue(float valueToRemove)
    {
        quantidadeDaPilha -= valueToRemove;
    }
}
