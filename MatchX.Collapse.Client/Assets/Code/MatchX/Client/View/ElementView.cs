using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace MatchX.Client.Authoring
{

	public class ElementView : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			// TODO: Remove entity usage from here after input system will be done 
			var entityReference = GetComponent<EntityRef>();
			World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<Pointer.Event.OnTap>(entityReference.Value);
		}
	}

}