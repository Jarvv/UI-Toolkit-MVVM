using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AR
{
	public class ARFurniture : MonoBehaviour
	{
		public void OnMouseDown()
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				AREvents.ARFurnitureTapped(transform.parent.gameObject);
			}
		}
	}
}

