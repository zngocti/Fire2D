using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour {

    public Button botonIniciar;
    public Button botonInstrucciones;
    public Button botonCreditos;
    public Button botonSalir;

    // Use this for initialization
    void Start () {
        Button btnIni = botonIniciar.GetComponent<Button>();
        btnIni.onClick.AddListener(IniciarPartida);

        Button btnInst = botonInstrucciones.GetComponent<Button>();
        btnInst.onClick.AddListener(Instrucciones);

        Button btnCred = botonCreditos.GetComponent<Button>();
        btnCred.onClick.AddListener(Creditos);

        Button btnSalir = botonSalir.GetComponent<Button>();
        btnSalir.onClick.AddListener(SalirDelJuego);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IniciarPartida()
    {
        SceneManager.LoadScene("Pantalla Principal");
    }

    void Instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    void SalirDelJuego()
    {
        Application.Quit();
    }
}
