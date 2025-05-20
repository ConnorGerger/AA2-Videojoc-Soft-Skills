using UnityEngine;


public class CircuitReceiverAnim : CircuirBase {
    public bool active = false;

    public float receiveAmountMin = 0.5f;

    public Animator anim;
    public string animTrig = "active";

    public SpriteRenderer sprR;


    private void Start() {
        sprR = GetComponent<SpriteRenderer>();
    }

    public override void Receive(float amount) {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (active) return;
        if (col.GetComponent<CircuitReceiverEmiter>()) {
            float amount = col.GetComponent<CircuitReceiverEmiter>().currentEmitAmount;
            if (amount < receiveAmountMin) return;

            active = true;

            anim.SetTrigger(animTrig);

            sprR.color = Color.green;
        }
    }
    
}
