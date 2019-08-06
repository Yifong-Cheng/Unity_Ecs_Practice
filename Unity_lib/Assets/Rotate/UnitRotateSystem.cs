using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class UnitRotateSystem : JobComponentSystem
{
    [BurstCompile]
    public struct PlayerRotateJob : IJobForEach<Rotation, UnitRotateData>
    {
        public float data;
        public void Execute(ref Rotation rotation,[ReadOnly] ref UnitRotateData rotateData)
        {
            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotateData.RotateSpeed * data));
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new PlayerRotateJob
        {
            data = Time.deltaTime
        };
        return job.Schedule(this, inputDeps);
    }

}
