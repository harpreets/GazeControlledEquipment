using UnityEngine;
using System;
using System.Collections;

public class KinectHeadTracker : MonoBehaviour {
   // public GameObject headGO;

    [Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("Whether the cubeman is facing the player or not.")]
    public bool mirroredMovement = false;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    private Int64 initialPosUserID = 0;

    private Vector3 leftHandJointPos;
    private Vector3 leftShouldJointPos;
    private Vector3 rightHandJointPos;
    private Vector3 rightShoulderJointPos;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    void Update()
    {
        KinectManager manager = KinectManager.Instance;

        // get 1st player
        Int64 userID = manager ? manager.GetUserIdByIndex(playerIndex) : 0;

        if (userID <= 0)
        {
            // reset the pointman position and rotation
            if (transform.position != initialPosition)
            {
                transform.position = initialPosition;
            }

            if (transform.rotation != initialRotation)
            {
                transform.rotation = initialRotation;
            }

            return;
        }
        transform.position = new Vector3(manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).x, manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).y, -manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).z);

        leftHandJointPos = new Vector3(manager.GetJointPosition(userID, (int)KinectInterop.JointType.HandLeft).x, manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).y, -manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).z);
        //Vector3 leftHandHeadDir = transform.position - leftHandJointPos;
        Debug.DrawLine(transform.position, leftHandJointPos, Color.red);

        //leftShouldJointPos = new Vector3(manager.GetJointPosition(userID, (int)KinectInterop.JointType.ShoulderLeft).x, manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).y, -manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).z);
        rightHandJointPos = new Vector3(manager.GetJointPosition(userID, (int)KinectInterop.JointType.HandRight).x, manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).y, -manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).z);
        //rightShoulderJointPos = new Vector3(manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).x, manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).y, -manager.GetJointPosition(userID, (int)KinectInterop.JointType.Head).z);

        Quaternion rotaJoint = manager.GetJointOrientation(userID, (int)KinectInterop.JointType.Head, !mirroredMovement);
        rotaJoint = initialRotation * rotaJoint;
        transform.rotation = rotaJoint;
    }
}
