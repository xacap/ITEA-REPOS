using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CFollowPath : MonoBehaviour
    {
        public enum EMovementType
        {
            moveing,
            lerping
        }

        [SerializeField] EMovementType eMovementType = EMovementType.moveing;
        private CMovementPath myPath;
        [SerializeField] float speed = 3;
        [SerializeField] float maxDistance = 0.1f;

        private IEnumerator<Transform> pointInPath; // проверка точек

        private void Awake()
        {
            var myPathObj = GameObject.Find("PathEnemy");
            myPath = myPathObj.GetComponent<CMovementPath>();
        }
        void Start()
        {
            if (myPath == null) // проверка прикреплен ли путь
            {
                return;
            }

            pointInPath = myPath.GetNextPathPoint(); // обращение к коротину
            pointInPath.MoveNext(); // получение следующей точки в пути

            if (pointInPath.Current == null) // есть ли точка к торой двигаться
            {
                return;
            }

            transform.position = pointInPath.Current.position; // объект на стартовой точке пути
        }

        
        void Update()
        {
            if (pointInPath == null || pointInPath.Current == null)
            {
                return;
            }

            if (eMovementType == EMovementType.moveing)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed); // двигаем объект к следующей точке
            }
            else if (eMovementType == EMovementType.lerping)
            {
                transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed); // двигаем объект к следующей точке
            }

            var distanceSqure = (transform.position - pointInPath.Current.position).sqrMagnitude; // проверяем дошли ли мы до нужной токи

            if (distanceSqure < maxDistance * maxDistance)
            {
                pointInPath.MoveNext();
            }
        }
    }

}
