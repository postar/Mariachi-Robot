using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public int CantidadBalas;
    public EntityArchetype balaEntity;
    public Mesh meshBala;
    public Material materialBala;

    void Start()
    {
        EntityManager manager = World.Active.EntityManager;
        balaEntity = manager.CreateArchetype(
            typeof(Speed),
            typeof(RenderMesh),
            typeof(Translation),
            typeof(LocalToWorld)
        );
    }
}
