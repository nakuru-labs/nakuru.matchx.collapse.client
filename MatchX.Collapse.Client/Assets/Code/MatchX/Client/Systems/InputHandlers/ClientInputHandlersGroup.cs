using Unity.Entities;

namespace MatchX.Client.InputHandlers
{

	public partial class ClientInputHandlersGroup : ComponentSystemGroup
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			AddSystemToUpdateList(World.CreateSystem<OnElementTapInputSystem>());
		}
	}

}