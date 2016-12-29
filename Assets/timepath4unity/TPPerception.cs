using UnityEngine;
using System.Collections;

using System.Collections.Generic;


/*!

 \brief When using Timepath, each object that can be perceived can have one or several perceptions (TPPerception) associated
 \date August 2014
 \details 
 
 	Each perception can be created through the Perception Inspector, which allows selecting the functions available in TPPerception. 
 	To define a new perception, it is enough to define a function that does not take more than one parameter, and which changes the double "value".

 	Accesory variables can also be defined (see for example the variables near and far, and the function isTagNear).

 \sa  TPPersonality TPPlot
*/
using timepath4unity;


    [System.Serializable]
    public class TPPerception : TPPerceptionBase
    {
    protected TPMentalBag MentalBag
    {
        get { return Me.GetComponent<TPMentalBag>(); }
    }

    #region DO NOT USE CLASS VARIABLES IN  PERCEPTION FUNCTIONS



    #endregion



    #region PERCEPTION FUNCTIONS AVAILABLE IN MENU_______________________________________________________________________________________
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
            TPResource res = Me.MyPerso.GetResourceByName(name);
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
            TPResource res = Me.MyPerso.GetResourceByName(name);
            if (res == null)
                value = 0.0;
            else if (res.AmountAvailable >= 1.0)
                value = 1.0;
            else
                value = 0.0;
        }


    public void doIHaveAtLeastOneM()
    {

        TPMentalBag bag = MentalBag;
        if (bag.M != null && bag.M.tag.Equals("picked"))
            value = 1.0;
        else
            value = 0.0;
    }



    

        #endregion

        #region OTHER USEFUL FUNCTIONS___________________________________________________________________
        public static GameObject FindClosestObjectTagged(GameObject mybody, string theTag)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(theTag);
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = mybody.transform.position;
            // Iterate through them and find the closest one
            foreach (GameObject go in gos)
            {
                float curDist = (go.transform.position - position).magnitude;
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

