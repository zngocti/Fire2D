using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour {

    public Button botonMenu;

    // Use this for initialization
    void Start () {
        Button btnMenu = botonMenu.GetComponent<Button>();
        btnMenu.onClick.AddListener(IrMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
