using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct UnitMoveData : IComponentData
{
    public float3 velocity;
    public float3 movedirection;
    public float movespeed;
}
