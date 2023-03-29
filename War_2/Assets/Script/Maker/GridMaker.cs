using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Tool.File;
using Tool.NavAgent;
using Tool.Factory;
using Manager;

namespace Maker
{
    public class GridMaker : MonoBehaviour
    {
        [Header("Tool")]
        [SerializeField]
        private Agent _navAgent = null;

        private int _nodeSize = 0;

        private int _chunkSize = 0;

        private int _width = 0;

        private int _length = 0;

        private int _widthChunkCount = 0;

        private int _lengthChunkCount = 0;

        private List<Chunk> _activeChunks = new List<Chunk>();

        private Action<List<Chunk>> _onCallbackDone = null;

        private bool _done = false;

        private string _selectMapName = string.Empty;

        public List<Chunk> activeChunks
        {
            get { return _activeChunks; }
        }

        public void Initialize(int width, int length, int chunkSize, int nodeSize, Action<List<Chunk>> onCallbackDone)
        {
            _onCallbackDone = onCallbackDone;

            _nodeSize = nodeSize;
            _chunkSize = chunkSize;
            _width = width;
            _length = length;

            _widthChunkCount = (_width / _nodeSize) / _chunkSize;
            _lengthChunkCount = (_length / _nodeSize) / _chunkSize;
        }

        public void MakeGrid(string selectMapName)
        {
            _selectMapName = selectMapName;
            StartCoroutine(MakeGridCo());
        }

        private IEnumerator MakeGridCo()
        {
            int width = (_width / _nodeSize);
            int length = (_length / _nodeSize);

            Debug.Log("Make Node");
            List<Node> nodelist = new List<Node>();
            int Number = 0;
            for (int l = 0; l < length; l++)
            {
                for (int w = 0; w < width; w++)
                {
                    Node node = new Node(Number, (w * _nodeSize) + (_nodeSize * 0.5f), (l * _nodeSize) + (_nodeSize * 0.5f), width);
                    nodelist.Add(node);

                    Number++;
                }

                yield return null;
            }

            yield return null;

            Debug.Log("Make Chunk");
            Number = 0;
            for (int l = 0; l < nodelist.Count;)
            {
                for (int w = l; w < l + width;)
                {
                    Chunk chunk = new Chunk(Number, (nodelist[w].position.x - _nodeSize * 0.5f) + (_chunkSize * 0.5f), (nodelist[w].position.z - _nodeSize * 0.5f) + (_chunkSize * 0.5f), (width / _chunkSize));

                    for (int i = w; i < w + (width * _chunkSize);)
                    {
                        for (int j = i; j < i + _chunkSize; j++)
                        {
                            chunk.AddNode(nodelist[j]);
                        }

                        i += width;
                    }

                    _activeChunks.Add(chunk);

                    yield return null;
                    Number++;
                    w += _chunkSize;
                }

                l += width * _chunkSize;
            }

            yield return null;

            Debug.Log("Node Walkable Chack");
            foreach(var chunk in _activeChunks)
            {
                foreach (var node in chunk.activeNodes)
                {
                    _navAgent.IsWalkable(node, _nodeSize);
                }

                yield return null;
            }

            yield return null;

            GameManager.Instance.file.WirteMapFile(_selectMapName, _activeChunks);

            yield return null;

            GameManager.Instance.file.ReadMapFile(_selectMapName);

            yield return null;

            Debug.Log("Done");
            _done = true;

            _onCallbackDone(_activeChunks);
        }
    }

    public class Chunk
    {
        private int _number = 0;

        private Vector3 _position = Vector3.zero;

        private int[] _sideChunkNumber; // up down right left

        private List<Node> _activeNodes = new List<Node>();

        private bool _isWalkable = false;

        public int number
        {
            get { return _number; }
        }

        public Vector3 position
        {
            get { return _position; }
        }

        public List<Node> activeNodes
        {
            get { return _activeNodes; }
        }

        public bool isWalkable
        {
            get { return _isWalkable; }
            set { _isWalkable = value; }
        }

        public Chunk(int number, float x, float z, int width)
        {
            _number = number;
            _position = new Vector3(x, GameManager.Instance.topHeight, z);

            _sideChunkNumber = new int[4];

            _sideChunkNumber[0] -= width;
            _sideChunkNumber[1] += width;
            _sideChunkNumber[2] += 1;
            _sideChunkNumber[3] -= 1;
        }

        public void ChackSideChunk(List<Chunk> chunkList)
        {
            SideChack(0, chunkList, true);
            SideChack(1, chunkList, true);
            SideChack(2, chunkList, false);
            SideChack(3, chunkList, false);
        }

        private void SideChack(int chunkNum, List<Chunk> chunkList, bool isWidth)
        {
            if ((-1 < chunkList[_sideChunkNumber[chunkNum]].number && chunkList[_sideChunkNumber[chunkNum]].number < chunkList.Count))
            {
                return;
            }

            if (isWidth == true)
            {
                if (chunkList[chunkNum].position.x != _position.x)
                {
                    _sideChunkNumber[chunkNum] = -1;
                }
            }

            if (isWidth == false)
            {
                if (chunkList[chunkNum].position.z != _position.z)
                {
                    _sideChunkNumber[chunkNum] = -1;
                }
            }
        }

        public void AddNode(Node node)
        {
            _activeNodes.Add(node);
        }
    }

    public class Node
    {
        private int _number = 0;

        private Vector3 _position = Vector3.zero;

        private int[] _sideNodeNumber; // up down right left

        private bool _isWalkable = false;

        public int number
        {
            get { return _number; }
        }

        public Vector3 position
        {
            get { return _position; }
        }

        public bool isWalkable
        {
            get { return _isWalkable; }
            set { _isWalkable = value; }
        }

        public Node(int number, float x, float z, int worldWidth)
        {
            _number = number;
            _position = new Vector3(x, GameManager.Instance.topHeight, z);

            _sideNodeNumber = new int[4];

            _sideNodeNumber[0] -= worldWidth;
            _sideNodeNumber[1] += worldWidth;
            _sideNodeNumber[2] += 1;
            _sideNodeNumber[3] -= 1;
        }

        public void ChackSideNode(List<Node> nodeList)
        {
            SideChack(0, nodeList, true);
            SideChack(1, nodeList, true);
            SideChack(2, nodeList, false);
            SideChack(3, nodeList, false);
        }

        private void SideChack(int nodeNum, List<Node> nodeList, bool isWidth)
        {
            if ((-1 < nodeList[_sideNodeNumber[nodeNum]].number && nodeList[_sideNodeNumber[nodeNum]].number < nodeList.Count) == false)
            {
                return;
            }

            if (isWidth == true)
            {
                if (nodeList[nodeNum].position.x != _position.x)
                {
                    _sideNodeNumber[nodeNum] = -1;
                }
            }

            if (isWidth == false)
            {
                if (nodeList[nodeNum].position.z != _position.z)
                {
                    _sideNodeNumber[nodeNum] = -1;
                }
            }
        }

        public void SetHeight(float y)
        {
            _position.y = y;
        }
    }
}
