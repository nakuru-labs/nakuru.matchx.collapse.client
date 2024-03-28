using Nakuru.Entities.Hybrid.Presentation;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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
				var currPosition = localTransformRo.ValueRO.Position.xy;
				var currDirection = movingData.To - currPosition;
				var currDirectionNormalized = math.normalize(currDirection);

				var nextPosition = currPosition + currDirectionNormalized * moveSpeed;
				var nextPositionClamped = math.clamp(nextPosition, movingData.To, movingData.From);
				var nextDirection = movingData.To - nextPositionClamped;

				localTransformRo.ValueRW.Position = new float3(nextPositionClamped.xy, 0f);
				
				movingRw.ValueRW.Velocity += SimulationConstants.ClientTargetDeltaTime * 2;
				movingRw.ValueRW.Velocity = math.clamp(movingRw.ValueRW.Velocity, 0f, 1.1f);
				
				var dot = math.dot(currDirection, nextDirection);
				Debug.Log($"Reached target = {dot <= 0}");
				
				Debug.Log($"vel - {movingRw.ValueRO.Velocity}");

				if (dot <= 0) {
					ecb.RemoveComponent<Moving>(entity);
				}
			}
			
			ecb.Playback(state.EntityManager);
		}
	}

}