using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Maker;

namespace Manager
{
    public class MapManager : MonoBehaviour
    {
        [Header("Maker")]
        [SerializeField]
        private GridMaker _gridMaker = null;

        [Header("Manager")]
        [SerializeField]
        private ResourcesManager _resourcesManager = null;

        [SerializeField]
        private SceneChanger _sceneChanger = null;

        [Header("Object")]
        [SerializeField]
        List<Terrain> _testMaps = new List<Terrain>();

        private int _nodeSize = 0;

        private int _chunkSize = 0;

        private Terrain _newMap = null;

        private string _selectMapName = string.Empty;

        private eCharacterType _characterType = eCharacterType.Non;

        private NavMeshData _navMeshData = null;

        private List<Building> _buildings;

        private List<Unit> _units;

        public void Initialize(string mapName, eCharacterType characterType)
        {
            _selectMapName = mapName;
            _characterType = characterType;
            _nodeSize = GameManager.Instance.nodeSize;
            _chunkSize = GameManager.Instance.chunkSize;
        }

        public void SelectMap()
        {
            foreach(var map in _testMaps)
            {
                if(map.gameObject.name.Contains(_selectMapName))
                {
                    _newMap = map;
                    break;
                }
            }

            //if (_file.Map_FileChack(_selectMapName))
            //{
            //    _gridMaker.Initialize((int)newTerrain.terrainData.size.x, (int)newTerrain.terrainData.size.z, _chunkSize, _nodeSize);

            //    NavMeshSurface[] surfaces = newTerrain.gameObject.GetComponentsInChildren<NavMeshSurface>();
            //    foreach (var surface in surfaces)
            //    {
            //        surface.RemoveData();
            //        surface.BuildNavMesh();
            //    }

            //    _gridMaker.MakeGrid(WirteMapFile);
            //}

            Terrain terrain = Instantiate(_newMap);
            _gridMaker.Initialize((int)terrain.terrainData.size.x, (int)terrain.terrainData.size.z, _chunkSize, _nodeSize, Done);

            NavMeshSurface surface = terrain.gameObject.GetComponentInChildren<NavMeshSurface>();
            if (surface.navMeshData == null)
            {
                surface.RemoveData();
                surface.BuildNavMesh();
            }

            _navMeshData = surface.navMeshData;

            _sceneChanger.mapMakeIsDone = 0.1f;

            InstantiateObject();

            _sceneChanger.mapMakeIsDone = 0.8f;

            _gridMaker.MakeGrid(_selectMapName);
        }

        private void InstantiateObject()
        {
            _buildings = new List<Building>();
            _units = new List<Unit>();

            _sceneChanger.mapMakeIsDone = 0.7f;

            _buildings = _resourcesManager.BuildingLoad();
            _units = _resourcesManager.UnitLoad();
            _units.AddRange(_resourcesManager.CharacterLoad());

            _sceneChanger.mapMakeIsDone = 0.75f;
        }

        private void Done(List<Chunk> chunks)
        {
            _sceneChanger.mapMakeIsDone = 0.7f;

            MapDetectionManager.Initialize(_newMap, _navMeshData, _characterType, _buildings, _units, chunks);

            _sceneChanger.mapMakeIsDone = 0.9f;
        }
    }
}
