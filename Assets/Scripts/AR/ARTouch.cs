using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AR
{
	public class ARTouch : MonoBehaviour
	{
		private const float PINCH_TURN_RATIO = Mathf.PI / 2;

		private void Update()
		{
			if (Input.touchCount == 1)
			{
				int id = Input.touches[0].fingerId;

				if (!EventSystem.current.IsPointerOverGameObject(id) && !EventSystem.current.currentSelectedGameObject)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
					{
						AREvents.TouchMove(Input.touches[0].position, TouchPhase.Stationary);
					}
					else if (Input.GetTouch(0).phase == TouchPhase.Began)
					{
						AREvents.TouchMove(Input.touches[0].position, TouchPhase.Began);
					}
					else if (Input.GetTouch(0).phase == TouchPhase.Ended)
					{
						AREvents.TouchMove(Input.touches[0].position, TouchPhase.Ended);
					}
				}
			}
			else if (Input.touchCount > 1)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
				{
					Vector2 touch0 = Input.GetTouch(0).position;
					Vector2 touch1 = Input.GetTouch(1).position;

					float turnAngle = Angle(touch0, touch1);
					float prevTurn = Angle(touch0 - Input.GetTouch(0).deltaPosition, touch1 - Input.GetTouch(1).deltaPosition);
					float turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

					if (Mathf.Abs(turnAngleDelta) > 0)
					{
						turnAngleDelta *= PINCH_TURN_RATIO;
						AREvents.TouchRotate(turnAngleDelta);
					}
				}
			}
		}

		private float Angle(Vector2 pos1, Vector2 pos2)
		{
			Vector2 from = pos2 - pos1;
			Vector2 to = new Vector2(1, 0);

			float result = Vector2.Angle(from, to);
			Vector3 cross = Vector3.Cross(from, to);

			if (cross.z > 0)
			{
				result = 360f - result;
			}

			return result;
		}
	}
}
