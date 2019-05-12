using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje1 : MonoBehaviour
{
    public float fuerza;
    private Rigidbody cuerpo;

    private void Start()
    {
        cuerpo = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float deltaX = Input.GetAxis("Horizontal")*fuerza;

        cuerpo.AddForce(new Vector3(deltaX * Time.fixedDeltaTime, 0));
    }
}