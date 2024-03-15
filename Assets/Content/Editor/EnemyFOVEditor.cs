using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tactics_Prototype {

    [CustomEditor (typeof(EnemyFOV))]
    public class EnemyFOVEditor : Editor {
        void OnSceneGUI() {
            EnemyFOV fov = (EnemyFOV)target;
            Handles.color = Color.red;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

            Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

            Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleA * fov.viewRadius));
            Handles.DrawLine(fov.transform.position, fov.transform.position + (viewAngleB * fov.viewRadius));

            Handles.color = Color.green;
            foreach (Transform visibleTarget in fov.visibleTargets) {
                Handles.DrawLine(fov.transform.position, visibleTarget.position);
            }
        }
    }
}
