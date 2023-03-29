using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using Maker;

namespace Tool.File
{
    public class File : MonoBehaviour
    {
        [Header("Application.persistentDataPath + ~~~")]
        [SerializeField]
        private string _mapDataPath = string.Empty;

        [SerializeField]
        private string _exampleDataPath = string.Empty;

        private void Awake()
        {
            _mapDataPath = Application.persistentDataPath + @"/" + _mapDataPath + @"/";
            _exampleDataPath = Application.persistentDataPath + @"/" + _exampleDataPath + @"/";
        }

        public void WirteMapFile(string selectMapName, List<Chunk> chunks)
        {
            CSV_Map_writer(selectMapName, chunks);
        }

        public void ReadMapFile(string selectMapName)
        {
            CSV_Map_Reader(selectMapName);
        }

        public bool Map_FileChack(string fileName)
        {
            fileName += fileName + ".dat";
            return System.IO.File.Exists(_mapDataPath + fileName);
        }

        private void CSV_Map_writer(string fileName, List<Chunk> chunks)
        {
            fileName += fileName + ".dat";
            StartCoroutine(CSV_Map_writerCo(fileName, chunks));
        }

        private void CSV_Map_Reader(string fileName)
        {
            fileName += fileName + ".dat";
            StartCoroutine(CSV_Map_ReaderCo(fileName));
        }

        private IEnumerator CSV_Map_writerCo(string fileName, List<Chunk> chunks)
        {
            //use Json

            yield return null;
        }

        private IEnumerator CSV_Map_ReaderCo(string fileName)
        {
            //use Json

            yield return null;
        }
    }

    [System.Serializable]
    public class ChunkData
    {
        public int number = 0;

        public float x = 0;

        public float z = 0;

        public int upChunkNumber = 0;

        public int downChunkNumber = 0;

        public int rightChunkNumber = 0;

        public int leftChunkNumber = 0;

        public List<Node> activeNodes = new List<Node>();
    }

    [System.Serializable]
    public class NodeData
    {
        public int number = 0;

        public float x = 0;

        public float z = 0;

        public int upNodeNumber = 0;

        public int downNodeNumber = 0;

        public int rightNodeNumber = 0;

        public int leftNodeNumber = 0;

        public bool isWalkable = default;
    }
}