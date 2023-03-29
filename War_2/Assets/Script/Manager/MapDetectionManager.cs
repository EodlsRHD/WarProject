using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

using Maker;

namespace Manager
{
    public class MapDetectionManager : MonoBehaviour
    {
        private static Terrain _newMap = null;

        [SerializeField]
        private Transform _worldCoordinate = null;

        private Transform _spawnPoint = null;

        private static List<Chunk> _activeChunks = new List<Chunk>();

        private static List<Building> _buildingss = new List<Building>();

        private static List<Unit> _units = new List<Unit>();

        private static eCharacterType _characterType = eCharacterType.Non;
        public static void Initialize(Terrain map, NavMeshData navMeshData, eCharacterType characterType, List<Building> buildings, List<Unit> units, List<Chunk> activeChunks)
        {
            _newMap = map;
            _characterType = characterType;
            _activeChunks = activeChunks;
            _buildingss = buildings;
            _units = units;

            NavMeshSurface surface = _newMap.gameObject.GetComponentInChildren<NavMeshSurface>();
            surface.navMeshData = navMeshData;
        }

        private void Start()
        {
            PlayerSpawn();
            StartCoroutine(MakeWorld());
        }

        private void PlayerSpawn()
        {
            Transform _spawnPoint = _newMap.transform.Find("Character_SP").transform;

            foreach(var unit in _units)
            {
                if(unit.gameObject.CompareTag("Player") && unit.gameObject.name.Contains(_characterType.ToString()))
                {
                    GameManager.Instance.instantiateFactory.ObjectInstantiate<Unit>(unit.gameObject, _worldCoordinate, _spawnPoint.position);
                    break;
                }
            }

            SearchRallyPoint("Corps_Red_SP", eObjColor.red);
            SearchRallyPoint("Corps_Blue_SP", eObjColor.blue);
        }

        private void SearchRallyPoint(string name, eObjColor color)
        {
            Transform rallypoint = _newMap.transform.Find(name).transform;

            Corps corps = new Corps();
            corps.objColor = color;
            corps.rallyPoint = rallypoint;

            CorpsManager.Instance.SetRallyPoint(corps);
        }

        private IEnumerator MakeWorld()
        {
            IngameUIManager.Instance.SetLoadingImageActive(true);

            GameManager.Instance.instantiateFactory.ObjectInstantiate(_newMap, _worldCoordinate);
            yield return null;


            Building[] building = FindObjectsOfType<Building>();
            List<Building> buildings = building.ToList();
            yield return null;

            buildings.ForEach((e) =>
            {
                e.Initialize();
            });

            yield return null;


            Unit[] unit = FindObjectsOfType<Unit>();
            List<Unit> units = unit.ToList();
            yield return null;

            units.ForEach((e) =>
            {
                e.Initialize();
            });

            yield return null;

            IngameUIManager.Instance.SetLoadingImageActive(false);

            yield return new WaitForSeconds(5f);

            buildings.ForEach((e) =>
            {
                e.StartProuctionUnit();
            });
        }

        float _chunkSize = 10f;
        float _nodeSize = 1f;

        private void OnDrawGizmos()
        {
            foreach (var chunk in _activeChunks)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(chunk.position, new Vector3(_chunkSize, 2f, _chunkSize));

                foreach (var node in chunk.activeNodes)
                {
                    //Gizmos.color = Color.black;
                    //Gizmos.DrawWireCube(node.position, new Vector3(_nodeSize, 1f, _nodeSize));

                    if (node.isWalkable == false)
                    {
                        Gizmos.color = Color.red;;
                        Gizmos.DrawWireCube(node.position, new Vector3(_nodeSize, 0.1f, _nodeSize));
                    }
                    else if (node.isWalkable == true)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawWireCube(node.position, new Vector3(_nodeSize, 0.1f, _nodeSize));
                    }
                }
            }
        }
    }
}
