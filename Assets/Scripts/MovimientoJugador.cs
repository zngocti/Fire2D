using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour {

    public float margenAbj = 0.05f;

    bool jugando = true;

    SpriteRenderer miSprite;
    
    float minX = 0;
    float maxX = 0;

    bool puedoDer = true;
    bool puedoIzq = true;

    float[] numerosX = { -6, -1, 4 };
    int indiceX = 1;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (jugando)
        {
            if (Input.GetKeyDown(KeyCode.D) && indiceX < numerosX.Length - 1)
            {
                indiceX++;
                transform.position = new Vector3((numerosX[indiceX]), transform.position.y, transform.position.z);
            }             
            if (Input.GetKeyDown(KeyCode.A) && indiceX > 0)
            {
                indiceX--;
                transform.position = new Vector3((numerosX[indiceX]), transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void GameOver()
    {
        jugando = false;
    }
}
