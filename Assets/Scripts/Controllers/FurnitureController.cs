using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
	public class FurnitureController : Controller
	{
    		private FurnitureRepository _furnitureRepository;
		
		private void Awake()
		{
			_furnitureRepository = new FurnitureRepository();
		}
		
		private void OnEnable()
		{
			FurnitureControllerEvents.LoadFurnitureData += LoadFurnitureData;
		}

	   	private void OnDisable()
		{
			FurnitureControllerEvents.LoadFurnitureData -= LoadFurnitureData;
		}
		
		private void LoadFurnitureData(Action<FurnitureSO[]> action)
		{
			action?.Invoke(_furnitureRepository.GetAllFurnitureItems());
		}
	}
}
