a demo to use simple behavior networks


NOTE: in timepath4unity, 
TPAction and TPPerception, which are part of the package, are already existing in the tutorial.
These files are where the action and perception methods are defined, and there are custom definitions for the demo.

Therefore, in the import process they are renamed as
TPACtion 1.cs
TPPerception 1.cs
the simplest is to delete the, and keep the previous files,
 since the tutorial version contains custom method definitions apropriate ofr hte demo.
TODO: THIS IS CORRECTED; RIGHT? RECHECK



current API:

            TPAgent me = TP.GetAgent(mindID);
            Body b = me.MyBody.GetComponent<Body>();
            TPMentalBag bag = me.GetComponent<TPMentalBag>();

improved, but still simple API:

me (TPAgent)
me.Body (GameObject, possibly with a Body component from adapt, or an animation controller from mecanim)
me.MentalBag
            
fields that might be null, depending on use:
me.AdaptBody
me.Animator

            
            
OLD-------------------------------------------------------------------
the API should be:

me (TPAgent)
me.Body (Body from ADAPT, if it exists)
me.Animator (Animator from Mecanim, if it exists)
me.MentalBag (TPMentalBag)


flags to know if there is a body or animator set up:
me.adaptBody (bool)
me.mecanimBody (bool)



-the locomotioneditor shuold not give errors
-the body picker should not allow for prefabs, only scene elements.

-defining personalities with prefabs causes trobule when we press play:

Setting the parent of a transform which resides in a prefab is disabled to prevent data corruption.

UnityEngine.Transform:set_parent(Transform)

timepath4unity.TPSkillList:GetSkillContainer(TPPersonality)

timepath4unity.TPPersonality:get_Skills()

timepath4unity.TPPersonality:removeSpacesInNames(TPPersonality)

timepath4unity.TPAgent:CreateMind()

timepath4unity.TPAgent:Start()
