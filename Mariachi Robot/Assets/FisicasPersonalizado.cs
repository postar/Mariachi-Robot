using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicasPersonalizado : MonoBehaviour
{
    Rigidbody rb;
    bool enElSuelo = true;
    bool derecha = true;
    bool izquierda = true;

    [SerializeField]
    Transform checkSuelo;
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
        //Vector3 velocidadFinale = new Vector3(0,0,0);
        //foreach (var velocidad in velocidades)
        //    velocidadFinale += velocidad;
        //rb.velocity = velocidadFinale;
        //velocidades.Clear();
        var velocidadFinal = velocidad + impulso;
        var deltaTime = Time.deltaTime;
        velocidad = new Vector3(0, 0, 0);
        velocidadFinal += aceleracion * deltaTime;
        rb.velocity = velocidadFinal;
    }

    private void ObtenerColiciones()
    {
        rb.mass = Maza;

        enElSuelo = Physics.Linecast(transform.position, checkSuelo.position, LayerMask.NameToLayer("piso"));
        derecha = Physics.Linecast(transform.position, checkDerecha.position, LayerMask.NameToLayer("piso"));
        izquierda = Physics.Linecast(transform.position, checkIzquierda.position, LayerMask.NameToLayer("piso"));
    }

    public void AgregarVelocidad(Vector3 velocidad)
    {
        //velocidades.Add(velocidad);
        this.velocidad += velocidad;
    }

    public void AgregarFuerza(Vector3 fuerza)
    {
        //velocidades.Add(velocidad/Maza);
         aceleracion += fuerza / Maza;
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
