using MatchX.Client.InputHandlers;
using Nakuru.Entities.Hybrid.Presentation;
using Nakuru.Unity.Ecs.Utilities;

namespace MatchX.Client
{

	public partial class ClientPresentationSystemsGroup : StrictOrderSystemsGroup
	{
		protected override void OnCreate()
		{
			base.OnCreate();

			AddSystemToUpdateList(World.CreateSystem<ClientInputHandlersGroup>());
			AddSystemToUpdateList(World.CreateSystem<HybridPresentationSystemGroup>());
		}
	}

}