using System;
using UnityEngine;
using Views;

public static class AREvents
{
	public static Action FurniturePlaced;
	public static Action<Vector3, TouchPhase> TouchMove;
	public static Action<float> TouchRotate;
	public static Action<TrackAndPlaceEvents> TrackAndPlace;
	public static Action<GameObject> ARFurnitureTapped;
	public static Action EditFurniturePosition;
}