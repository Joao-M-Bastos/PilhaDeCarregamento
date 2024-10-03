using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TremState
{
    CHEGANDO,
    POSICIONANDO,
    ABRINDO_PORTAS,
    DESCARREGANDO,
    SAINDO
}

public class Trem : MonoBehaviour
{
    [Header("Caracteristicas do Trem")]
    [SerializeField] int quantidadeDeVagoes;
    [SerializeField] float densidadeMinimaDoMinerio;
    [SerializeField] float densidadeMaximaDoMinerio;
    [SerializeField] float volumeMinimoCacamba;
    [SerializeField] float volumeMaximoCacamba;

    [Space(10), Header("Ciclo")]
    [SerializeField] int carrosPorCiclo;
    [SerializeField] int operariosPorCiclo;
    [SerializeField] float eficienciaMaximaDoCiclo;
    [SerializeField] float eficienciaMinimaDoCiclo;

    [Space(10), Header("Posicionamento")]
    [SerializeField] float distanciaPosicionamento;
    [SerializeField] float velocidadePosicionamentoMinima;
    [SerializeField] float velocidadePosicionamentoMaxima;

    [Space(10), Header("Abertura")]
    [SerializeField] int quantidadeComportas;
    [SerializeField] float tempoMaximoAberturaComporta;
    [SerializeField] float tempoMinimoAberturaComporta;

    [Space(10), Header("Descarga")]
    [SerializeField] float eficienciaDevidoAoMaterial;
    [SerializeField] float tempoMinimoEscoamento;
    [SerializeField] float tempoMaximoEscoamento;

    Rigidbody tremRigidBody;

    [Space(10), Header("Feedback")]
    [SerializeField]TremState state = new TremState();

    [SerializeField] float quantidadeMinerio;
    float quantidadeDescarga;

    [SerializeField] float tempoAtual;

    [Space(5), Header("Valores Gerados")]
    [SerializeField] float capacidadeVagao;
    [SerializeField] float tempoPosicionamento;
    [SerializeField] float tempoAbertura;
    [SerializeField] float tempoDescarga;

    Pilha pilhaTrem;
    [SerializeField] TextMeshProUGUI quantityText;

    private void Awake()
    {
        pilhaTrem = GameObject.FindGameObjectWithTag("PilhaTrem").GetComponent<Pilha>();
        tremRigidBody = GetComponent<Rigidbody>();
        
        
    }

    private void Start()
    {
        float[] values = DataBase.GetInstance().GetTremValues();

        quantidadeDeVagoes = (int)values[0];
        eficienciaDevidoAoMaterial = values[1];
        volumeMinimoCacamba = values[2];
         volumeMaximoCacamba = values[3];

        carrosPorCiclo = (int)values[4];
        operariosPorCiclo = (int)values[5];
        eficienciaMinimaDoCiclo = values[6];
        eficienciaMaximaDoCiclo = values[7];
         

         distanciaPosicionamento = values[8];
        quantidadeComportas = (int)values[9];
        velocidadePosicionamentoMinima = values[10];
         velocidadePosicionamentoMaxima = values[11];

        tempoMinimoAberturaComporta = values[12];
        tempoMaximoAberturaComporta = values[13];
         

         
         tempoMinimoEscoamento = values[14];
         tempoMaximoEscoamento = values[15];
        GerarValoresDoTrem();
    }

    public void GerarValoresDoTrem()
    {
        capacidadeVagao = ManagerScript.instance.GetDensity() * DentroDosParametros(volumeMinimoCacamba, volumeMaximoCacamba) * ManagerScript.instance.GetMultiplier();
        tempoPosicionamento = 3.6f * distanciaPosicionamento / DentroDosParametros(velocidadePosicionamentoMinima, velocidadePosicionamentoMaxima);
        tempoAbertura = quantidadeComportas * DentroDosParametros(tempoMinimoAberturaComporta, tempoMaximoAberturaComporta) * carrosPorCiclo / operariosPorCiclo;
        tempoDescarga = DentroDosParametros(tempoMinimoEscoamento, tempoMaximoEscoamento) / (eficienciaDevidoAoMaterial /100);
        quantidadeMinerio = capacidadeVagao * quantidadeDeVagoes;
    }

    public float DentroDosParametros(float minimo, float maximo)
    {
        return Random.Range(minimo, maximo);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TremState.CHEGANDO:
                transform.Translate((transform.forward) * Time.deltaTime);
                UpdateText();

                if (transform.position.z >= -10)
                {
                    tempoAtual = tempoPosicionamento;
                    transform.position = new Vector3(-15, 1, -10);
                    state = TremState.POSICIONANDO;
                }
                break;
            case TremState.POSICIONANDO:
                tempoAtual -= Time.deltaTime;
                tremRigidBody.velocity = transform.forward / (tempoPosicionamento / 10);
                if (transform.position.z > 0)
                {
                    transform.position = new Vector3(-15, 1, 0);
                    tremRigidBody.velocity = Vector3.zero;
                    state = TremState.ABRINDO_PORTAS;
                    tempoAtual += tempoAbertura;
                }
                break;
            case TremState.ABRINDO_PORTAS:
                tempoAtual -= Time.deltaTime;
                if(tempoAtual <= 0)
                {
                    state = TremState.DESCARREGANDO;
                    tempoAtual += ((tempoDescarga + tempoPosicionamento + tempoAbertura) * Mathf.CeilToInt(quantidadeDeVagoes / carrosPorCiclo)) - tempoAbertura;
                    quantidadeDescarga = (quantidadeMinerio / tempoAtual);
                }
                break;
            case TremState.DESCARREGANDO:
                tempoAtual -= Time.deltaTime;

                float quantidadeDescargaFrame = quantidadeDescarga * Time.deltaTime;
                if(quantidadeDescargaFrame > quantidadeMinerio)
                {
                    pilhaTrem.AddValue(quantidadeMinerio);
                    quantidadeMinerio = 0;
                    UpdateText();
                }
                else
                {
                    quantidadeMinerio -= quantidadeDescargaFrame;
                    pilhaTrem.AddValue(quantidadeDescargaFrame);
                    UpdateText();
                }
                

                if (quantidadeMinerio <= 0)
                {
                    state = TremState.SAINDO;
                    tempoAtual += tempoDescarga;
                    UpdateText();
                }

                break;

            case TremState.SAINDO:
                transform.Translate((transform.forward) * Time.deltaTime);

                if (transform.position.z >= 10)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
    public void UpdateText()
    {
        quantityText.text = quantidadeMinerio.ToString();
    }

}
