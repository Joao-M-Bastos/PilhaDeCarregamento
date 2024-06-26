using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaCarregadeira : MonoBehaviour
{
    [SerializeField] float capacidade, variacaoCapacidade;
    [SerializeField] float densidade, variacaoDensidade;
    [SerializeField] float enchimento, variacaoEnchimento;
    [SerializeField] float tempoDeColeta, variacaoTempoDeColeta;
    [SerializeField] float tempoDeDescarga, variacaoTempoDeDescarga;
    [SerializeField] float distancia, variacaoDistancia;
    [SerializeField] float velocidadeCarregado, variacaoVelocidadeCarregado;
    [SerializeField] float velocidadeVazio, variacaoVelocidadeVazio;

    float capacidadeAtual, tempoLocomocaoVazio, tempoLocomocaoCarregado, tempoColetaAtual, tempoDescargaAtual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            GenerateNewValues();
    }

    public void GenerateNewValues()
    {
        capacidadeAtual = Randomize(capacidade, variacaoCapacidade) * Randomize(densidade, variacaoDensidade) * Randomize(enchimento, variacaoEnchimento);
        tempoLocomocaoVazio = Randomize(velocidadeVazio, variacaoVelocidadeVazio) * Randomize(distancia, variacaoDistancia);
        tempoLocomocaoCarregado = Randomize(velocidadeCarregado, variacaoVelocidadeCarregado) * Randomize(distancia, variacaoDistancia);

        tempoColetaAtual = Randomize(tempoDeColeta, variacaoTempoDeColeta);
        tempoDescargaAtual = Randomize(tempoDeDescarga, variacaoTempoDeDescarga);

    }

    public float Randomize(float value, float variation)
    {
        //Debug.Log(value * variation);
        return value + (Random.Range(-0.5f,0.5f) * (value * variation/100));
    }
}
