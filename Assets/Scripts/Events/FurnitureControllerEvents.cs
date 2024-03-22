using System;
using ScriptableObjects;

public static class FurnitureControllerEvents
{
	public static Action<Action<FurnitureSO[]>> LoadFurnitureData;
	public static Action<string, Action<FurnitureSO>> GetFurnitureItem;
}