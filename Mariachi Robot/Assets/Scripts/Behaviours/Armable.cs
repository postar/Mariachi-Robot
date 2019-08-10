using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

public class Armable : MonoBehaviour
{
    public Arma arma;
    public KeyCode botonDisparo;
    
    void Update()
    {
        if (Input.GetKeyDown(botonDisparo))
        {
            EntityManager manager = World.Active.EntityManager;
            NativeArray<Entity> entities = new NativeArray<Entity>(arma.CantidadBalas, Allocator.Temp);
            manager.CreateEntity(arma.balaEntity, entities);
            for (int i = 0; i < arma.CantidadBalas; i++)
            {
                Entity entity = entities[i];
                manager.SetComponentData(entity, new Translation { Value = new float3(transform.position.x, transform.position.y, transform.position.z) });
                manager.SetSharedComponentData(entity,
                    new RenderMesh {
                        mesh = arma.meshBala,
                        material = arma.materialBala
                    }
                );
            }
        }
    }
}
