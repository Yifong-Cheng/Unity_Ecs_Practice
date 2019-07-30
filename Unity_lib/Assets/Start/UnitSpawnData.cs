using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct UnitSpawnData : IComponentData
{
    public int count_x;
    public int count_y;
    public Entity prefab;
}
