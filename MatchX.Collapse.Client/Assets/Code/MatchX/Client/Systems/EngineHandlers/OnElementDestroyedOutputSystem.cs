using MatchX.Common;
using MatchX.Engine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MatchX.Client
{

	[RequireMatchingQueriesForUpdate]
	public partial struct OnElementDestroyedOutputSystem : ISystem
	{
		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			var elementsToDestroy = new NativeHashSet<uint>(2, Allocator.Temp);
			
			foreach (var (idRo, entity) in SystemAPI.Query<RefRO<Element.Id>>()
			                                                    .WithAll<EngineOutput.ElementDestroyed>()
			                                                    .WithEntityAccess()) {
				ecb.AddComponent<ReadyToDestroy>(entity);
				elementsToDestroy.Add(idRo.ValueRO.Value);
			}
			
			foreach (var (idRo, entity) in SystemAPI.Query<RefRO<Element.Id>>()
			                                        .WithAll<Element.Tag>()
			                                        .WithEntityAccess()) {
				if (!elementsToDestroy.Contains(idRo.ValueRO.Value))
					continue;
				
				ecb.AddComponent<ReadyToDestroy>(entity);
			}
			
			ecb.Playback(state.EntityManager);
		}
	}

}