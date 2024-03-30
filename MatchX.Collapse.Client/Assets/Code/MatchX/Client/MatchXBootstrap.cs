using MatchX.Engine;
using Unity.Collections;
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

			var elementShape = new NativeArray<uint2>(1, Allocator.Temp);
			elementShape[0] = uint2.zero;
			
			var elementShape2 = new NativeArray<uint2>(2, Allocator.Temp);
			elementShape2[0] = uint2.zero;
			elementShape2[1] = new uint2(0, 1);
			
			var elementShape2x2 = new NativeArray<uint2>(4, Allocator.Temp);
			elementShape2x2[0] = uint2.zero;
			elementShape2x2[1] = new uint2(0, 1);
			elementShape2x2[2] = new uint2(1, 0);
			elementShape2x2[3] = new uint2(1, 1);

			// io example
			engineIo.CreateBoard(10, 10, new int2(0, -1));
			engineIo.CreateElement(new int2(0, 9), elementShape);
			engineIo.CreateElement(new int2(1, 9), elementShape);
			engineIo.CreateElement(new int2(0, 8), elementShape);
			engineIo.CreateElement(new int2(1, 8), elementShape);
			engineIo.CreateElement(new int2(0, 7), elementShape);
			engineIo.CreateElement(new int2(1, 7), elementShape);
			engineIo.CreateElement(new int2(2, 7), elementShape);
			engineIo.CreateElement(new int2(3, 7), elementShape);
			engineIo.CreateElement(new int2(4, 6), elementShape);
			engineIo.CreateElement(new int2(0, 6), elementShape);
			engineIo.CreateElement(new int2(1, 6), elementShape);
			engineIo.CreateElement(new int2(2, 6), elementShape);
			engineIo.CreateElement(new int2(3, 6), elementShape);
			engineIo.CreateElement(new int2(4, 5), elementShape);
			engineIo.CreateElement(new int2(2, 8), elementShape2x2);
			engineIo.CreateElement(new int2(0, 5), elementShape);
			engineIo.CreateElement(new int2(0, 4), elementShape);
			engineIo.CreateElement(new int2(0, 3), elementShape);
			engineIo.CreateElement(new int2(0, 2), elementShape);
			engineIo.CreateElement(new int2(0, 1), elementShape);
			engineIo.CreateElement(new int2(0, 0), elementShape);
			engineIo.CreateElement(new int2(5, 4), elementShape2x2);
			engineIo.CreateElement(new int2(5, 9), elementShape);
			engineIo.CreateElement(new int2(5, 7), elementShape);
			engineIo.CreateElement(new int2(6, 9), elementShape);
			engineIo.CreateElement(new int2(6, 8), elementShape);
			engineIo.CreateElement(new int2(6, 7), elementShape);
			engineIo.CreateElement(new int2(7, 9), elementShape);
			engineIo.CreateElement(new int2(7, 8), elementShape);
			engineIo.CreateElement(new int2(8, 5), elementShape);
			engineIo.CreateElement(new int2(8, 9), elementShape);
			// EngineIo.DestroyBoard();

			return true;
		}
	}

}