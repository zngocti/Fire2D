using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionDePersonajes : MonoBehaviour {

    public Transform ventana1;
    public Transform ventana2;
    public Transform ventana3;

    public GameObject personajePrefab;
    public GameObject vidaPrefab;
    public GameObject bombaPrefab;

    public const int cantidadDePersonajes = 30;
    public const int cantidadDeBombas = 10;

    GameObject unaVida;
    GameObject[] poolDePersonajes = new GameObject[cantidadDePersonajes];
    GameObject[] poolDeBombas = new GameObject[cantidadDeBombas];

    Personajes rVida;
    Personajes[] poolRPersonajes = new Personajes[cantidadDePersonajes];
    Personajes[] poolRBombas = new Personajes[cantidadDeBombas];

    Personajes objetoV1;
    Personajes objetoV2;
    Personajes objetoV3;

    bool jugando = true;

    bool disponibleV1 = true;
    bool disponibleV2 = true;
    bool disponibleV3 = true;

    float timerV1 = 0;
    float timerV2 = 0;
    float timerV3 = 0;
    float cdVentanas = 1;

    float timer = 0;
    float timerPausa = 0;
    float cdPausa = 3;
    bool timerOn = true;
    int dificultad = 0;
    int dificultadMax = 5;

    int counter = 0;

    // Use this for initialization
    void Start () {

        unaVida = Instantiate(vidaPrefab, new Vector3(-100, 0, 0), Quaternion.identity, this.transform);
        rVida = unaVida.GetComponent<Personajes>();
        rVida.SetAdministrador(gameObject);

        for (int i = 0; i < cantidadDePersonajes; i++)
        {
            poolDePersonajes[i] = Instantiate(personajePrefab, new Vector3(-100, 0, 0), Quaternion.identity, this.transform);
            poolRPersonajes[i] = poolDePersonajes[i].GetComponent<Personajes>();
            poolRPersonajes[i].SetAdministrador(gameObject);
        }

        for (int i = 0; i < cantidadDeBombas; i++)
        {
            poolDeBombas[i] = Instantiate(bombaPrefab, new Vector3(-100, 0, 0), Quaternion.identity, this.transform);
            poolRBombas[i] = poolDeBombas[i].GetComponent<Personajes>();
            poolRBombas[i].SetAdministrador(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (jugando)
        {
            if (timerOn)
            {
                timer = timer + Time.deltaTime;
                PrepararPersonaje();
            }
            else if (timerPausa >= cdPausa)
            {
                timerPausa = 0;
                timerOn = true;
            }
            else
            {
                timerPausa = timerPausa + Time.deltaTime;
            }

            ActualizarVentanas();
        }
	}

    public void GameOver()
    {
        jugando = false;
    }

    void ActualizarVentanas()
    {
        if (!disponibleV1 && timerV1 < cdVentanas)
        {
            timerV1 = timerV1 + Time.deltaTime;
        }
        else if (!disponibleV1)
        {
            disponibleV1 = true;
            timerV1 = 0;
            objetoV1.IniciarRebote();
        }
        if (!disponibleV2 && timerV2 < cdVentanas)
        {
            timerV2 = timerV2 + Time.deltaTime;
        }
        else if (!disponibleV2)
        {
            disponibleV2 = true;
            timerV2 = 0;
            objetoV2.IniciarRebote();
        }
        if (!disponibleV3 && timerV3 < cdVentanas)
        {
            timerV3 = timerV3 + Time.deltaTime;
        }
        else if (!disponibleV3)
        {
            disponibleV3 = true;
            timerV3 = 0;
            objetoV3.IniciarRebote();
        }
    }

    public void IniciarVida()
    {
        if (!rVida.GetUso())
        {
            IniciarObjeto(rVida);
        }
    }

    public void IniciarBomba()
    {
        for (int i = 0; i < cantidadDeBombas; i++)
        {
            if (!poolRBombas[i].GetUso())
            {
                IniciarObjeto(poolRBombas[i]);
                i = cantidadDeBombas;
            }
        }
    }

    public void IniciarPersonaje()
    {
        for (int i = 0; i < cantidadDePersonajes; counter++)
        {
            if (!poolRPersonajes[counter].GetUso())
            {
                IniciarObjeto(poolRPersonajes[counter]);
                i = cantidadDePersonajes;
            }
            if(counter == cantidadDePersonajes - 1)
            {
                counter = 0;
            }
        }
    }

    void IniciarObjeto(Personajes miObjeto)
    {
        bool fueAsignado = false;

        do
        {
            switch (Random.Range(0, 5))
            {
                case 0:
                    if(disponibleV1)
                    {
                        miObjeto.PosicionarObjeto(ventana1, 1);
                        disponibleV1 = false;
                        objetoV1 = miObjeto;
                        fueAsignado = true;
                    }
                    break;
                case 1:
                case 2:
                    if (disponibleV2)
                    {
                        miObjeto.PosicionarObjeto(ventana2, 2);
                        disponibleV2 = false;
                        objetoV2 = miObjeto;
                        fueAsignado = true;
                    }
                    break;
                case 3:
                case 4:
                    if (disponibleV3)
                    {
                        miObjeto.PosicionarObjeto(ventana3, 3);
                        disponibleV3 = false;
                        objetoV3 = miObjeto;
                        fueAsignado = true;
                    }
                    break;
                default:
                    if (disponibleV3)
                    {
                        miObjeto.PosicionarObjeto(ventana3, 3);
                        disponibleV3 = false;
                        objetoV3 = miObjeto;
                        fueAsignado = true;
                    }
                    break;
            }
        } while (!fueAsignado);
    }

    void PrepararPersonaje()
    {
        if (timer >= dificultadMax - dificultad + 1)
        {
            timer = 0;
            int num = Random.Range(0, 99);

            if (num < dificultad * 4)
            {
                IniciarBomba();
            }
            else
            {
                IniciarPersonaje();
            }

        }
    }

    public void ApagarEfectos()
    {
        {
            if (rVida.GetUso())
            {
                rVida.DesactivarEfecto();
            }

            for (int i = 0; i < cantidadDeBombas; i++)
            {
                if (poolRBombas[i].GetUso())
                {
                    poolRBombas[i].DesactivarEfecto();
                }
            }

            for (int i = 0; i < cantidadDePersonajes; i++)
            {
                if (poolRPersonajes[i].GetUso())
                {
                    poolRPersonajes[i].DesactivarEfecto();
                }
            }
        }
    }

    public void ApagarObjetos()
    {
        if(rVida.GetUso())
        {
            rVida.DesactivarUso();
        }

        for (int i = 0; i < cantidadDeBombas; i++)
        {
            if (poolRBombas[i].GetUso())
            {
                poolRBombas[i].DesactivarUso();
            }
        }

        for (int i = 0; i < cantidadDePersonajes; i++)
        {
            if (poolRPersonajes[i].GetUso())
            {
                poolRPersonajes[i].DesactivarUso();
            }
        }
    }

    public void PausarTimer()
    {
        timerOn = false;
    }

    public void AumentarDificultad()
    {
        if (dificultad < dificultadMax)
        {
            dificultad++;
        }
    }
}
