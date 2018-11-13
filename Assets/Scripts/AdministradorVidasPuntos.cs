using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdministradorVidasPuntos : MonoBehaviour {

    int vidas = 3;
    int puntos = 0;

    public Text misPuntos;
    public Text misVidas;
    public Text tuPuntaje;

    public GameObject jugador;
    MovimientoJugador miMovimiento;

    CreacionDePersonajes creadorDePersonajes;
    AudioSource sonidosAudioSource;

    public AudioClip gameOver;

    public GameObject miCanvas;

    // Use this for initialization
    void Start()
    {
        creadorDePersonajes = GetComponent<CreacionDePersonajes>();
        sonidosAudioSource = GetComponent<AudioSource>();
        miMovimiento = jugador.GetComponent<MovimientoJugador>();

        sonidosAudioSource.loop = true;

        misVidas.text = "Vidas: " + vidas;
        misPuntos.text = "Puntos: " + puntos;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AumentarVidas()
    {
        vidas++;
        misVidas.text = "Vidas: " + vidas;

    }

    public void DisminuirVidas()
    {
        vidas--;
        misVidas.text = "Vidas: " + vidas;
        if (vidas < 1)
        {
            creadorDePersonajes.GameOver();
            miMovimiento.GameOver();
            sonidosAudioSource.loop = false;
            sonidosAudioSource.clip = gameOver;
            sonidosAudioSource.Play();
            miCanvas.SetActive(true);
            tuPuntaje.text = "Tu puntaje: " + puntos;
        }
    }

    public void AumentarPuntos()
    {
        puntos++;
        misPuntos.text = "Puntos: " + puntos;
        if (puntos % 75 == 0)
        {
            creadorDePersonajes.IniciarVida();
        }
        if (puntos % 25 == 0)
        {
            creadorDePersonajes.AumentarDificultad();
        }
    }
}
