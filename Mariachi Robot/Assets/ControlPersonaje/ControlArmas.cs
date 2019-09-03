using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArmas : MonoBehaviour
{

    bool botonEcopeto = false;
    bool botonPistola = false;
    bool botonDash = false;
    float xaxis = 0;
    float yaxis = 0;
    int direcion = 1;
    FisicasPersonalizado fisicas;

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

    void Start()
    {
        fisicas = GetComponent<FisicasPersonalizado>();
    }

    private void FixedUpdate()
    {
        ObtenerImpust();

        //Dash();
        RecargaDeArmas();
        DisparoArmas();
    }

    private void ObtenerImpust()
    {
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

    private void RecargaDeArmas()
    {
        if (RecargaEscopeta < TimerEscoperta)
            RecargaEscopeta++;
        if (RecargaSprite < TimerSprite)
            RecargaSprite++;
        if (RecargaPistolas < TimerPistolas)
            RecargaPistolas++;
    }

    private void DisparoArmas()
    {
        if (fisicas.EnElSuelo() && botonDash && RecargaSprite == TimerSprite)
        {
            RecargaSprite = 0;
            var impulso = new Vector3(direcion * ImpulsoDeSprite, 0, 0);
            fisicas.AgregarImpulso(impulso);
        }
        if (botonEcopeto && RecargaEscopeta == TimerEscoperta)
        {
            RecargaEscopeta = 0;

            var impulso = new Vector3(-direcion, -yaxis, 0)* ImpulsoEscopeta;
            fisicas.AgregarImpulso(impulso);
        }
        if (botonPistola)
        {
            var velocidad = new Vector3(0, 0, 0);
            fisicas.SetVelocidad(velocidad);
        }
    }
}
