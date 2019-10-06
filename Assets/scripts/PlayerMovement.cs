using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //public Transform cameraTarget;
    public Transform camera;
    public Text textV;
    public Text textH;
 
    public bl_Joystick Joystick;//Joystick reference for assign in inspector

    public float moveSpeed = 5;
    public float rotateSpeed = 20;

    private CharacterController charControl;

    private float gravityForce = -1.0f;

    Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        charControl = GetComponent<CharacterController>();
  
    }

    // Update is called once per frame
    void Update()
    {
  
        Vector3 movement = new Vector3(Joystick.Horizontal , 0, Joystick.Vertical );
        movement = Vector3.ClampMagnitude(movement*moveSpeed, moveSpeed);//*moveSpeed, moveSpeed);
        movement.y = gravityForce;

        textV.text = Joystick.Vertical.ToString()+":";
         textV.text += movement.z.ToString() + "::" + gravityForce;

        textH.text = Joystick.Horizontal.ToString()+":";
         textH.text += movement.x.ToString()+"::"+movement.magnitude.ToString();

        if (movement.magnitude > 0)
        {
            GetComponent<Animator>().SetBool("isMove", true);
            //charControl.Move(verInput);
            //transform.Translate(0, 0, verInput);

            // transform.Rotate(0, horInput, 0);




            Quaternion tmp = camera.rotation;
              camera.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
              movement = camera.TransformDirection(movement);
              camera.rotation = tmp;
            Vector3 movementWithGrav = new Vector3(movement.x, 0, movement.z);
            if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
            {
                GetComponent<Animator>().SetBool("isMove", true); 
                // transform.rotation = Quaternion.LookRotation(movement);
           
                GetComponent<Animator>().SetBool("isRun", (Mathf.Abs(movementWithGrav.magnitude) > moveSpeed*2 / 3));

                Quaternion direction = Quaternion.LookRotation(movementWithGrav);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotateSpeed * Time.deltaTime);
            }
            else
            {
                GetComponent<Animator>().SetBool("isMove", false);
                GetComponent<Animator>().SetBool("isRun", false);
            }
            
            movement *= Time.deltaTime;

            charControl.Move(movement);
            camera.position = new Vector3(this.transform.position.x, this.transform.position.y + 20, this.transform.position.z - 20.0f);

        }

        

        Gravity();

    }

    private void Gravity()
    {
        if (!charControl.isGrounded) gravityForce = -20f;
        else gravityForce = -1.0f;
    }
}
