using MatchX.Common;
using MatchX.Engine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MatchX.Client
{

	public partial struct OnElementMovedOutputSystem : ISystem
	{
		[BurstCompile]
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<EngineOutput.ElementMoved>();
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			var positionsMap = new NativeHashMap<uint, RefRW<Board.Position>>(10, Allocator.Temp);
			var entitiesMap = new NativeHashMap<uint, Entity>(10, Allocator.Temp);
			
			foreach (var (idRo, boardPositionRw, entity) in SystemAPI.Query<RefRO<Element.Id>, RefRW<Board.Position>>()
			                                                         .WithAll<Element.Tag>()
			                                                         .WithEntityAccess()) {
				positionsMap.Add(idRo.ValueRO.Value, boardPositionRw);
				entitiesMap.Add(idRo.ValueRO.Value, entity);
			}
			
			foreach (var (idRo, boardPositionRo, entity) in SystemAPI.Query<RefRO<Element.Id>, RefRO<Board.Position>>()
			                                              .WithAll<EngineOutput.ElementMoved>()
			                                              .WithEntityAccess()) {
				ecb.AddComponent<ReadyToDestroy>(entity);

				var elementEntity = entitiesMap[idRo.ValueRO.Value];
				var positionRw = positionsMap[idRo.ValueRO.Value];
				
				if (!SystemAPI.HasComponent<Moving>(elementEntity)) {
					ecb.AddComponent(elementEntity, new Moving {
						From = positionRw.ValueRO.Value,
						To = boardPositionRo.ValueRO.Value,
						Velocity = 0.1f
					});
				} else {
					var rw = SystemAPI.GetComponentRW<Moving>(elementEntity);
					rw.ValueRW.To = boardPositionRo.ValueRO.Value;
				}
				
				positionRw.ValueRW.Value = boardPositionRo.ValueRO.Value;
			}
			
			ecb.Playback(state.EntityManager);
		}
	}

}