using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public static class GameObjectExt
    {
        /// <summary>
        /// Returns the closest object in a list from obj.
        /// </summary>
        public static T FindClosestFrom<T>(this T[] objList, GameObject obj) where T : MonoBehaviour
        {
            if (objList == null)
                throw new Exception("List of objects is null.");

            if (!objList.Any())
                throw new Exception("List of objects is empty.");

            if (obj == null)
                throw new Exception("Object is null.");

            float closestObjectDist = float.MaxValue;
            T closestObject = null;

            // Loop through all objects
            foreach (T enemyBehaviour in objList)
            {
                // Calculate the distance
                float dist = (obj.transform.position - enemyBehaviour.transform.position).magnitude;

                // First case
                if (closestObject == null)
                {
                    closestObject = enemyBehaviour;
                    closestObjectDist = dist;
                    continue;
                }

                // Check if it's closer than previous
                if (dist < closestObjectDist)
                {
                    closestObjectDist = dist;
                    closestObject = enemyBehaviour;
                }
            }

            return closestObject;
        }
    }
}