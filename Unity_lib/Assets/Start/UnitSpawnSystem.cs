﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class UnitSpawnSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_bufferSystem;


    struct spawnObj : IJobForEachWithEntity<UnitSpawnData, LocalToWorld>
    {
        public EntityCommandBuffer m_commandBuffer;

        public void Execute(Entity entity, int index,[ReadOnly] ref UnitSpawnData data,[ReadOnly] ref LocalToWorld location)
        {
            for(int x=0; x< data.count_x; x++)
            {
                for(int y=0; y<data.count_y; y++)
                {
                    var instance = m_commandBuffer.Instantiate(data.prefab);
                    //var position = math.transform(location.Value, new float3(x * 5, 0, y * 5));
                    var position = math.transform(location.Value, new float3(x * 2, (x+1)%data.count_x , y * 2));

                    //something else...
                    var rotatedata = new UnitRotateData
                    {
                        RotateSpeed = 5f,
                    };

                    var movedata = new UnitMoveData
                    {
                        velocity = new float3(0, 1, 0),
                        movespeed = 2,
                    };

                    m_commandBuffer.SetComponent(instance, new Translation { Value = position });

                    //if want to add other compoments...
                    //...
                    m_commandBuffer.AddComponent(instance, rotatedata);
                    m_commandBuffer.AddComponent(instance, movedata);
                }
            }
            m_commandBuffer.DestroyEntity(entity);
        }
    }

    protected override void OnCreate()
    {
        m_bufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new spawnObj
        {
            m_commandBuffer = m_bufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_bufferSystem.AddJobHandleForProducer(job);
        return job;
    }

    
}
