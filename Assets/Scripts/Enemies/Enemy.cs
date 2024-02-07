using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;
    public GameObject[] waypoints;
    public int currentDestination = -1;
    public GameObject pursuedPlayer = null;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update(){
        
    }

    [Task]
    bool ChooseDestination(){
        if(waypoints.Length == 0){
            return false;
        }
        currentDestination = (currentDestination + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentDestination].transform.position);
        return true;
    }

    [Task]
    void GoToDestination(){
        if(pursuedPlayer != null){
            Task.current.Fail();
            return;
        }
        if(agent.remainingDistance <= agent.stoppingDistance){
            Task.current.Succeed();
            return;
        }
    }

    [Task]
    void PursuePlayer(){
        if(pursuedPlayer == null){
            Task.current.Fail();
            return;
        }
        agent.SetDestination(pursuedPlayer.transform.position);
    }
}
