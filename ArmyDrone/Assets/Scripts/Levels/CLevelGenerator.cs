using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CLevelGenerator : MonoBehaviour
    {
        public static CLevelGenerator instance;

        public List<CLevelPiece> levelPrefabs = new List<CLevelPiece>();
        [SerializeField] Transform levelStartPoint;
        public List<CLevelPiece> pieces = new List<CLevelPiece>();

        [SerializeField] CLevelPiece pieceFinish;
        private int pieceCount = 0;

        public void Awake()
        {
            { if (instance == null) { instance = this; } }
        }

        void Start()
        {
            GenerateInitialPieces();
        }

        public void GenerateInitialPieces()
        {
            for (int i = 0; i < 3; i++)
            {
                AddPiece();
            }
        }

        public void AddPiece()
        {
            CLevelPiece piece = new CLevelPiece();

            piece = (CLevelPiece)Instantiate(levelPrefabs[pieceCount]);
            piece.transform.SetParent(this.transform, false);

            Vector3 spawnPosition = Vector3.zero;
            Vector3 offset = new Vector3(0, 5.44f, 0);

            if (pieces.Count == 0)
            {
                spawnPosition = levelStartPoint.position;
            }
            else
            {
                spawnPosition = pieces[pieces.Count - 1].exitPoint.position + offset;
            }

            piece.transform.position = spawnPosition;

            pieces.Add(piece);
            pieceCount++;

            if (pieceCount >= 3)
            {
                pieceCount = 0;
            }
        }

        public void RemoveOldestPiece()
        {
            CLevelPiece oldestPiece = pieces[0];
            pieces.Remove(oldestPiece);
            Destroy(oldestPiece.gameObject);
        }
    }
}

