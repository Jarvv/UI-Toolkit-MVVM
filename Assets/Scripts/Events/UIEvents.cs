using System;
using Views.UI.States;

public static class UIEvents
{
	public static Action Enter;
	public static Action<string> CatalogueItemOpened;
	public static Action<AppViewStates> NavButtonTapped;
	public static Action FurnitureBackButtonTapped;
	public static Action<bool> ARActive;
	public static Action PlacementConfirm;
	public static Action<string> SelectFurniture;
	public static Action FurnitureSelectItemActive;
}