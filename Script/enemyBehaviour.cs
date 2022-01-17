using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyBehaviour : MonoBehaviour
{
    public AudioSource audioS;

    [SerializeField]
    Transform target;
    [SerializeField]
    NavMeshAgent enemy;
    public GameObject redBullet;
    public Transform firePos;
    private bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(target.position);
        if(Vector3.Distance(transform.position, target.position)>2.1f){
             this.GetComponent<Renderer>().material.color = Color.white;
        }else{
            attack();
            
        }
    }

    private void attack(){
      
        StartCoroutine(Fire(1.0f));
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "bullet"){
            audioS.Play();
            Destroy(this.gameObject, 1.0f);
        }
    }

    private IEnumerator Fire(float coolTime){
        if(isShooting) yield break;
       
        isShooting = true;
        Instantiate(redBullet, firePos.position, this.transform.rotation);
        yield return new WaitForSeconds(coolTime);
        
        isShooting =  false; 
    }
}
