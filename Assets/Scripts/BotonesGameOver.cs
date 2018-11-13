using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonesGameOver : MonoBehaviour {

    public Button botonReplay;
    public Button botonMenu;

    // Use this for initialization
    void Start () {
        Button btnRpl = botonReplay.GetComponent<Button>();
        btnRpl.onClick.AddListener(Replay);

        Button btnMenu = botonMenu.GetComponent<Button>();
        btnMenu.onClick.AddListener(IrMenu);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void Replay()
    {
        SceneManager.LoadScene("Pantalla Principal");
    }

    void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
