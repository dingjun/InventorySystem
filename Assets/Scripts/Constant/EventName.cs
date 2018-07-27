﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public static class EventName
	{
		public const string TOGGLE_INVENTORY_SCREEN		= "TOGGLE_INVENTORY_SCREEN";
		public const string TOGGLE_EQUIPMENT_SCREEN		= "TOGGLE_EQUIPMENT_SCREEN";
		public const string TOGGLE_ATTRIBUTE_SCREEN		= "TOGGLE_ATTRIBUTE_SCREEN";
		public const string SPAWN_NEW_ITEMS				= "SPAWN_NEW_ITEMS";
		public const string SET_PICKUP_OPTION			= "SET_PICKUP_OPTION";

		public const string UPDATE_INVENTORY			= "UPDATE_INVENTORY";
		public const string UPDATE_EQUIPMENT			= "UPDATE_EQUIPMENT";
		public const string UPDATE_ATTRIBUTE			= "UPDATE_ATTRIBUTE";
		public const string UPDATE_AIR_ITEM				= "UPDATE_AIR_ITEM";

		public const string CLICK_ITEM_OBJECT			= "CLICK_ITEM_OBJECT";
		public const string LEFT_CLICK_ITEM_ICON		= "LEFT_CLICK_ITEM_ICON";
		public const string RIGHT_CLICK_ITEM_ICON		= "RIGHT_CLICK_ITEM_ICON";
		public const string MIDDLE_CLICK_ITEM_ICON		= "MIDDLE_CLICK_ITEM_ICON";

		public const string OPEN_TOOLTIP				= "OPEN_TOOLTIP";
		public const string CLOSE_TOOLTIP				= "CLOSE_TOOLTIP";
	}
}
