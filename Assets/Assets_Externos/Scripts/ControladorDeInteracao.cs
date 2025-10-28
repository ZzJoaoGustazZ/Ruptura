using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControladorDeInteracao : MonoBehaviour
{
    [Header("UI de Interação")]
    public GameObject iconeInteracao;
    public GameObject textoInteracao;
    public Image fundopreto; // fundo do texto
    public Image fundobranco; // fundo do icone

    [Header("Configurações da Transição")]
    public Image imagemDeFade;
    public float velocidadeDoFade = 1f;
    private bool estaInteragindo = false;

    [Header("Destino")]
    public Transform pontoDeDestino;
    public string nomeDaCenaParaCarregar;

    void Start()
    {
        // Garante que a UI comece desativada
        if (iconeInteracao != null) iconeInteracao.SetActive(false);
        if (textoInteracao != null) textoInteracao.SetActive(false);
        // CORRIGIDO: Adicionado .gameObject
        if (fundopreto != null) fundopreto.gameObject.SetActive(false);
        if (fundobranco != null) fundobranco.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !estaInteragindo)
        {
            if (iconeInteracao != null) iconeInteracao.SetActive(true);
            if (textoInteracao != null) textoInteracao.SetActive(true);
            // CORRIGIDO: Erro de digitação "f" removido e .gameObject adicionado
            if (fundobranco != null) fundobranco.gameObject.SetActive(true);
            if (fundopreto != null) fundopreto.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (iconeInteracao != null) iconeInteracao.SetActive(false);
            if (textoInteracao != null) textoInteracao.SetActive(false);
            // CORRIGIDO: Adicionado .gameObject
            if (fundobranco != null) fundobranco.gameObject.SetActive(false);
            if (fundopreto != null) fundopreto.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !estaInteragindo && Input.GetKeyDown(KeyCode.E))
        {
            estaInteragindo = true;

            // Esconde a UI de interação
            if (iconeInteracao != null) iconeInteracao.SetActive(false);
            if (textoInteracao != null) textoInteracao.SetActive(false);
            // CORRIGIDO: Adicionado .gameObject
            if (fundobranco != null) fundobranco.gameObject.SetActive(false);
            if (fundopreto != null) fundopreto.gameObject.SetActive(false);

            StartCoroutine(FadeETransicao(other.transform));
        }
    }

    // A coroutine de Fade e Transição continua a mesma...
    private IEnumerator FadeETransicao(Transform jogador)
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * velocidadeDoFade;
            imagemDeFade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        if (pontoDeDestino != null)
        {
            CharacterController cc = jogador.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            jogador.position = pontoDeDestino.position;
            if (cc != null) cc.enabled = true;
        }
        else if (!string.IsNullOrEmpty(nomeDaCenaParaCarregar))
        {
            SceneManager.LoadScene(nomeDaCenaParaCarregar);
        }

        yield return new WaitForSeconds(0.5f);

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * velocidadeDoFade;
            imagemDeFade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        estaInteragindo = false;
    }
}