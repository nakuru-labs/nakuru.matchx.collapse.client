using MatchX.Client;
using MatchX.Engine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace MatchX.Client
{

	public class MatchXBootstrap : ICustomBootstrap
	{
		public bool Initialize(string defaultWorldName)
		{
			Application.targetFrameRate = SimulationConstants.ClientTargetFrameRate;

			// build engine world
			var engineIo = MatchXEngineBuilder.Build(true);
			
			// setup engine tick rate
			var engineSimulationGroup = engineIo.EntityManager.World.GetExistingSystemManaged<EngineSimulationSystemsGroup>();
			engineSimulationGroup.RateManager = new RateUtils.FixedRateCatchUpManager(SimulationConstants.EngineTargetDeltaTime);

			// build client world with engine io
			var clientWorld = MatchXClientBuilder.Build(engineIo);
			
			// inject output gateway where engine will send output events
			engineIo.InjectOutputGateway(clientWorld.EntityManager);

			// io example
			engineIo.CreateBoard(10, 10, new int2(0, -1));
			engineIo.CreateElement(new int2(0, 9));
			engineIo.CreateElement(new int2(1, 9));
			engineIo.CreateElement(new int2(2, 9));
			engineIo.CreateElement(new int2(3, 9));
			engineIo.CreateElement(new int2(4, 9));
			engineIo.CreateElement(new int2(0, 8));
			engineIo.CreateElement(new int2(1, 8));
			engineIo.CreateElement(new int2(2, 8));
			engineIo.CreateElement(new int2(3, 8));
			engineIo.CreateElement(new int2(4, 8));
			engineIo.CreateElement(new int2(0, 7));
			engineIo.CreateElement(new int2(1, 7));
			engineIo.CreateElement(new int2(2, 7));
			engineIo.CreateElement(new int2(3, 7));
			engineIo.CreateElement(new int2(4, 7));
			engineIo.CreateElement(new int2(0, 6));
			engineIo.CreateElement(new int2(1, 6));
			engineIo.CreateElement(new int2(2, 6));
			engineIo.CreateElement(new int2(3, 6));
			engineIo.CreateElement(new int2(4, 6));
			engineIo.CreateElement(new int2(4, 9));
			engineIo.CreateElement(new int2(0, 5));
			engineIo.CreateElement(new int2(0, 4));
			engineIo.CreateElement(new int2(0, 3));
			engineIo.CreateElement(new int2(0, 2));
			engineIo.CreateElement(new int2(0, 1));
			engineIo.CreateElement(new int2(0, 0));
			engineIo.CreateElement(new int2(8, 8));
			engineIo.CreateElement(new int2(8, 7));
			engineIo.CreateElement(new int2(8, 5));
			engineIo.CreateElement(new int2(7, 5));
			engineIo.CreateElement(new int2(7, 9));
			// EngineIo.DestroyBoard();

			return true;
		}
	}

}