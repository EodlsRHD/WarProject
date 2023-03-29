using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ColorManager : MonoBehaviour
    {
        private static ColorManager _instance;

        public static ColorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ColorManager();
                }

                return _instance;
            }
        }

        [SerializeField]
        private List<Material> _buildingColorMaterials = new List<Material>();

        [SerializeField]
        private List<Material> _unitolorMaterials = new List<Material>();

        public void Awake()
        {
            _instance = this;
        }

        public Material Change_BuildingColor(eObjColor objColor)
        {
            eObjColor color = eObjColor.Non;

            switch (objColor)
            {
                case eObjColor.red:
                    color = eObjColor.red;
                    break;

                case eObjColor.blue:
                    color = eObjColor.blue;
                    break;
            }

            return Finder(color.ToString(), _buildingColorMaterials);
        }

        public Material Change_UnitColor(eObjColor objColor)
        {
            eObjColor color = eObjColor.Non;

            switch (objColor)
            {
                case eObjColor.red:
                    color = eObjColor.red;
                    break;

                case eObjColor.blue:
                    color = eObjColor.blue;
                    break;
            }

            return Finder(color.ToString(), _unitolorMaterials);
        }

        private Material Finder(string colorName, List<Material> materials)
        {
            foreach (var material in materials)
            {
                if (material.name.Contains(colorName))
                {
                    return material;
                }
            }

            return null;
        }
    }
}
