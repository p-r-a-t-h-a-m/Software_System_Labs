using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    private float CurrPosX;
    private Vector3 velocity=Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookahead;

    private void Update()
    {
        //transform.position=Vector3.SmoothDamp(transform.position, new Vector3(CurrPosX,transform.position.y,transform.position.z), ref velocity,speed);
        transform.position=new Vector3(player.position.x,transform.position.y, transform.position.z);
        lookahead=Mathf.Lerp(lookahead,(aheadDistance*player.localScale.x), Time.deltaTime*cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        CurrPosX=_newRoom.position.x;
    }
}