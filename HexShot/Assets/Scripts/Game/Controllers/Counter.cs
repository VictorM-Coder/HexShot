using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI tempoText;
    private float tempoTotal = 3f * 60f;
    private bool cronometroRodando;

    private void Start()
    {
        cronometroRodando = true;
    }

    private void Update()
    {
        if (cronometroRodando)
        {
            tempoTotal -= Time.deltaTime;
            if (tempoTotal <= 0) {
                 SceneManager.LoadScene("Final");
            }
            AtualizarTempoText();
        }
    }

    public void IniciarCronometro()
    {
        cronometroRodando = true;
    }

    public void PararCronometro()
    {
        cronometroRodando = false;
    }

    private void AtualizarTempoText()
    {
        float minutos = Mathf.FloorToInt(tempoTotal / 60);
        float segundos = Mathf.FloorToInt(tempoTotal % 60);
        
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
