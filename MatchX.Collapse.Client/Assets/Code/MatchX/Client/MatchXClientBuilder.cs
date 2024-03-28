using MatchX.Engine;
using Nakuru.Unity.Ecs.Utilities;
using Unity.Entities;

namespace MatchX.Client
{

	public static class MatchXClientBuilder
	{
		public static World Build(EngineIo io)
		{
			var world = WorldBuilder.NewWorld("Client World", WorldFlags.Game, true)
			                     .WithAllUnityDefaultSystems()
			                     .WithInitializePhaseManagedSystem<ClientInitializeSystemsGroup>()
			                     .WithSimulationPhaseManagedSystem<ClientSimulationSystemsGroup>()
			                     .WithPresentationPhaseManagedSystem<ClientPresentationSystemsGroup>()
			                     .Build(true);
			
			var engineIoEntity = world.EntityManager.CreateEntity();
			world.EntityManager.SetName(engineIoEntity, "Engine IO");
			world.EntityManager.AddComponentData(engineIoEntity, io);
			
			return world;
		}
	}

}