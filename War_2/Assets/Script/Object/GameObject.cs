using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObj
{
    public class GameObject : MonoBehaviour
    {
        [SerializeField]
        protected ObjColor _objColor = null;

        public ObjColor objColor
        {
            get { return _objColor; }
        }

        protected void InitializeBuildingColor()
        {
            _objColor.InitializeBuilding();
        }

        protected void InitializeUnitColor()
        {
            _objColor.InitializeUnit();
        }
    }
}
