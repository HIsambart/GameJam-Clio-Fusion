using System.Collections.Generic;
using MiniGames;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Branch.Hugo.Scripts
{
    public class DebrisSpawnerHandler : MonoBehaviour
    {
        [Header("===== PREFABS =====")]
        [SerializeField] private GameObject _debrisPrefab;

        [Header("===== SPAWN SETTINGS =====")]
        [SerializeField] private float _tickRate = 1f;
        [SerializeField] private float _innerRadius = 9.5f;
        [SerializeField] private float _outerRadius = 12f;
        [SerializeField] private Vector2Int _spawnCountRange = new(8, 12);
        
        [Header("===== REFERENCES =====")]
        [SerializeField] private ContinueMiniGame _continueMiniGameTemperature;
        
        [Header("===== DEBUG =====")]
        [SerializeField] private List<GameObject> _debrisPrefabs;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (_timer > _tickRate)
            {
                ClearDebris();
                
                if (_continueMiniGameTemperature.CurrentLevel < _continueMiniGameTemperature.MinimumLevel
                    && _debrisPrefabs.Count < 3)
                {
                    SpawnDebris();
                }
                
                _timer = 0f;
            }
        }

        [ContextMenu("Spawn Debris")]
        public void SpawnDebris()
        {
            int spawnCount = Random.Range(_spawnCountRange.x, _spawnCountRange.y + 1);
            
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 randomCirclePoint = Random.insideUnitCircle.normalized;
                float randomDistance = Random.Range(_innerRadius, _outerRadius);

                Vector3 spawnOffset = new Vector3(
                    randomCirclePoint.x * randomDistance, 
                    0f, 
                    randomCirclePoint.y * randomDistance
                );

                Vector3 spawnPosition = transform.position + spawnOffset;

                GameObject go = Instantiate(_debrisPrefab, spawnPosition, Quaternion.identity, transform);
                _debrisPrefabs.Add(go);
            }
        }
        
        private void ClearDebris()
        {
            _debrisPrefabs.RemoveAll(go => go == null);
        }

        #region ===== DEBUG =====

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            DrawGizmoDisk(transform.position, _innerRadius);
            
            Gizmos.color = Color.red;
            DrawGizmoDisk(transform.position, _outerRadius);
        }

        private void DrawGizmoDisk(Vector3 center, float radius)
        {
            float angle = 0f;
            Vector3 lastPos = center + new Vector3(radius, 0, 0);
            for (int i = 1; i <= 32; i++)
            {
                angle += (Mathf.PI * 2f) / 32f;
                Vector3 nextPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                Gizmos.DrawLine(lastPos, nextPos);
                lastPos = nextPos;
            }
        }

        #endregion
    }
}