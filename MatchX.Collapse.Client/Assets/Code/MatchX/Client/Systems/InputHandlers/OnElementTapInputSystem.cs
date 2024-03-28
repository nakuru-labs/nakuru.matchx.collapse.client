using MatchX.Engine;
using Unity.Burst;
using Unity.Entities;

namespace MatchX.Client.InputHandlers
{
	public partial struct OnElementTapInputSystem : ISystem
	{
		[BurstCompile]
		public void OnCreate(ref SystemState state)
		{
			var inputQuery = SystemAPI.QueryBuilder()
			                     .WithAll<Element.Id, Element.Tag, Pointer.Event.OnTap>()
			                     .Build();
			
			state.RequireForUpdate(inputQuery);
			state.RequireForUpdate<EngineIo>();
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			var engineIo = SystemAPI.GetSingleton<EngineIo>();
			
			foreach (var elementIdRo in SystemAPI.Query<RefRO<Element.Id>>()
			                                    .WithAll<Element.Tag, Pointer.Event.OnTap>()) {
				engineIo.TriggerElement(elementIdRo.ValueRO.Value);
			}
		}
	}

}