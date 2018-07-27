using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class MouseFollower : MonoBehaviour
	{
		// Update is called once per frame
		public void Update()
		{
			transform.position = Input.mousePosition;
		}
	}
}
