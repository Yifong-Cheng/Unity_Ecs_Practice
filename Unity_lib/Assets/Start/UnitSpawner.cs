using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class UnitSpawner : MonoBehaviour , IConvertGameObjectToEntity , IDeclareReferencedPrefabs
{
    public int Count_X;
    public int Count_Y;
    public GameObject Prefab;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnData = new UnitSpawnData
        {
            count_x = Count_X,
            count_y = Count_Y,
            prefab = conversionSystem.GetPrimaryEntity(Prefab)
        };

        dstManager.AddComponentData(entity, spawnData);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(Prefab);
    }

    
}
