using UnityEngine;
using System.Collections;
using System.Collections.Generic;




using timepath4unity;

/*!
\brief
TPAction implements a behaviour in Simple Behavior Networks.

 Important:  
do NOT declare class variables, since each Action defined will be executed separately (similar to static methods). 
 It might work for methods called within one same Action, but it is dangerous way to proceed, no garantees.
  
To deal with objects of the scene, it is usefull to declare a mental bag with the elements needed

  
*/
public  class TPAction : TPActionBase
    {

    //it returns the mental bag associated with the agent
    protected TPMentalBag MentalBag
    {
        get { return Me.GetComponent<TPMentalBag>(); }
    }


    //it returns the body that hte agent will use
    protected Body MyBody
    {
        get { return MentalBag.body; }
    }



    #region DO NOT USE CLASS VARIABLES IN TPAction


    #endregion

    #region ACTIONS


    public void approachNearestObjectWithTag(string tag)
        {
            GameObject go = TPPerception.FindClosestObjectTagged((GameObject) MyBody.gameObject, tag);

        
            if (go)
            {
                MyBody.NavGoTo(go.transform.position);
            }
        
    }


    public void approachObjectCalled(string name)
        {

            GameObject go = GameObject.Find(name);
      
            if (go)
            {
                MyBody.NavGoTo(go.transform.position);

            }

    
        }

   
    public void approachObject(GameObject go )
    {

        if (go)
        {
            MyBody.NavGoTo(go.transform.position);

        }


    }




    public void leaveM()
        {
            

            TPMentalBag bag =MentalBag;
            
            if (bag.M)
            {
                bag.M.GetComponent<Rigidbody>().isKinematic = false;

                bag.M.GetComponent<Rigidbody>().useGravity = true;
                
                //and we put it back with all the meteorites in the hierarchy
                GameObject temp = GameObject.Find("Meteorites");
                if (temp)
                {
                    bag.M.transform.parent = temp.transform;
                }
                bag.M = null;

            
            }
        }

  
          
    public void takeMhome()
    {

        
        approachNearestObjectWithTag(MentalBag.destinationTag);
        
          RaycastHit hitInfo;
            Vector3 dir = transform.TransformDirection(Vector3.down);
            //define the region of arrival with your tag. make sure the baking is done AFTER the regions are frozen, and that they all are rendered.
                if (Physics.Raycast(MyBody.transform.position , dir, out hitInfo, 10))
                {

                    if (hitInfo.collider.tag == MentalBag.destinationTag)
                    {

                      leaveM();

                    }
                  
                }
    }


  
        public void selectM()
        {


   

        TPMentalBag bag = MentalBag;
        Body b = MentalBag.body;

        if (bag.M ==null){
                GameObject go = TPPerception.FindClosestObjectTagged(MyBody.gameObject, "pickme");
                if(go != null){
                    bag.M = go.GetComponent<Meteorite>();
                    bag.M.tag = "selected";
                }
            }
            else if (bag.M.tag != "selected") //because it fell on another region, for example
            {
                leaveM();
            }


        }

    /*
public void aproachM(float minDist)
    {

        Body b = Me.MyBody.GetComponent<Body>();
        TPMentalBag bag = Me.GetComponent<TPMentalBag>();

        if (bag.M)
        {

        //we approach
        if ((MyBody.transform.position - bag.M.transform.position).sqrMagnitude > minDist) //we are more far appart than 1 meter
            {


            if (b.NavIsStopped()) {
                Debug.Log("i approach M");
                // /todo why this does not work??
                b.NavGoTo(bag.M.transform.position);
            }

        }
        else { //we are closer than 1 meter
                b.NavStop();
            }
        }
    }

    */

    public void aproachM(float minDist)
{

    TPMentalBag bag = Me.GetComponent<TPMentalBag>();

    if (bag.M)
    {


        //we approach
        if ((bag.body.transform.position - bag.M.transform.position).sqrMagnitude > minDist) //we are more far appart than 1 meter
        {


            if (bag.body.NavIsStopped())
            {
                bag.body.NavGoTo(bag.M.transform.position);
            }

        }
        else { //we are closer than 1 meter
            bag.body.NavStop();
        }
    }
}






    public void lookM(float lookDist)
        {
           
            TPMentalBag bag = Me.GetComponent<TPMentalBag>();

            if (bag.M)
            {

                if ((transform.position - bag.M.transform.position).sqrMagnitude < lookDist)
                    //we reach

                    MyBody.HeadLookAt(bag.M.transform.position);

            }
        }

        public void reachM(float reachDist){
         
            Body b = MyBody.GetComponent<Body>();
        TPMentalBag bag = Me.GetComponent<TPMentalBag>();

        if (bag.M != null)
            {
                if ((MyBody.transform.position - bag.M.transform.position).sqrMagnitude < reachDist)
                    b.ReachFor(bag.M.transform.position);
            }
        }


        public void takeControlOverM(float reachDist){
         

   

        if (MentalBag.M != null)
            {


            float test = (MyBody.transform.position - MentalBag.M.transform.position).sqrMagnitude;
                if ( test < reachDist)
                {

                MentalBag.M.transform.parent = MyBody.transform;
                MentalBag.M.tag = "picked";

                Debug.Log("i have taken control over M");
                MentalBag.M.GetComponent<Rigidbody>().isKinematic = false; //it follows the hand.
                MentalBag.M.GetComponent<Rigidbody>().useGravity = false;

                MentalBag.M.transform.position += new Vector3(0.0f, 2.0f, 0.0f);

                MyBody.ReachStop();
                MyBody.HeadLookStop();
                    
                    //me.MyPerso.GetResourceByName("captured_meteorite").AmountAvailable = 1.0f;

                } 
            }
        }


    public void playAnimation(string animname) {
       
        Body b = MyBody.GetComponent<Body>();
        b.AnimPlay(animname);



    }
    



    #endregion

}

