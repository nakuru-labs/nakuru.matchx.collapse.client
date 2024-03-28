using MatchX.Common;
using MatchX.Engine;
using Nakuru.Entities.Hybrid.Presentation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MatchX.Client
{

	[RequireMatchingQueriesForUpdate]
	public partial struct OnElementCreatedOutputSystem : ISystem
	{
		[BurstCompile]
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<Board.Tag>();
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			
			foreach (var (idRo, positionRo, entity) in SystemAPI.Query<RefRO<Element.Id>, RefRO<Board.Position>>()
			                                      .WithAll<EngineOutput.ElementCreated>()
			                                      .WithEntityAccess()) {
				ecb.AddComponent<ReadyToDestroy>(entity);

				var boardEntity = SystemAPI.GetSingletonEntity<Board.Tag>();

				var factory = new ViewElement.Factory();
				factory.Create(ecb)
				       .WithName($"Element<{idRo.ValueRO.Value}>")
				       .WithGameObjectFromAddressables("Blocks/P_Block_Green.prefab")
				       .WithPosition(new float3(positionRo.ValueRO.Value.xy, 0f))
				       .WithParent(boardEntity)
				       .WithComponent<Element.Tag>()
				       .WithComponent(idRo.ValueRO)
				       .WithComponent(new Board.Position { Value = positionRo.ValueRO.Value });
			}
			
			ecb.Playback(state.EntityManager);
		}
	}

}