using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class LevelManager : MonoBehaviour
	{
		private const int NUMBER_GROUND_LINE = 24;
		private const int NUMBER_ROCK = 3;

		public GameObject WallTile;
		public GameObject GroundTile;
		public GameObject RockTile;

		// Use this for initialization
		void Start()
		{
			float tileSize = GroundTile.GetComponent<Renderer>().bounds.size.x;
			float groundSize = tileSize * NUMBER_GROUND_LINE;
			float wallCoordinate = (groundSize + tileSize) / 2;
			float groundCoordinate = wallCoordinate - tileSize;

			// wall
			for (int i = 0; i < NUMBER_GROUND_LINE; ++i)
			{
				float coordinate = i * tileSize - groundCoordinate;
				Instantiate(WallTile, new Vector3(-wallCoordinate, coordinate, 0), Quaternion.identity, transform);
				Instantiate(WallTile, new Vector3(wallCoordinate, coordinate, 0), Quaternion.identity, transform);
				Instantiate(WallTile, new Vector3(coordinate, -wallCoordinate, 0), Quaternion.identity, transform);
				Instantiate(WallTile, new Vector3(coordinate, wallCoordinate, 0), Quaternion.identity, transform);
			}

			// ground
			for (int i = 0; i < NUMBER_GROUND_LINE; ++i)
			{
				for (int j = 0; j < NUMBER_GROUND_LINE; ++j)
				{
					Instantiate(GroundTile, new Vector3(i * tileSize - groundCoordinate, j * tileSize - groundCoordinate, 0), Quaternion.identity, transform);
				}
			}

			// rock
			for (int i = 0; i < NUMBER_ROCK; ++i)
			{
				float x = Random.value * groundCoordinate * 2 - groundCoordinate;
				float y = Random.value * groundCoordinate * 2 - groundCoordinate;
				Instantiate(RockTile, new Vector3(x, y, 0), Quaternion.identity, transform);
			}
		}
	}
}
