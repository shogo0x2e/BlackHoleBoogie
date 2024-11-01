using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AstronautRagdoll : MonoBehaviour {
    [SerializeField] private List<Rigidbody> hitJoints;
    [SerializeField] private List<Rigidbody> armJoints;
    [SerializeField] private List<Rigidbody> legJoints;


    public void Start() {
        Destroy(gameObject, 6f);
    }

    public void KnockBack(Vector3 colPosition, float colForce) {
        foreach (var hitJoint in hitJoints) {
            hitJoint.AddExplosionForce(colForce, colPosition, 10F);
        }

        foreach (var armJoint in armJoints) {
            armJoint.AddTorque(1000f, 0, 0);
        }

        foreach (var legJoint in legJoints) {
            legJoint.AddTorque(-1000f, 0, 0);
        }
    }

    public void OnCollisionEnter(Collision c) {
        foreach (var hitJoint in hitJoints) {
            hitJoint.AddExplosionForce(8F, c.contacts[0].point, 1000F);
        }
    }
}
