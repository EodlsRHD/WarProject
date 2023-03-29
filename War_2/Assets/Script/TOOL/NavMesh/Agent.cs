using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Maker;

namespace Tool.NavAgent
{
    public class Agent : MonoBehaviour
    {
        public void IsWalkable(Node node, int nodeSize)
        {
            if (Physics.Raycast(node.position, Vector3.down, out RaycastHit rayHit, Mathf.Infinity))
            {
                if(NavMesh.SamplePosition(rayHit.point, out NavMeshHit navHit, nodeSize * 0.5f, NavMesh.AllAreas))
                {
                    node.isWalkable = true;
                }

                //node.SetHeight(rayHit.point.y);
            }
        }
    }

}