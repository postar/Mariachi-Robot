using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicasPersonalizado : MonoBehaviour
{
    Rigidbody rb;
    bool enElSuelo = true;
    bool derecha = true;
    bool izquierda = true;
    int rozamiento = 4;

    [SerializeField]
    Transform checkSuelo1;
    [SerializeField]
    Transform checkSuelo2;
    [SerializeField]
    Transform checkIzquierda;
    [SerializeField]
    Transform checkDerecha;
    [SerializeField]
    float Maza = 1;

    List<Vector3> velocidades = null;
    Vector3 velocidad = new Vector3(0, 0, 0);
    Vector3 aceleracion = new Vector3(0, 0, 0);
    Vector3 impulso = new Vector3(0, 0, 0);


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidades = new List<Vector3>();
    }

    void Update()
    {
        ObtenerColiciones();
        AplicarRozamiento();

        rb.velocity += impulso;
        impulso = new Vector3(0, 0, 0);
    }

    public void AplicarRozamiento()
    {
        if(rb.velocity.x != 0 && enElSuelo)
        {
            if(rb.velocity.x < 0)
                rb.AddForce(new Vector3(rozamiento, 0, 0));
            else
                rb.AddForce(new Vector3(-rozamiento, 0, 0));
        }
        if (rb.velocity.y != 0 && (izquierda || derecha))
        {
            if (rb.velocity.x < 0)
                rb.AddForce(new Vector3(0, rozamiento, 0));
            else
                rb.AddForce(new Vector3(0, -rozamiento, 0));
        }
    }

    private void ObtenerColiciones()
    {
        rb.mass = Maza;
        
        enElSuelo = Physics.Linecast(transform.position, checkSuelo1.position, LayerMask.NameToLayer("piso")) || Physics.Linecast(transform.position, checkSuelo2.position, LayerMask.NameToLayer("piso"));
        derecha = Physics.Linecast(transform.position, checkDerecha.position, LayerMask.NameToLayer("piso"));
        izquierda = Physics.Linecast(transform.position, checkIzquierda.position, LayerMask.NameToLayer("piso"));
    }

    public void SetVelocidad(Vector3 velocidad)
    {
        rb.velocity = velocidad;
    }
    public void AgregarVelocidad(Vector3 velocidad)
    {
        this.velocidad += velocidad;
    }
    public void AgregarPasoEnX(Vector3 paso, Vector3 velocidadMaxima) //Por ahora solo en X //esta funcion de seguro tiene una forma mejor de hacerse.
    {
        if (velocidadMaxima.x > 0) {
            if (velocidadMaxima.x > rb.velocity.x) {
                if (velocidadMaxima.x > rb.velocity.x + paso.x)
                {
                    rb.velocity += new Vector3(paso.x, 0, 0);
                } else {
                    rb.velocity += new Vector3(velocidadMaxima.x - rb.velocity.x, 0, 0);
                }
            }
        }
        if (velocidadMaxima.x < 0)
        {
            if (velocidadMaxima.x < rb.velocity.x)
            {
                if (velocidadMaxima.x < rb.velocity.x + paso.x)
                {
                    rb.velocity += new Vector3(paso.x, 0, 0);
                }
                else
                {
                    rb.velocity += new Vector3(velocidadMaxima.x - rb.velocity.x, 0, 0);
                }
            }
        }
    }
    public void AgregarFuerza(Vector3 fuerza)
    {
        rb.AddForce(fuerza);
    }
    public void AgregarImpulso(Vector3 impulso)
    {
        this.impulso += impulso / Maza;
    }

    public bool EnElSuelo()
    {
        return enElSuelo;
    }
    public bool Derecha()
    {
        return derecha;
    }
    public bool Izquierda()
    {
        return izquierda;
    }
    
}
