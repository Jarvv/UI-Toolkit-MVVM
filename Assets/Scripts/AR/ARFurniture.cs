using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ARFurniture : MonoBehaviour
{
	public void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject() && !EventSystem.current.currentSelectedGameObject)
		{
			AREvents.ARFurnitureTapped(gameObject);
		}
	}
}
