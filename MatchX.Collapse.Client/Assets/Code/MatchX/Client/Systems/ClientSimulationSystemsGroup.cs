using Nakuru.Unity.Ecs.Utilities;
using Unity.Entities;

namespace MatchX.Client
{

	public partial class ClientSimulationSystemsGroup : StrictOrderSystemsGroup
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			
			AddSystemToUpdateList(World.CreateSystem<EngineOutputHandlersGroup>());
			AddSystemToUpdateList(World.CreateSystem<ClientGravitySystem>());
		}
	}

}