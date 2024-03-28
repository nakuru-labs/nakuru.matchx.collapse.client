using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MatchX.Client.InputHandlers
{

	[RequireMatchingQueriesForUpdate]
	public partial struct ClearInputEventSystem : ISystem
	{
		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Allocator.Temp);
			
			RemoveEventOnTap(ref state, ecb);
			
			ecb.Playback(state.EntityManager);
		}

		private void RemoveEventOnTap(ref SystemState state, EntityCommandBuffer ecb)
		{
			foreach (var (_, entity) in SystemAPI.Query<RefRO<Pointer.Event.OnTap>>()
			                                     .WithEntityAccess()) {
				ecb.RemoveComponent<Pointer.Event.OnTap>(entity);
			}
		}
	}

}