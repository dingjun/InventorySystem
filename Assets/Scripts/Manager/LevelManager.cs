using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class LevelManager : MonoBehaviour
	{
		private const int NUMBER_GROUND_LINES = 24;
		private const int NUMBER_ROCKS = 3;
		private const int NUMBER_SPAWNING_ITEMS = 18;

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
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.SPAWN_NEW_ITEMS, SpawnNewItems);
		}

		public void SpawnNewItems(object[] eventParams)
		{
			for (int i = 0; i < NUMBER_SPAWNING_ITEMS; ++i)
			{
				GameObject itemObject = Instantiate(ItemPrefab, RandomCoordinates, Quaternion.identity, ItemParent);
				Item item = ItemDB.AttachItemComponent(itemObject, i);
				itemObject.GetComponent<SpriteRenderer>().sprite = item.Icon;
			}
		}
	}
}
