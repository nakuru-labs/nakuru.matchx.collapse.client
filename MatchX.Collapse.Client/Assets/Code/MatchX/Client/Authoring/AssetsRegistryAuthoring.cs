using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MatchX.Client.Authoring
{

	public class AssetsRegistry : IComponentData
	{
		public List<GameObject> Value = new();
	}

	public class AssetsRegistryAuthoring : MonoBehaviour
	{
		public List<GameObject> Prefabs;
		
		private class Bakers : Baker<AssetsRegistryAuthoring>
		{
			public override void Bake(AssetsRegistryAuthoring authoring)
			{
				var entity = GetEntity(TransformUsageFlags.Dynamic);
			
				AddComponentObject(entity, new AssetsRegistry
				{
					Value = authoring.Prefabs
				});
			}
		}
	}
	
}