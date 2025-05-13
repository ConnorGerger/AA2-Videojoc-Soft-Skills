using StarterAssets;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class PlayerAttack : MonoBehaviour {
    public int damageId = 0;

    public float attackPowLast = 0;
    public float attackPowMax = 1;

    public float attackOffsetDist = 1;
    public float attackDist = 10;

    public float charge = 0;
    public float chargeMax = 2;

    public bool charging = false;

    public float colTimeMax = 0.5f;

    public Transform attackInitPos;

    public Collider2D attackCol;
    public Animator anim;
    public string animTrig = "attack";

    public StarterAssetsInputs inputs;


    void OnEnable() {
        if (inputs == null)
            inputs = GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
        if (attackCol == null)
            attackCol = GetComponent<Collider2D>();

        inputs.Attack += Attack;
        inputs.Charge += Charge;
    }
    void OnDisable() {
        charging = false;

        inputs.Attack -= Attack;
        inputs.Charge -= Charge;
    }

    public void Attack(bool c) {
        if (charge == 0 && c) return;

        attackPowLast = charge * attackPowMax / chargeMax;
        charge = 0;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = pos - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        if (anim != null)
            anim.SetTrigger(animTrig);

        attackCol.enabled = true;
        StartCoroutine(ColDis());
    }
    public void Charge(bool c) {
        charging = c;
    }

    void FixedUpdate() {
        if (charging && charge < chargeMax) charge += Time.deltaTime;
        if (charge > chargeMax) charge = chargeMax;
    }

    IEnumerator ColDis() {
        yield return new WaitForSeconds(colTimeMax);
        attackCol.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (attackPowLast <= 0) return;

        StopAllCoroutines();

        if (col.GetComponent<DamageReceiver>()) {
            if (damageId == col.GetComponent<DamageReceiver>().damageReceiveId) {
                Vector2 dir = col.transform.position - attackInitPos.position;
                RaycastHit2D hit = Physics2D.Raycast(attackInitPos.position, dir, attackDist);
                if (hit.collider != null) {
                    if (hit.transform.GetComponent<DamageReceiver>()) {
                        if (damageId == hit.transform.GetComponent<DamageReceiver>().damageReceiveId) {
                            Debug.Log("DamagedEnemy_" + attackPowLast);
                            hit.transform.GetComponent<DamageReceiver>().ReceiveDamage(attackPowLast);
                            attackPowLast = 0;
                            attackCol.enabled = false;
                        }
                    }
                }
            }
        }

    }

    /*void OnDrawGizmos() {
        Vector2 right = new Vector2(1, 0);
        Gizmos.DrawLine(attackInitPos.position, attackInitPos.position* right * attackDist);

    }*/

}
