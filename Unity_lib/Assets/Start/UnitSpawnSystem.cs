using Unity.Burst;
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
                    var position = math.transform(location.Value, new float3(x * 5, 0, y * 5));

                    //something else...

                    m_commandBuffer.SetComponent(instance, new Translation { Value = position });

                    //if want to add other compoments...
                    //...

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
