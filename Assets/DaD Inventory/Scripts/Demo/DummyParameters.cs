﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Demo example of items parameters calculation.
/// </summary>
public class DummyParameters : MonoBehaviour
{
	[Tooltip("Attack parameter text field")]
	public Text attackText;													// Attack parameter text field
	[Tooltip("Defense parameter text field")]
	public Text defenseText;												// Defense parameter text field

	void OnEnable()
	{
		Debug.Assert(attackText && defenseText, "Wrong settings");
		UpdateParameters();
	}

	/// <summary>
	/// Calculate the parameters from equiped items.
	/// </summary>
	private void UpdateParameters()
	{
		int attackAmount = 0;
		int defenseAmount = 0;
		foreach (StackCell stackCell in GetComponentsInChildren<StackCell>())
		{
			StackItem stackItem = stackCell.GetStackItem();
			if (stackItem != null)
			{
				// Get DummyEquipment from each equiped item
				DummyEquipment dummyEquipment = stackItem.GetComponent<DummyEquipment>();
				if (dummyEquipment != null)
				{
					attackAmount += dummyEquipment.attackAmount;
					defenseAmount += dummyEquipment.defenseAmount;
				}
			}
		}
		attackText.text = attackAmount.ToString();
		defenseText.text = defenseAmount.ToString();
	}

	/// <summary>
	/// Raises the stack group event event.
	/// </summary>
	private void OnStackGroupEvent()
	{
		UpdateParameters();
	}
}
