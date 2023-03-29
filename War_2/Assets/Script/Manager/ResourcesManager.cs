using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Manager
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private string _buildingPrefabPath = string.Empty;

        [SerializeField]
        private string _unitPrefabPath = string.Empty;

        [SerializeField]
        private string _playerUnitPrefabPath = string.Empty;

        private Building[] _buildings;

        private Unit[] _unit;

        public List<Building> BuildingLoad()
        {
            _buildings = Resources.LoadAll<Building>(_buildingPrefabPath);
            return _buildings.ToList();
        }

        public List<Unit> UnitLoad()
        {
            _unit = Resources.LoadAll<Unit>(_unitPrefabPath);
            return _unit.ToList();
        }

        public List<Unit> CharacterLoad()
        {
            _unit = Resources.LoadAll<Unit>(_playerUnitPrefabPath);
            return _unit.ToList();
        }
    }
}
