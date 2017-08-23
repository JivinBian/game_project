using UnityEngine;
using System.Collections;

namespace GameCore.Script.Interface
{
	public interface INavigatable
	{
		bool MoveTo(Vector3 pPoint);
		void StopMove();
		bool IsMoving();
		bool TestMove(Vector3 pPoint);
		Vector3 GetPosition();
		void SetPosition(Vector3 pPosition);
		float Speed { get; }
		float GetStopDistance();
		void SetRotation(float pRotaion);
		float GetRotation();
		void FaceTo(Vector3 pTargetPosition);
		float GetAcceleration();
		float GetAngularSpeed();
	}
}

