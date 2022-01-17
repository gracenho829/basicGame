using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speedv = 1.0f; 
    public float jumpPower = 10.0f; 
    public Animator playerAnim;
    public float rotateSpeed = 2.0f;
    public GameObject bullet;
    public Transform firePos;
    private Rigidbody rigid; 
    bool isJump;
    public Camera mainCamera;
    private Vector2 input;
    private Vector3 targetDirection;
    private Quaternion freeRotation; 
    // Start is called before the first frame update
    void Awake()
    {
        isJump = false;
       rigid = this.GetComponent<Rigidbody>();
       StartCoroutine(Fire(1.0f));
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && !isJump) {
            isJump = true;
            rigid.AddForce(new Vector3(0,jumpPower,0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        //세심한 차이까지 다  계산 할  수 있음
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Vector3 dir =targetDirection;
        rigid.AddForce(dir*speedv, ForceMode.Impulse);

        if (input.x==0 && input.y==0){
            playerAnim.SetBool("isRun", false);
        }
        else {
            playerAnim.SetBool("isRun", true);
        }
        UpdateTargetDirection();
        if(input!= Vector2.zero && targetDirection.magnitude > 0.1f){
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var differenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (differenceRotation != 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0,eulerY,0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), rotateSpeed*Time.deltaTime); 
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "floor"){
            isJump = false;
        }
    }

    private IEnumerator Fire(float coolTime){
        while (true){
            if (Input.GetMouseButtonDown(0)){
                Instantiate(bullet, firePos.position, this.transform.rotation);
                 yield return new WaitForSeconds(coolTime);
            }
            yield return null; 
        }
    }

    public void UpdateTargetDirection(){
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        var right = mainCamera.transform.TransformDirection(Vector3.right);
        targetDirection = input.x * right + input.y * forward; 
    }
}
