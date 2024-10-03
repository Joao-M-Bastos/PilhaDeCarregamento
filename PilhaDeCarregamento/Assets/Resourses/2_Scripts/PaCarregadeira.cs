using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] float porcentagemDeEnchimentoNecessarioParaTrabalho;

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

    [SerializeField] GameObject body;
    [SerializeField] TextMeshProUGUI quantityText;

    Pilha pilhaFinal, pilhaTrem;

    private void Awake()
    {
        pilhaTrem = GameObject.FindGameObjectWithTag("PilhaTrem").GetComponent<Pilha>();
        pilhaFinal = GameObject.FindGameObjectWithTag("PilhaFinal").GetComponent<Pilha>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float[] values = DataBase.GetInstance().GetPaCarregadoraValues();

        capacidade = values[0];
        variacaoCapacidade = values[1];
        enchimento = values[2] / 100;
        variacaoEnchimento = values[3];


        tempoDeColeta = values[4];
        variacaoTempoDeColeta = values[5];
        tempoDeDescarga = values[6];
        variacaoTempoDeDescarga = values[7];


        distancia = values[8];
        variacaoDistancia = values[9];
        velocidadeCarregado = values[10];
        variacaoVelocidadeCarregado = values[11];
        velocidadeVazio = values[12];
        variacaoVelocidadeVazio = values[13];

        porcentagemDeEnchimentoNecessarioParaTrabalho = values[14];
    }

    public void GenerateNewValues()
    {
        capacidadeAtual = Randomize(capacidade, variacaoCapacidade) * ManagerScript.instance.GetDensity() * Randomize(enchimento, variacaoEnchimento) * ManagerScript.instance.GetMultiplier();
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
                quantityText.text = "PARADA";
                if (pilhaTrem.PorcentagemDeEnchimento() > porcentagemDeEnchimentoNecessarioParaTrabalho/100)
                {
                    GenerateNewValues();
                    tempoAtual = tempoColetaAtual;
                    ChangeState(EstadosDaPa.COLETA);
                }
                break;
            case EstadosDaPa.COLETA:
                tempoAtual -= Time.deltaTime;
                if(tempoAtual < 0)
                {
                    EncherCacamba();
                    ChangeState(EstadosDaPa.ANDANDO_CHEIA);
                    body.transform.localScale = new Vector3(20, 20, 20);
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
                    body.transform.localScale = new Vector3(20, -20, 20);
                    ChangeState(EstadosDaPa.ANDANDO_VAZIA);
                }
                break;

            case EstadosDaPa.ANDANDO_VAZIA:

                percorrido = transform.forward * -1 / (tempoLocomocaoVazio / 20);
                rb.velocity = percorrido;

                if (transform.position.x < -10)
                {
                    transform.position = new Vector3(-10, 1, 0);

                    if (pilhaTrem.PorcentagemDeEnchimento() > 0)
                    {
                        GenerateNewValues();
                        tempoAtual = tempoColetaAtual;
                        ChangeState(EstadosDaPa.COLETA);
                    }
                    else
                    {
                        ChangeState(EstadosDaPa.PARADA);
                    }
                    
                }
                break;

        }
    }

    private void EsvaziarCacamba()
    {
        pilhaFinal.AddValue(quantidadeMinerio);
        quantidadeMinerio = 0;
        UpdateText();
    }

    private void EncherCacamba()
    {
        if(pilhaTrem.ReturnValue() < capacidadeAtual )
            quantidadeMinerio = pilhaTrem.ReturnValue();
        else
            quantidadeMinerio = capacidadeAtual;
           
        pilhaTrem.RemoveValue(quantidadeMinerio);
        UpdateText();
    }

    public void UpdateText()
    {
        quantityText.text = quantidadeMinerio.ToString();
    }

    public void ChangeState(EstadosDaPa newState)
    {
        rb.velocity = Vector3.zero;
        statesPa = newState;
    }
}
