using Nakuru.Entities.Hybrid.Presentation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace MatchX.Client
{

	[RequireMatchingQueriesForUpdate]
	public partial struct ClientGravitySystem : ISystem
	{
		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			const float engineDependentDeltaTime = SimulationConstants.ClientTargetDeltaTime * SimulationConstants.EngineTargetFrameRate;
			
			foreach (var (localTransformRo, movingRw, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Moving>>()
			                                                     .WithAll<ViewElement.Tag>()
			                                                     .WithEntityAccess()) {
				var movingData = movingRw.ValueRO;
				var velocity = movingRw.ValueRO.Velocity;
				var moveSpeed = engineDependentDeltaTime * velocity;
				// var moveSpeed = engineDependentDeltaTime;
				var currPosition = localTransformRo.ValueRO.Position.xy;
				var currDirection = movingData.To - currPosition;
				var currDirectionNormalized = math.normalize(currDirection);

				var nextPosition = currPosition + currDirectionNormalized * moveSpeed;
				var nextDirection = movingData.To - nextPosition;

				localTransformRo.ValueRW.Position = new float3(nextPosition.xy, 0f);
				
				movingRw.ValueRW.Velocity += SimulationConstants.ClientTargetDeltaTime;
				movingRw.ValueRW.Velocity = math.clamp(movingRw.ValueRW.Velocity, 0f, 1.1f);
				
				var dot = math.dot(currDirection, nextDirection);

				if (dot <= 0) {
					ecb.RemoveComponent<Moving>(entity);
					var nextPositionClamped = math.clamp(nextPosition, movingData.To, movingData.From);
					localTransformRo.ValueRW.Position = new float3(nextPositionClamped.xy, 0f);
				}
			}

			ecb.Playback(state.EntityManager);
		}
	}

}