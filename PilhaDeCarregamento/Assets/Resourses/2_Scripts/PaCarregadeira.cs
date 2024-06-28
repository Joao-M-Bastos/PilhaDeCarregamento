using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadosDaPa
{
    PARADA,
    ANDANDO_VAZIA,
    ANDANDO_CHEIA,
    COLETA,
    DESCARREGANDO
}

public class PaCarregadeira : MonoBehaviour
{
    [Space(10), Header("Toneladas de Minerio")]
    [SerializeField] float capacidade, variacaoCapacidade;
    [SerializeField] float densidade, variacaoDensidade;
    [SerializeField] float enchimento, variacaoEnchimento;

    [Space(10), Header("Tempo de interação com minerio")]
    [SerializeField] float tempoDeColeta, variacaoTempoDeColeta;
    [SerializeField] float tempoDeDescarga, variacaoTempoDeDescarga;
    

    [Space(10), Header("Tempo de Locomoção")]
    [SerializeField] float distancia, variacaoDistancia;
    [SerializeField] float velocidadeCarregado, variacaoVelocidadeCarregado;
    [SerializeField] float velocidadeVazio, variacaoVelocidadeVazio;

    float capacidadeAtual, tempoLocomocaoVazio, tempoLocomocaoCarregado, tempoColetaAtual, tempoDescargaAtual;

    [SerializeField] float quantidadeMinerio;

    [SerializeField] float tempoAtual;

    Rigidbody rb;
   [SerializeField] EstadosDaPa statesPa = new EstadosDaPa();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    void Update()
    {
        Vector3 percorrido;

        


        switch (statesPa)
        {
            case EstadosDaPa.PARADA:
                GenerateNewValues();
                tempoAtual = tempoColetaAtual;
                ChangeState(EstadosDaPa.COLETA);
                break;
            case EstadosDaPa.COLETA:
                tempoAtual -= Time.deltaTime;
                if(tempoAtual < 0)
                {
                    EncherCacamba();
                    ChangeState(EstadosDaPa.ANDANDO_CHEIA);
                }
                break;
            case EstadosDaPa.ANDANDO_CHEIA:

                percorrido = transform.forward / (tempoLocomocaoCarregado / 20);
                rb.velocity = percorrido;


                if (transform.position.x > 10)
                {
                    tempoAtual += tempoDescargaAtual;

                    transform.position = new Vector3(10, 1, 0);

                    ChangeState(EstadosDaPa.DESCARREGANDO);
                    
                }

                break;
            case EstadosDaPa.DESCARREGANDO:

                tempoAtual -= Time.deltaTime;
                if (tempoAtual < 0)
                {
                    EsvaziarCacamba();
                    ChangeState(EstadosDaPa.ANDANDO_VAZIA);
                }
                break;

            case EstadosDaPa.ANDANDO_VAZIA:

                percorrido = transform.forward * -1 / (tempoLocomocaoVazio / 20);
                rb.velocity = percorrido;

                if (transform.position.x < -10)
                {
                    transform.position = new Vector3(-10, 1, 0);
                    ChangeState(EstadosDaPa.PARADA);
                }
                break;

        }
    }

    private void EsvaziarCacamba()
    {
        quantidadeMinerio = 0;
    }

    private void EncherCacamba()
    {
        quantidadeMinerio = capacidadeAtual;
    }

    public void ChangeState(EstadosDaPa newState)
    {
        rb.velocity = Vector3.zero;
        statesPa = newState;
    }
}
