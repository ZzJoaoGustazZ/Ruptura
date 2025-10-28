using UnityEngine;
using UnityEngine.UI; // Precisamos disso para interagir com o Button
using UnityEngine.Events; // Precisamos disso para o AddListener

public class GerenciadorDeSequencia : MonoBehaviour
{
    [Header("Componentes da Sequência")]
    public GameObject objetoDoBotao; // Arraste o GameObject do seu botão aqui
    public Animator animadorParaExecutar; // Arraste o Animator que tocará a animação
    public AudioSource audioParaParar; // Arraste a fonte de áudio que deve parar

    [Header("Configurações")]
    public string nomeDoTriggerDaAnimacao; // O nome exato do "Trigger" no seu Animator

    private Button botaoInterno; // Referência interna para o componente Button

    void Start()
    {
        // Garante que o botão comece desativado
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(false);

            // 1. Pega o componente "Button" dentro do GameObject
            botaoInterno = objetoDoBotao.GetComponent<Button>();

            if (botaoInterno != null)
            {
                // 2. Adiciona um "ouvinte" ao clique do botão via código.
                // Isso diz ao botão: "Quando você for clicado, chame a função 'IniciarSequencia'"
                botaoInterno.onClick.AddListener(IniciarSequencia);
            }
            else
            {
                Debug.LogError("O 'objetoDoBotao' não tem um componente 'Button' nele!", this);
            }
        }
    }

    // Esta é a função principal que é chamada pelo clique do botão
    private void IniciarSequencia()
    {
        Debug.Log("Botão clicado! Iniciando a sequência de eventos...");

        // --- Execute sua série de eventos aqui ---

        // Evento 1: Esconder o botão (para não ser clicado de novo)
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(false);
        }

        // Evento 2: Parar o som
        if (audioParaParar != null && audioParaParar.isPlaying)
        {
            Debug.Log("Parando o áudio: " + audioParaParar.name);
            audioParaParar.Stop();
        }

        // Evento 3: Executar a animação
        if (animadorParaExecutar != null && !string.IsNullOrEmpty(nomeDoTriggerDaAnimacao))
        {
            Debug.Log("Disparando trigger de animação: " + nomeDoTriggerDaAnimacao);
            animadorParaExecutar.SetTrigger(nomeDoTriggerDaAnimacao);
        }

        // Você pode adicionar quantos eventos quiser aqui...
    }

    // --- Funções Públicas para Ativar ---

    // Chame esta função de outro script (como o seu trigger de porta) para fazer o botão aparecer
    public void MostrarBotao()
    {
        if (objetoDoBotao != null)
        {
            objetoDoBotao.SetActive(true);
        }
    }

    // Boa prática: remover o listener quando o objeto for destruído
    void OnDestroy()
    {
        if (botaoInterno != null)
        {
            botaoInterno.onClick.RemoveListener(IniciarSequencia);
        }
    }
}