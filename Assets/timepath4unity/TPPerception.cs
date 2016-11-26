using UnityEngine;
using System.Collections;

using System.Collections.Generic;


/*!

 \brief When using Timepath, each object that can be perceived can have one or several perceptions (TPPerception) associated
 \date August 2014
 \details 
 
    Each perception can be created through the Perception Inspector, which allows selecting the functions available in TPPerception. 
    To define a new perception, it is enough to define a function that does not take more than one parameter, and which changes the double "value".

    Important:  
do NOT declare class variables, since each Action defined will be executed separately (similar to static methods). 
 It will work for methods called within one same Perception, but it is dangerous.
  
To deal with objects of the scene, it is usefull to declare a mental bag with the elements needed

    To get the agent, use:  TPAgent me = TP.GetAgent(mindID);
 and eventually the ADAPT body:  
     Body b = me.GetComponent<Body>();
or the mental bag:
    TPMentalBag bag = me.GetComponent<TPMentalBag>();
 * 
 * 
 * 
 * 
 \todo find out how to connect a perception to a physics simulation (eventually through mental bag?)
 \sa  TPPersonality TPPlot
*/
using timepath4unity;


    [System.Serializable]
    public class TPPerception : TPPerceptionBase
    {


        #region DO NOT USE CLASS VARIABLES IN TPPerception

       
        #endregion



        #region PERCEPTION FUNCTIONS AVAILABLE IN MENU_______________________________________________________________________________________

       

        
   




        public void IamOnRegion(GameObject region)
        {
            TPAgent me = TP.GetAgent(mindID);
       
            TPMentalBag bag = me.GetComponent<TPMentalBag>();


            RaycastHit hitInfo;
            Vector3 dir = transform.TransformDirection(Vector3.down);


            //define the region of arrival with your tag. make sure the baking is done AFTER the regions are frozen, and that they all are rendered.

                if (Physics.Raycast(transform.position , dir, out hitInfo, 10))
                {
                 //   Debug.Log("collided tag: " + hitInfo.collider.tag);

                    if (hitInfo.collider.tag == bag.destinationTag )
                    {

                        value = 1.0f;

                    }else{
                        value = 0.0f;


                    }
                }
           	


        }

    public void perceiveLow()
    {

        value = 0.096;
    }

    public void perceiveHigh()
    {
        value = 0.98;
    }



    public void getResourceCount(string name)
        {
            TPResource res = TP.GetAgent(this.mindID).MyPerso.GetResourceByName(name);
            if (res == null)
                value = 0.0;
            else if (res.AmountAvailable > 1.0)
                value = 1.0;
            else if (res.AmountAvailable <= 0)
                value = 0.0;
            else
                value = res.AmountAvailable;

        }

        public void doIHaveAtLeastOneOfResource(string name)
        {
            

            TPResource res = TP.GetAgent(this.mindID).MyPerso.GetResourceByName(name);
       
            if (res == null)
                value = 0.0;
            else if (res.AmountAvailable >= 1.0)
                value = 1.0;
            else
                value = 0.0;
        }


    public void iHaveObjectWithTag(string name)
    {
        TPAgent me = TP.GetAgent(mindID);
        TPMentalBag bag = me.GetComponent<TPMentalBag>();


        if (bag.M)
        {
            if (bag.M.tag.Equals(name))
                value = 1.0;
            else
                value = 0.0;
        }
        else
        {
            value = 0.0;

        }
    }

        





    #endregion

    #region OTHER USEFUL FUNCTIONS___________________________________________________________________
    public static GameObject FindClosestObjectTagged(int agentID, string theTag)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(theTag);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = TP.GetAgent(agentID).transform.position;
            // Iterate through them and find the closest one
            foreach (GameObject go in gos)
            {
                float curDist = (go.transform.position - position).sqrMagnitude;
                if (curDist < distance)
                {
                    closest = go;
                    distance = curDist;
                }
            }

            if (closest != null && !closest.tag.Equals(theTag))
            {
                Debug.Log("the object selected does not have  the tag " + theTag + "but rather " + closest.tag);
                return null;
            }

            
            return closest;
        }


        public static GameObject[] FindObjectTaggedInRadius(GameObject target, string theTag, float far, float near)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(theTag);
            List<GameObject> closeObjects = new List<GameObject>();
            Vector3 position = target.transform.position;

            foreach (GameObject go in gos)
            {
                float curDist = (go.transform.position - position).magnitude;
                if (curDist < far && curDist > near)
                {
                    closeObjects.Add(go);
                }
            }
            return closeObjects.ToArray();
        }


        #endregion


    }

