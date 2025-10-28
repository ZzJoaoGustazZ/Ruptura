using UnityEngine;
using UnityEngine.UI; // Precisamos disso para interagir com o Button
using UnityEngine.Events; // Precisamos disso para o AddListener

public class GerenciadorDeSequencia : MonoBehaviour
{
    [Header("Componentes da Sequ�ncia")]
    public GameObject objetoDoBotao; // Arraste o GameObject do seu bot�o aqui
    public Animator animadorParaExecutar; // Arraste o Animator que tocar� a anima��o
    public AudioSource audioParaParar; // Arraste a fonte de �udio que deve parar

    [Header("Configura��es")]
    public string nomeDoTriggerDaAnimacao; // O nome exato do "Trigger" no seu Animator

    private Button botaoInterno; // Refer�ncia interna para o componente Button

    void Start()
    {
        // Garante que o bot�o comece desativado
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(false);

            // 1. Pega o componente "Button" dentro do GameObject
            botaoInterno = objetoDoBotao.GetComponent<Button>();

            if (botaoInterno != null)
            {
                // 2. Adiciona um "ouvinte" ao clique do bot�o via c�digo.
                // Isso diz ao bot�o: "Quando voc� for clicado, chame a fun��o 'IniciarSequencia'"
                botaoInterno.onClick.AddListener(IniciarSequencia);
            }
            else
            {
                Debug.LogError("O 'objetoDoBotao' n�o tem um componente 'Button' nele!", this);
            }
        }
    }

    // Esta � a fun��o principal que � chamada pelo clique do bot�o
    private void IniciarSequencia()
    {
        Debug.Log("Bot�o clicado! Iniciando a sequ�ncia de eventos...");

        // --- Execute sua s�rie de eventos aqui ---

        // Evento 1: Esconder o bot�o (para n�o ser clicado de novo)
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(false);
        }

        // Evento 2: Parar o som
        if (audioParaParar != null && audioParaParar.isPlaying)
        {
            Debug.Log("Parando o �udio: " + audioParaParar.name);
            audioParaParar.Stop();
        }

        // Evento 3: Executar a anima��o
        if (animadorParaExecutar != null && !string.IsNullOrEmpty(nomeDoTriggerDaAnimacao))
        {
            Debug.Log("Disparando trigger de anima��o: " + nomeDoTriggerDaAnimacao);
            animadorParaExecutar.SetTrigger(nomeDoTriggerDaAnimacao);
        }

        // Voc� pode adicionar quantos eventos quiser aqui...
    }

    // --- Fun��es P�blicas para Ativar ---

    // Chame esta fun��o de outro script (como o seu trigger de porta) para fazer o bot�o aparecer
    public void MostrarBotao()
    {
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(true);
        }
    }

    // Boa pr�tica: remover o listener quando o objeto for destru�do
    void OnDestroy()
    {
        if (botaoInterno != null)
        {
            botaoInterno.onClick.RemoveListener(IniciarSequencia);
        }
    }
}