using MatchX.Client.InputHandlers;
using MatchX.Common.Systems;
using Nakuru.Unity.Ecs.Utilities;
using WorldExtensions = Unity.Entities.WorldExtensions;

namespace MatchX.Client
{

	public partial class ClientInitializeSystemsGroup : StrictOrderSystemsGroup
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			
			AddSystemToUpdateList(WorldExtensions.CreateSystem<DestroyEntitiesSystem>(World));
			AddSystemToUpdateList(WorldExtensions.CreateSystem<ClearInputEventSystem>(World));
		}
	}

}