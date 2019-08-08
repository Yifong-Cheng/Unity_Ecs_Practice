using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class UnitMoveSystem : JobComponentSystem
{

    [BurstCompile]
    public struct UnitMoveJob : IJobForEach<Translation, Rotation, UnitMoveData>
    {
        public float velocity;
        public void Execute(ref Translation translation, ref Rotation rotation, ref UnitMoveData moveData)
        {
            if(translation.Value.y>2f && moveData.velocity.y==1)
            {
                moveData.velocity.y = -1;
            }
            else if(translation.Value.y<-2f && moveData.velocity.y==-1)
            {
                moveData.velocity.y = 1;
            }

            moveData.movedirection = math.forward(rotation.Value);
            float3 dt = moveData.movedirection * moveData.movespeed + new float3(0, velocity, 0) * moveData.velocity;
            translation.Value = translation.Value + dt;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var movejob = new UnitMoveJob()
        {
            velocity = .5f,
        };
        return movejob.Schedule(this, inputDeps);
    }
}
