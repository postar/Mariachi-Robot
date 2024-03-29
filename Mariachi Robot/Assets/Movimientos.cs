﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    Rigidbody rb;
    int salto = 10;
    bool enElSuelo = true;
    bool derecha= true;
    bool izquierda = true;
    bool botonEcopeto = false;
    bool botonPistola = false;
    bool botonDash = false;
    bool up = false;
    int direcion = 0;
    float xaxis = 0;
    float yaxis = 0;
    //Vector3 velocidadAnterior = new Vector3(0, 0, 0);
    //Vector3 fuerzaAnterior = new Vector3(0,0,0);

    [SerializeField]
    Transform checkSuelo;
    [SerializeField]
    Transform checkIzquierda;
    [SerializeField]
    Transform checkDerecha;
    [SerializeField]
    int FuerzaDeSalto = 80;
    [SerializeField]
    int ModuloVelocidad = 4;
    [SerializeField]
    bool CorrerConAceleracion = true;
    [SerializeField]
    int FuerzadeCorrida = 10;
    [SerializeField]
    int FuerzadeFriccion = 1;
    [SerializeField]
    float Maza = 1;

    [SerializeField]
    int ImpulsoDeSprite = 40;
    [SerializeField]
    int ImpulsoEscopeta = 40;
    [SerializeField]
    int TimerEscoperta = 20;
    [SerializeField]
    int RecargaEscopeta = 0;
    [SerializeField]
    int TimerSprite = 20;
    [SerializeField]
    int RecargaSprite = 0;
    [SerializeField]
    int TimerPistolas = 20;
    [SerializeField]
    int RecargaPistolas = 0;
    [SerializeField]
    Rigidbody MunicionEscopeta;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        ObtenerImpust();
        Movimiento();
        AplicarRozamiento();
        Salto();
        //Dash();
        RecargaDeArmas();
        DisparoARmas();
    }

    private void ObtenerImpust()
    {
        up = Input.GetButtonDown("Jump") || Input.GetButtonDown("space");
        botonDash = Input.GetKey("w");
        botonEcopeto = Input.GetKey("q");
        botonPistola = Input.GetKey("e");
        rb.mass = Maza;
        xaxis = Input.GetAxis("Horizontal");
        yaxis = Input.GetAxis("Vertical");
        if (xaxis > 0)
            direcion = 1;
        if (xaxis < 0)
            direcion = -1;
        enElSuelo = Physics.Linecast(transform.position, checkSuelo.position, LayerMask.NameToLayer("piso"));
        derecha = Physics.Linecast(transform.position, checkDerecha.position, LayerMask.NameToLayer("piso"));
        izquierda = Physics.Linecast(transform.position, checkIzquierda.position, LayerMask.NameToLayer("piso"));
    }

    private void Movimiento()
    {
        if (xaxis != 0 && enElSuelo)
        {
            if (!CorrerConAceleracion)
            {
                var velocidad = rb.velocity;
                velocidad.x = xaxis * ModuloVelocidad;
                rb.velocity = velocidad;
            }
            else
            {
                var vector = new Vector3(xaxis * FuerzadeCorrida, 0, 0);
                rb.AddForce(vector);
            }
        }
    }

    private void AplicarRozamiento()
    {
        if (enElSuelo)
        {
            if (!CorrerConAceleracion)
            {

            }
            else
            {
                var velocidadX = rb.velocity.x;
                var vector = new Vector3(-velocidadX * FuerzadeFriccion * Maza, 0, 0);
                rb.AddForce(vector);
            }
        }
    }

    private void Salto()
    {
        //if (up && enElSuelo && !derecha && !izquierda)
        //{
        //    var vector = new Vector3(0, FuerzaDeSalto, 0);
        //    rb.AddForce(vector);
        //}
        if (up && derecha)
        {
            var vector = new Vector3(-FuerzaDeSalto, FuerzaDeSalto, 0);
            rb.AddForce(vector);
        }
        if (up && izquierda)
        {
            var vector = new Vector3(FuerzaDeSalto, FuerzaDeSalto, 0);
            rb.AddForce(vector);
        }
    }

    private void Dash()
    {
        if (enElSuelo && botonDash)
        {
            var vector = new Vector3(direcion * (ImpulsoDeSprite / rb.mass), 0, 0);
            rb.velocity = vector;
        }
    }

    private void RecargaDeArmas()
    {
        if (RecargaEscopeta < TimerEscoperta)
            RecargaEscopeta++;
        if (RecargaSprite < TimerSprite)
            RecargaSprite++;
        if (RecargaPistolas < TimerPistolas)
            RecargaPistolas++;
    }

    private void DisparoARmas()
    {
        if (enElSuelo && botonDash && RecargaSprite == TimerSprite)
        {
            RecargaSprite = 0;
            var vector = new Vector3(direcion * (ImpulsoDeSprite / rb.mass), 0, 0);
            //rb.AddForce(vector);
            rb.velocity = vector;
        }
        if (botonEcopeto && RecargaEscopeta == TimerEscoperta)
        {
            RecargaEscopeta = 0;
            //    var Auxilair = 0;
            //    if (xaxis == 0)
            //        Auxilair == direcion;
            var vector = new Vector3(-xaxis, -yaxis, 0);
            vector = vector * (ImpulsoEscopeta / rb.mass);
            //rb.AddForce(vector);
            rb.velocity = vector;

            //var vector2 = vector * -1;
            //var bullet = Instantiate(MunicionEscopeta, rb.position, rb.rotation) as Rigidbody;
            //bullet.AddForce(vector2);
        }
        if (botonPistola)
        {
            var vector = new Vector3(0, 0, 0);
            rb.velocity = vector;
        }
    }
}
