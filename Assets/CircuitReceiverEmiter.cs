using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class CircuitReceiverEmiter : CircuirBase {
    public bool canReceive = true;
    public float receiveEmitCd = 1;
    public bool canEmit = true;
    public float receiveAmountMin = 1;

    public float emitSpeed = 5;
    public float baseRadius = 0;
    public float emitRadius = 2;
    public float currentEmitAmount = 0;
    public bool emiting = false;

    public GameObject circuitPassAxis;
    public float destroyTime = 1;
    public string animTrig = "attack";

    public CircleCollider2D circleC;

    public List<GameObject> alreadyConduit;


    void Start() {
        circleC = GetComponent<CircleCollider2D>();
        baseRadius = circleC.radius;
    }

    public override void Receive(float amount) {
        if (!canReceive || amount < receiveAmountMin) return;

        canReceive = false;
        currentEmitAmount = amount;
        StartCoroutine(ReceiveCd());

        if (canEmit && !emiting) Emit();
    }
    public void Emit() {
        emiting = true;

    }

    void FixedUpdate() {
        if (!emiting) return;

        if (circleC.radius < emitRadius)
            circleC.radius += emitSpeed * Time.deltaTime;
        if (circleC.radius >= emitRadius) {
            emiting = false;
            circleC.radius = baseRadius;
        }
    }

    IEnumerator ReceiveCd() {
        yield return new WaitForSeconds(receiveEmitCd);
        alreadyConduit.Clear();
        canReceive = true;
    }

    public void SpawnThunder(Transform pos) {
        var tmp = Instantiate(circuitPassAxis, transform.position, Quaternion.identity);
        tmp.transform.parent = transform;

        Vector2 direccion = pos.position - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        tmp.transform.rotation = Quaternion.Euler(0, 0, angulo);
        tmp.transform.localScale = new Vector3(direccion.magnitude, tmp.transform.localScale.y, tmp.transform.localScale.z);

        tmp.transform.GetChild(0).GetComponent<Animator>().SetTrigger(animTrig);

        Destroy(tmp, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<CircuirBase>()) {
            if (alreadyConduit.Contains(col.gameObject)) return;

            if (emiting&& !canReceive) {
                SpawnThunder(col.transform);
            }

            if (col.GetComponent<CircuitReceiverEmiter>()) {
                float amount = col.GetComponent<CircuitReceiverEmiter>().currentEmitAmount;
                if (!canReceive || amount < receiveAmountMin) return;

                canReceive = false;
                currentEmitAmount = amount;
                alreadyConduit.Add(col.gameObject);
                StartCoroutine(ReceiveCd());
            } else {
                if (!canReceive) return;

                canReceive = false;
                alreadyConduit.Add(col.gameObject);
                StartCoroutine(ReceiveCd());
            }

            SpawnThunder(col.transform);

            if (canEmit && !emiting) Emit();
        }
        if (col.GetComponent<DamageReceiver>()) {
            if (!emiting || alreadyConduit.Contains(col.gameObject)) return;

            SpawnThunder(col.transform);

            col.GetComponent<DamageReceiver>().ReceiveDamage(currentEmitAmount);

            canReceive = false;
            alreadyConduit.Add(col.gameObject);
            StartCoroutine(ReceiveCd());
        }
    }

}
