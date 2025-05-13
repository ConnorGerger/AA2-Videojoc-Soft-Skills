using UnityEngine;


public class DamageReceiver : MonoBehaviour {
    public HealthSystem healthS;

    public int damageReceiveId = 0;
    public float damageMult = 1;


    public void ReceiveDamage(float dmg) {
        float totalDmg = dmg * damageMult;
        healthS.ReceiveDamage(totalDmg);
    }

}
