using UnityEngine;
using System.Collections;
using System.Collections.Generic;




using timepath4unity;

/*!
\brief
TPPAction implements a Timepath action.
It will be performed by a TPPersonality. 
It can also be part of a TPPlotSentence within a story plot (TPPlot).


*/
    public  class TPAction : TPActionBase
    {



        #region VARIABLES USED BY SOME ACTION FUNCTIONS

        GameObject target = null;

        float speed = 1.0f;
        #endregion

        #region ACTIONS


        // a reference to the animator on the character
        static int deadState = Animator.StringToHash("Base Layer.Dying");



        public void IGrowLikeActivation()
        {
            this.transform.localScale = this.transform.localScale * (float)this.decisiveness;
        }
        public void IDoubleSize()
        {
            TP.GetAgent(mindID).transform.localScale = TP.GetAgent(mindID).transform.localScale * 2.0f;
        }

        public void TargetMultiplySize(float times)
        {
            if (target)
                target.transform.localScale = target.transform.localScale * times;
        }

        public void selectClosestObjectWithTag(string tag)
        {
            if (this.target == null)
                this.target = TPPerception.FindClosestObjectTagged(mindID, tag);
        }
        public void approachTarget()
        {
            if (target != null)
                approach(mindID, target, (float)this.ActionIntensity);

        }


        //! \todo NOT TESTED
        public void selectClosestCharacterWithRole(string roleName)
        {

            target = TPPerception.FindClosestCharacterWithRole(mindID, roleName);

        }

        public void approachObjectWithTag(string tag)
        {
            approach(mindID, TPPerception.FindClosestObjectTagged(mindID, tag), (float)this.ActionIntensity);
        }

        public void setSpeed(float s)
        {
            speed = s;

        }

        public void selectMyselfAsTarget()
        {
           this.target = TP.GetAgent(mindID).gameObject;


        }

        public void rotateX90()
        {
            if (target != null)
            {

                target.transform.Rotate(90, 0, 0);

            }


        }

        public void selectTargetFromAgentCollider(string tag)
        {

            GameObject go = TP.GetAgent(mindID).getObjectCollidedWithTag(tag);
            if (go)
            {
                target = go;
            }
        }


        public void PickSelectedObjectAsResource(string resname)
        {
            if (target != null)
            {
                //! we assumes there exists a resource named as the tag of the selected gameObject
                TP.GetAgent(mindID).MyPerso.AddResourceUnits(resname, 1.0f);
                Destroy(target);
            }
        }

        //! \todo these functions should allow selecting the tag from a popup list
        public void changeTargetTag(string newTag)
        {
            if (target != null)
            {
                if (target.tag.Equals(newTag))
                {
                    Debug.Log("you are trying to change the tag of an object which already has the tag " + newTag);
                    return;
                }
                target.tag = newTag;
                target.name = newTag;
                target.transform.parent = null;
            }
        }

        public void say(string text)
        {
            Vector3 pos = TP.GetAgent(mindID).transform.position + 2.0f * Vector3.up;

            Debug.Log(TP.GetAgent(mindID).name + ": " + text);
            //! \todo characters must have a hidden LABEL field 
        }


        //! it removes any tags and leaves it as a son.
        public void LeaveWithSelectedObjectResourceNamed(string resourceName)
        {
            if (target != null)
            {

                GameObject resourceInstance = TP.GetAgent(mindID).MyPerso.GetResourceUnits(resourceName, 1.0f);

                if (resourceInstance)
                {
                    resourceInstance.SetActive(true);
                    if (target != null) { 
                    try
                    {
                        resourceInstance.tag = target.tag;
                    }
                    catch
                    {
                        Debug.Log("trouble retaging object" + resourceInstance.name);
                    }
                }
                    resourceInstance.transform.parent = target.transform;

                    Vector3 temp = Random.onUnitSphere;
                    temp.Scale(new Vector3(1.0f, 0.0f, 1.0f));
                    resourceInstance.transform.position = target.transform.position + temp;
                }
            }
        }


        public void unselectTarget()
        {

            this.target = null;

        }

        /*
        public void setAnimFloat(string paramName){
            //anim.SetFloat (animFloat);
            }
	
        */

        //a function to turn around an object	

        /*
        public void turnAroundObject(GameObject go){

            TPAgent me=TPPersonalityTools.getAgent(agentID);
		
            Vector3 dist=me.transform.position - go.transform.position;
            float angle=Vector3.Angle(dist, me.transform.forward);
            turn (agentID,(180.0f-angle)/180.0f);
            runForward (agentID,1.0f);
            putToFloor ();
        }*/

        #endregion


        #region OTHER USEFUL FUNCTIONS

        //! a function to avoid an object
        void avoid(int agentID, GameObject go, float d)
        {
            TPAgent me = TP.GetAgent(agentID);
            Vector3 dist = me.transform.position - go.transform.position;
            //float angle=Vector3.Angle(dist, me.transform.forward);
            Vector3 increment = speed * Mathf.Clamp((float)d, 0.4f, 1.0f) * dist.normalized * Time.deltaTime;
            me.transform.Translate(increment);
            Debug.Log("I am avoiding" + go.name + " " + d + " " + increment);
            putToFloor();

            /*
            turn (agentID, angle/180.0f);
            if(angle  < 30 ){
                runForward (agentID,d);	
                Debug.Log ("I am avoiding" + go.name);
            }
            else{
                //Debug.Log ("I am not avoiding any more " + go.name);
                me.GetComponent<Animator>().speed = 0.0f;
            }*/
        }


        //a function to approach an object
        void approach(int agentID, GameObject go, float d)
        {
            TPAgent me = TP.GetAgent(agentID);

            if (go != null)
            {
                Vector3 dist = go.transform.position - me.transform.position;
                //float angle=Vector3.Angle(dist, me.transform.forward);
                Vector3 increment =   speed * dist.normalized * Time.deltaTime;
                increment.y = 0.0f;//to prevent it from flying or going down
                me.transform.Translate(increment);
            }
            putToFloor();


        }




        void turn(int agentID, float h)
        {
            TPAgent me = TP.GetAgent(agentID);
            me.transform.Rotate(Vector3.up, h);
        }


        void runForward(int agentID, float v)
        {

            TPAgent me = TP.GetAgent(agentID);
            Animator anim = me.GetComponent<Animator>();
            AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);// set our currentState variable to the current state of the Base Layer (0) of animation
            anim.speed = 1.5f;

            anim.SetFloat("forward", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				

            if (currentBaseState.nameHash != deadState)
            {

                me.transform.Translate(5 * anim.speed * v * Vector3.forward * Time.deltaTime);


            }
            putToFloor();

        }

        void putToFloor()
        {


            // To stick it to the ground, we Raycast down from the center of the character.. 
            Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
            RaycastHit hitInfo = new RaycastHit();

            transform.Translate(Vector3.down * 1.0f);

            transform.Translate(Vector3.down * hitInfo.distance);

        }



        void die(int agentID, float a)
        {

            TPAgent me = TP.GetAgent(agentID);
            Animator anim = me.GetComponent<Animator>();
            anim.SetFloat("energy", -1.0f);


        }




        #endregion

    }

