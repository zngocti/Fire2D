using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personajes : MonoBehaviour {

    float fuerzaX = 10;
    float fuerzaY = 10;
    float inicialX1 = 140;
    float inicialX2 = 75;
    float inicialX3 = 65;
    float inicialX = 10;
    float inicialY = 200;

    Rigidbody2D miRigidbody;
    SpriteRenderer miSprite;

    public bool esUnPersonaje = true;
    public bool esUnaVida = false;
    public bool esUnaBomba = false;

    bool enElSuelo = false;

    bool estaEnUso = false;

    AdministradorVidasPuntos miAdministrador;
    CreacionDePersonajes miCreador;

    Animator miAnimator;

    public AudioClip rebote;
    public AudioClip explosion;
    public AudioClip vida;
    public AudioClip caida;

    AudioSource sonidosAudioSource;

    bool efectoUsado = false;

    public 

	// Use this for initialization
	void Start () {
        miSprite = GetComponent<SpriteRenderer>();
        miRigidbody = GetComponent<Rigidbody2D>();

        miAnimator = GetComponent<Animator>();
    }

    public void SetAdministrador(GameObject miAdmin)
    {
        miAdministrador = miAdmin.GetComponent<AdministradorVidasPuntos>();
        miCreador = miAdmin.GetComponent<CreacionDePersonajes>();
        sonidosAudioSource = miAdmin.GetComponent<AudioSource>();
    }

    public void DesactivarUso()
    {
        estaEnUso = false;
        miRigidbody.isKinematic = true;
        miRigidbody.gravityScale = 0;
        miRigidbody.velocity = Vector2.zero;
        transform.position = new Vector3(-100, 0, 0);

        if (esUnaBomba && enElSuelo)
        {
            miAnimator.ResetTrigger("Desaparece");
        }
        else if (esUnaBomba)
        {
            miAnimator.ResetTrigger("Explota");
        }
        else if (esUnPersonaje && enElSuelo)
        {
            miAnimator.ResetTrigger("Suelo");
            miAnimator.ResetTrigger("Salto");
            miAnimator.SetTrigger("Reset");
        }
        else if (esUnPersonaje)
        {
            miAnimator.ResetTrigger("Salto");
            miAnimator.SetTrigger("Reset");
        }
        else if (esUnaVida && enElSuelo)
        {
            miAnimator.ResetTrigger("Suelo");
            miAnimator.SetTrigger("Reset");
        }
    }

    public void PosicionarObjeto(Transform unTransform, int num)
    {
        miRigidbody.isKinematic = true;

        if(esUnPersonaje || (esUnaVida && enElSuelo))
        {
            miAnimator.ResetTrigger("Reset");
        }
        enElSuelo = false;

        switch (num)
        {
            case 1:
                inicialX = inicialX1;
                break;
            case 2:
                inicialX = inicialX2;
                break;
            case 3:
                inicialX = inicialX3;
                break;
            default:
                inicialX = inicialX3;
                break;
        }

        switch (Random.Range(0,5))
        {
            case 0:
                fuerzaX = 180;
                fuerzaY = 300;
                break;
            case 1:
            case 2:
                fuerzaX = 140;
                fuerzaY = 400;
                break;
            case 3:
            case 4:
                fuerzaX = 100;
                fuerzaY = 600;
                break;
            default:
                fuerzaX = 90;
                fuerzaY = 600;
                break;
        }
        transform.position = unTransform.position;
        estaEnUso = true;
        efectoUsado = false;
    }

    public void IniciarRebote()
    {
        miRigidbody.isKinematic = false;
        miRigidbody.gravityScale = 1;
        miRigidbody.AddForce(Vector2.right * inicialX + Vector2.up * inicialY);

        if (esUnPersonaje)
        {
            miAnimator.SetTrigger("Salto");
        }
    }

    void Impulsar()
    {
        miRigidbody.velocity = Vector2.zero;
        miRigidbody.AddForce(Vector2.up * fuerzaY + Vector2.right * fuerzaX);
        sonidosAudioSource.PlayOneShot(rebote);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag.Equals("Finish"))
        {

            if(esUnaVida && !efectoUsado)
            {
                efectoUsado = true;
                sonidosAudioSource.PlayOneShot(vida);
                miAdministrador.AumentarVidas();
                DesactivarUso();
            }
            else if(esUnaBomba && !efectoUsado)
            {
                efectoUsado = true;
                miRigidbody.isKinematic = true;
                bajarUnaVida();
                miAnimator.SetTrigger("Explota");
                sonidosAudioSource.PlayOneShot(explosion);
            }
            else
            {
                DesactivarUso();
            }
        }
        else
        {
            RaycastHit2D rayIzq = Physics2D.Raycast((miRigidbody.position - new Vector2(miSprite.bounds.extents.x, 0)), Vector2.left);
            RaycastHit2D rayDer = Physics2D.Raycast((miRigidbody.position + new Vector2(miSprite.bounds.extents.x, 0)), Vector2.right);

            if (!rayIzq && !rayDer)
            {
                if (col.gameObject.tag.Equals("Player"))
                {
                    Impulsar();
                    if (esUnPersonaje)
                    {
                        miAdministrador.AumentarPuntos();
                    }
                }
            }
            else if (!rayIzq && rayDer)
            {
                if (!rayDer.transform.tag.Equals("Player") && col.gameObject.tag.Equals("Player"))
                {
                    Impulsar();
                    if (esUnPersonaje)
                    {
                        miAdministrador.AumentarPuntos();
                    }
                }
            }
            else if (!rayDer && rayIzq)
            {
                if (!rayIzq.transform.tag.Equals("Player") && col.gameObject.tag.Equals("Player"))
                {
                    Impulsar();
                    if (esUnPersonaje)
                    {
                        miAdministrador.AumentarPuntos();
                    }
                }
            }
            else if (!rayIzq.transform.tag.Equals("Player") && !rayDer.transform.tag.Equals("Player") && col.gameObject.tag.Equals("Player"))
            {
                Impulsar();
                if (esUnPersonaje)
                {
                    miAdministrador.AumentarPuntos();
                }
            }
        }

        if (col.gameObject.tag.Equals("Piso"))
        {
            enElSuelo = true;

            if(esUnPersonaje && !efectoUsado)
            {
                efectoUsado = true;
                bajarUnaVida();
                sonidosAudioSource.PlayOneShot(caida);
                miAnimator.SetTrigger("Suelo");
            }
            else if (esUnaBomba)
            {
                miAnimator.SetTrigger("Desaparece");
                Invoke("DesactivarUso", 1.0f);
            }
            else if (esUnaVida)
            {
                miAnimator.SetTrigger("Suelo");
                Invoke("DesactivarUso", 1.0f);
            }
        }
    }

    void ApagarObjetos()
    {
        DesactivarUso();
        miCreador.ApagarObjetos();
        miCreador.PausarTimer();
    }

    public bool GetUso()
    {
        return estaEnUso;
    }

    public void DesactivarEfecto()
    {
        efectoUsado = true;
    }

    void bajarUnaVida()
    {
        miCreador.ApagarEfectos();
        miAdministrador.DisminuirVidas();
        Invoke("ApagarObjetos", 1.0f);
    }
}
