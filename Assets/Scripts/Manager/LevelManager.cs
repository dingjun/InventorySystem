using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class LevelManager : MonoBehaviour
	{
		private const int NUMBER_GROUND_LINES = 24;
		private const int NUMBER_ROCKS = 3;
		private const int DISTANCE_DROPPED_ITEM = 1;

		public Transform TileParent;
		public Transform ItemParent;
		public GameObject WallTile;
		public GameObject GroundTile;
		public GameObject RockTile;
		public GameObject ItemPrefab;
		public ItemDatabase ItemDB;

		private float _groundCoordinate;

		private Vector3 RandomCoordinates
		{
			get
			{
				float x = Random.value * _groundCoordinate * 2 - _groundCoordinate;
				float y = Random.value * _groundCoordinate * 2 - _groundCoordinate;
				return new Vector3(x, y, 0);
			}
		}

		// Use this for initialization
		void Start()
		{
			float tileSize = GroundTile.GetComponent<Renderer>().bounds.size.x;
			float groundSize = tileSize * NUMBER_GROUND_LINES;
			float wallCoordinate = (groundSize + tileSize) / 2;
			_groundCoordinate = wallCoordinate - tileSize;

			// wall
			for (int i = 0; i < NUMBER_GROUND_LINES; ++i)
			{
				float coordinate = i * tileSize - _groundCoordinate;
				Instantiate(WallTile, new Vector3(-wallCoordinate, coordinate, 0), Quaternion.identity, TileParent);
				Instantiate(WallTile, new Vector3(wallCoordinate, coordinate, 0), Quaternion.identity, TileParent);
				Instantiate(WallTile, new Vector3(coordinate, -wallCoordinate, 0), Quaternion.identity, TileParent);
				Instantiate(WallTile, new Vector3(coordinate, wallCoordinate, 0), Quaternion.identity, TileParent);
			}

			// ground
			for (int i = 0; i < NUMBER_GROUND_LINES; ++i)
			{
				for (int j = 0; j < NUMBER_GROUND_LINES; ++j)
				{
					Instantiate(GroundTile, new Vector3(i * tileSize - _groundCoordinate, j * tileSize - _groundCoordinate, 0), Quaternion.identity, TileParent);
				}
			}

			// rock
			for (int i = 0; i < NUMBER_ROCKS; ++i)
			{
				Instantiate(RockTile, RandomCoordinates, Quaternion.identity, TileParent);
			}

			// item
			SpawnNewItems(null);
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.SPAWN_NEW_ITEMS, SpawnNewItems);
			EventManager.StartListening(EventName.SPAWN_ITEM_NEAR_PLAYER, SpawnItemNextPlayer);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.SPAWN_NEW_ITEMS, SpawnNewItems);
			EventManager.StopListening(EventName.SPAWN_ITEM_NEAR_PLAYER, SpawnItemNextPlayer);
		}

		public void SpawnNewItems(object[] eventParams)
		{
			for (int i = 0; i < ItemDB.Count; ++i)
			{
				GameObject itemObject = Instantiate(ItemPrefab, RandomCoordinates, Quaternion.identity, ItemParent);
				itemObject.GetComponent<ItemObject>().Item = ItemDB.GetItem(i);
			}
		}

		public void SpawnItemNextPlayer(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 2 && eventParams[0] is Vector3 && eventParams[1] is Item);
			Vector3 playerPosition = (Vector3)eventParams[0];
			Item item = (Item)eventParams[1];
			IStackable stackable = item as IStackable;

			int itemCount = (stackable == null) ? 1 : stackable.Count;
			for (int i = 0; i < itemCount; ++i)
			{
				Vector3 itemPosition = playerPosition + (Vector3)Random.insideUnitCircle.normalized * DISTANCE_DROPPED_ITEM;
				GameObject itemObject = Instantiate(ItemPrefab, itemPosition, Quaternion.identity, ItemParent);
				itemObject.GetComponent<ItemObject>().Item = ItemDB.GetItem(item.Name);
			}
		}
	}
}
