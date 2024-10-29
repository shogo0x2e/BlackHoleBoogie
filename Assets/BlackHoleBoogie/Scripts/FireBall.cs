using Object;
using Object.Spawnable;
using UnityEngine;

public class FireBall : AbstractObject {
    private Alien srcShooter;

    private float psStartSize1;
    private float psStartSize2;

    public override void OnSpawn() {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 direction = (BlackHole.GetInstance().GetPosition() - transform.position).normalized;
        rb.AddForce(6F * direction, ForceMode.Impulse);

        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        psStartSize1 = particleSystems[0].main.startSizeMultiplier;
        psStartSize2 = particleSystems[1].main.startSizeMultiplier;

        Destroy(gameObject, 7.2F);
    }

    public new void Update() {
        base.Update();

        if (IsSuckedIn()) {
            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
            ScaleParticleSystem(particleSystems[0], psStartSize1);
            ScaleParticleSystem(particleSystems[1], psStartSize2);
        }
    }

    private void ScaleParticleSystem(ParticleSystem ps, float baseSize) {
        ParticleSystem.MainModule main = ps.main;
        main.startSizeMultiplier = baseScale * (shrinkTime - shrinkTimeAcc) * (1F / shrinkTime);
    }

    public override void GetSucked() {
        base.GetSucked();

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        SetMoveSpeed(0.6F);
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

        if (IsDestroyed()) {
            return;
        }

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
