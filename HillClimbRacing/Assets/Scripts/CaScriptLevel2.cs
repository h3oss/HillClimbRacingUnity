using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaScriptLevel2 : MonoBehaviour
{
    WheelJoint2D[] wheelJoints;
    JointMotor2D frontWheel;
    JointMotor2D backWheel;

    public float maxSpeed = -1000;
    private float maxBackSpeed = 1500f;
    private float acceleration = 500f;
    private float deacceleration = -100f;
    public float brakeForce = 3000f;
    private float gravity = 9.8f;
    private float angleCar = 0;
    public bool grounded = false;
    public LayerMask map;
    public Transform bwheel;
    public Text coinsText;
    private int coinsInt = 0;


    public ClickScript[] ControlCar;


    void Start()
    {
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        backWheel = wheelJoints[1].motor;
        frontWheel = wheelJoints[0].motor;
    }

    void Update()
    {
        coinsText.text = coinsInt.ToString();
        grounded = Physics2D.OverlapCircle(bwheel.transform.position, 0.08f, map);
    }

    void FixedUpdate()
    {
        frontWheel.motorSpeed = backWheel.motorSpeed;
        angleCar = transform.localEulerAngles.z;

        if (angleCar >= 180)
        {
            angleCar = angleCar - 360;
        }

        if (ControlCar[0].clickedIs == true)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, maxSpeed, maxBackSpeed);

        }
        if ((ControlCar[0].clickedIs == false && backWheel.motorSpeed < 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar < 0))
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angleCar / 180) * 80) * Time.deltaTime, maxSpeed, 0);
        }
        else if ((ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar > 0))
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angleCar / 180) * 80) * Time.deltaTime, 0, maxBackSpeed);
        }

        else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed < 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration * Time.deltaTime, maxSpeed, 0);
        }

        else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxBackSpeed);
        }

        if (ControlCar[1].clickedIs == true && backWheel.motorSpeed > 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);

        }

        else if (ControlCar[1].clickedIs == true && backWheel.motorSpeed < 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxSpeed, 0);
        }

        wheelJoints[1].motor = backWheel;
        wheelJoints[0].motor = frontWheel;
    }


    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "coin")
        {
            coinsInt++;
            Destroy(trigger.gameObject);
        }

        else if (trigger.gameObject.tag == "finish")
        {
            SceneManager.LoadScene(0);
        }
    }



}
