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
			FurnitureControllerEvents.GetFurnitureItem += GetFurnitureItem;
		}

	   	private void OnDisable()
		{
			FurnitureControllerEvents.LoadFurnitureData -= LoadFurnitureData;
			FurnitureControllerEvents.GetFurnitureItem -= GetFurnitureItem;
		}
		
		private void LoadFurnitureData(Action<FurnitureSO[]> callback)
		{
			callback.Invoke(_furnitureRepository.GetAllFurnitureItems());
		}
		
		private void GetFurnitureItem(string id, Action<FurnitureSO> callback)
		{
			callback.Invoke(_furnitureRepository.GetFurnitureItemById(id));
		}
	}
}
