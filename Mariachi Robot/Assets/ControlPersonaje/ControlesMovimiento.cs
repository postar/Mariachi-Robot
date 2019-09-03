using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesMovimiento : MonoBehaviour
{
    FisicasPersonalizado fisicas;
    bool botonEcopeto = false;
    bool botonPistola = false;
    bool botonDash = false;
    bool up = false;
    int direcion = 0;
    float xaxis = 0;
    float yaxis = 0;
    [SerializeField]
    int ModuloVelocidad = 1;
    [SerializeField]
    int FuerzaDeSalto = 80;


    [SerializeField]
    int TimerSalto = 2;
    [SerializeField]
    int RecargaSalto = 0;

    void Start()
    {
        fisicas = GetComponent<FisicasPersonalizado>();
    }

    // Update is called once per frame
    void Update()
    {
        ObtenerImpust();
        Movimiento();
        RecargaTimSalto();
        Salto();
    }

    private void RecargaTimSalto()
    {
        if (RecargaSalto < TimerSalto)
            RecargaSalto++;
    }


    private void ObtenerImpust()
    {
        up = Input.GetButtonDown("Jump") || Input.GetKeyDown("space");
        botonDash = Input.GetKey("w");
        botonEcopeto = Input.GetKey("q");
        botonPistola = Input.GetKey("e");
        xaxis = Input.GetAxis("Horizontal");
        yaxis = Input.GetAxis("Vertical");
        if (xaxis > 0)
            direcion = 1;
        if (xaxis < 0)
            direcion = -1;
    }

    private void Movimiento()
    {
        if (xaxis != 0 && fisicas.EnElSuelo())
        {
            fisicas.AgregarPasoEnX(new Vector3(ModuloVelocidad * xaxis, 0, 0), new Vector3(ModuloVelocidad * xaxis, 0, 0));
        }
    }

    private void Salto()
    {
        if (RecargaSalto == TimerSalto && up) {
            if (fisicas.EnElSuelo())
            {
                var vector = new Vector3(0, FuerzaDeSalto, 0);
                fisicas.AgregarFuerza(vector);
            }
            if (fisicas.Derecha())
            {
                var vector = new Vector3(-FuerzaDeSalto, FuerzaDeSalto, 0);
                fisicas.AgregarFuerza(vector);
            }
            if (fisicas.Izquierda())
            {
                var vector = new Vector3(FuerzaDeSalto, FuerzaDeSalto, 0);
                fisicas.AgregarFuerza(vector);
            }
            RecargaSalto = 0;
        }
    }
}
