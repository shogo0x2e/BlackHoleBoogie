using Object;
using Object.Spawnable;
using UnityEngine;

public class FireBall : AbstractObject {
    private Alien srcShooter;

    public override void OnSpawn() {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 direction = (BlackHole.GetInstance().GetPosition() - transform.position).normalized;
        rb.AddForce(6F * direction, ForceMode.Impulse);

        Destroy(gameObject, 7.2F);
    }

    public new void OnDestroy() {
        srcShooter.SetCanShoot(true);
    }

    public override void OnHeadCollision(Vector3 colPosition, float colForce) {
        KnockBack(colPosition, colForce);

        if (IsDestroyed() || colForce < softestForce) {
            return;
        }

        ScoreManager.scoreCount -= 30;
        SetDestroyed(true);
    }

    public override void OnArrowCollision(Vector3 colPosition, float colForce) {
        KnockBack(colPosition, colForce);
        ScoreManager.scoreCount += 80;
        Destroy(gameObject, 3.6F);
        SetDestroyed(true);
    }

    public override void OnSlap(Vector3 colPosition, float colForce) {
        KnockBack(colPosition, colForce);

        if (IsDestroyed() || colForce < softForce) {
            return;
        }

        ScoreManager.scoreCount += 60;
        Destroy(gameObject, 6F);
        SetDestroyed(true);
    }

    public override bool IsGrabbable() {
        return true;
    }

    public override void OnPunch(Vector3 colPosition, float colForce) {
        if (IsDestroyed() || colForce < softForce) {
            return;
        }

        ScoreManager.scoreCount += 60;
        Destroy(gameObject, 6F);
        SetDestroyed(true);
    }

    public void SetShooter(Alien value) {
        srcShooter = value;
    }
}
