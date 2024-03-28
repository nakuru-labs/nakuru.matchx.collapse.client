using Unity.Entities;
using Unity.Mathematics;

namespace MatchX.Client
{

	public struct Moving : IComponentData
	{
		public int2 From;
		public int2 To;
		public float Velocity;
	}

}