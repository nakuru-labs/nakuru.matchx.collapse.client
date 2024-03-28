using Unity.Entities;

namespace MatchX.Client
{

	public partial class EngineOutputHandlersGroup : ComponentSystemGroup
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			
			AddSystemToUpdateList(World.CreateSystem<OnBoardCreatedOutputSystem>());
			AddSystemToUpdateList(World.CreateSystem<OnElementCreatedOutputSystem>());
			AddSystemToUpdateList(World.CreateSystem<OnElementMovedOutputSystem>());
			AddSystemToUpdateList(World.CreateSystem<OnElementDestroyedOutputSystem>());
		}
	}

}