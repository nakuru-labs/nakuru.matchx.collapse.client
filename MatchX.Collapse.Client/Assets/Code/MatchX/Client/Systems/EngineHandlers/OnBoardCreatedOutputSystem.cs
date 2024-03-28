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
	public partial struct OnBoardCreatedOutputSystem : ISystem
	{
		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			
			foreach (var (sizeRo, gravityRo, entity) in SystemAPI.Query<RefRO<Board.Size>, RefRO<Board.Gravity>>()
			                                                    .WithAll<EngineOutput.BoardCreated>()
			                                                    .WithEntityAccess()) {
				ecb.AddComponent<ReadyToDestroy>(entity);
				
				new ViewElement.Factory().Create(ecb)
				                         .WithName("Board")
				                         .WithGameObjectNew()
				                         .WithPosition(new float3(-4.5f, -4.5f, 0f))
				                         .WithComponent<Board.Tag>()
				                         .WithComponent(sizeRo.ValueRO)
				                         .WithComponent(gravityRo.ValueRO);
			}
			
			ecb.Playback(state.EntityManager);
		}
	}

}