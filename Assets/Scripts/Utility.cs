using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public enum Axis
    {
        XZ,
        XY,
        YZ
    }

    public static List<Vector3> CalculateCirclePositions(Vector3 center, float radius, int count, Axis axis = Axis.XZ)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i; // Utilisation de 360° pour un cercle complet
            float radians = Mathf.Deg2Rad * angle;

            float x = 0, y = 0, z = 0;

            switch (axis)
            {
                case Axis.XZ:
                    x = center.x + radius * Mathf.Sin(radians);
                    z = center.z + radius * Mathf.Cos(radians);
                    y = center.y; // Pas de changement sur l'axe Y
                    break;

                case Axis.XY:
                    x = center.x + radius * Mathf.Sin(radians);
                    y = center.y + radius * Mathf.Cos(radians);
                    z = center.z; // Pas de changement sur l'axe Z
                    break;

                case Axis.YZ:
                    y = center.y + radius * Mathf.Sin(radians);
                    z = center.z + radius * Mathf.Cos(radians);
                    x = center.x; // Pas de changement sur l'axe X
                    break;
            }

            positions.Add(new Vector3(x, y, z));
        }

        return positions;
    }

}
