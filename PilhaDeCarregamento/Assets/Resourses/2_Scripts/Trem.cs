using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        tremRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TremState.CHEGANDO:
                tremRigidBody.velocity = transform.forward;

                if(transform.position.z > -5)
                {
                    state= TremState.POSICIONANDO;
                }
                break;
            case TremState.POSICIONANDO:
                tremRigidBody.velocity = transform.forward / 40 / 2;
                if (transform.position.z > 0)
                {
                    tremRigidBody.velocity = Vector3.zero;
                    state = TremState.ABRINDO_PORTAS;
                }
                break;
            case TremState.ABRINDO_PORTAS:
                Debug.Log("a");
                break;
        }
    }

    public float GerarValorDentroDosParametros(float minimo, float maximo)
    {
        return Random.Range(minimo, maximo);
    }
}
