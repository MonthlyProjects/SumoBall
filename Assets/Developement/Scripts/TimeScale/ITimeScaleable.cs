using System;
using UnityEngine;

public interface ITimeScaleable
{
	public float Priority { get; }
	public float TimeScale { get; }

	public GameObject OwnerObject { get; }

	public System.Action<float> OnChangeTimeScale { get; set; }

	public void AddTimeScale();
	public void RemoveTimeScale();

}