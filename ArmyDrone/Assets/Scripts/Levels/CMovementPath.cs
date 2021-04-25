using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CMovementPath : MonoBehaviour
    {
       public enum EPathTypes
        {
            linear,
            loop
        }

        [SerializeField] public EPathTypes ePathTypes; // определяю тип пути
        [SerializeField] int movementDirection = 1; // направление движения : вперед/назад
        [SerializeField] int moveingTo = 0; // к какой точке двигаться
        [SerializeField] Transform[] PathElements; // массив из точек движения

        public void OnDrawGizmos() // отбражает линии между точками пути
        {
            if (PathElements == null || PathElements.Length < 2)
            {
                return;
            }

            for (var i = 1; i < PathElements.Length; i++) // прогоняет все точки массива
            {
                Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); // рисует линию между ними
            }

            if (ePathTypes == EPathTypes.loop)
            {
                Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
            }
        }

        public IEnumerator<Transform> GetNextPathPoint() // получаем положение следующей точки
        {
            if (PathElements == null || PathElements.Length < 1) //проверяет, есть ли точки которым нужно проверять положение
            {
                yield break; // позволяет выйти из коротина если нашел несоответствие
            }

            while (true)
            {
                yield return PathElements[moveingTo]; //возвращает текущее положение точки

                if (PathElements.Length == 1) // если точка всего одна, выйти
                {
                    continue;
                }

                if (ePathTypes == EPathTypes.linear) // если линия не зациклена
                {
                    if (moveingTo <= 0) // если двигаемся по наростающей
                    {
                        movementDirection = 1; // добавляем 1 к движению
                    }
                    else if (moveingTo >= PathElements.Length - 1) // если двигаемся по убывающей
                    {
                        movementDirection = -1; // убираем 1 из движения
                    }
                }

                moveingTo = moveingTo + movementDirection; // диапазон движения от 1 до -1


                if (ePathTypes == EPathTypes.loop)
                {
                    if(moveingTo >= PathElements.Length) //если мы дошли до последней точки
                    {
                        moveingTo = 0; //то надо идти не в обратную сторону, а кпервой точке
                    }

                    if (moveingTo < 0) // если мы дошли до первой точки двигаясь в обратную сторону
                    {
                        moveingTo = PathElements.Length - 1; // то надо двинуть к последней
                    }
                }
            }
        }
    }
}

