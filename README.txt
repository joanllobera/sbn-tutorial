a demo to use timepath4unity



IDEAL install 
(not working yet, ofr some reason teh SKILLS are forgotten??):
TODO: 
a) make the timepath4unity package without TPAction? would this work?
b) start with an empty project, not with the timepath4unitydemo checkout, 
and import timepath4unitydemo as a package


1) checkout timepath4unity.package 
(or compile it from https://bitbucket.org/joanllobera/timepath4unitycode and export the package)
import the package

2) GDC2013

create new project.
go to asset store, to pick the gdc2013 tutorial https://www.assetstore.unity3d.com/en/#!/content/9896
download it through the asset store, and accept when it requires an upgrade, and import, and accept the upgrade scripts.


open the file Action.cs (eventually using the plugin UnityVS),
 and refactor Action.cs to ActionGDC.cs 
(this is to avoid conflicts with the Action class in ADAPT)
to check if it is done well, open the scene Bearpocalypse.unity, play it, and press forward. 
if the character runs forward, and the character slides under the fence, it is OK.



3) checkout ADAPT software, https://github.com/ashoulson/ADAPT

open a scene ( for example Tutorial4Empty), 
export the package with the whole project, and import it into hte main project.




4) download timepath4unitydemo.

This means:
a) create folder where the demo will be hosted
git init
git pull https://bitbucket.org/joanllobera/timepath4unitydemo


5) open the timepath4unitydemo project

a) import the modified GDC2013
b) import ADAPT
c) import timepath4unity.package




NOTE: in timepath4unity, 
TPAction and TPPerception, which are part of the package, are already existing in the tutorial.
These files are where the action and perception methods are defined, and there are custom definitions for the demo.

Therefore, in the import process they are renamed as
TPACtion 1.cs
TPPerception 1.cs
the simplest is to delete the, and keep the previous files,
 since the tutorial version contains custom method definitions apropriate ofr hte demo.
TODO: THIS IS CORRECTED; RIGHT? RECHECK



NOTE2: 

how to debug the timepath4unitypackage INSIDE the timepath4unitydemo:

a) remove (or rename)  Assets/timepath4unity/timepath4unity.dll
b) copy the folder \code\timepath4unitytest\Assets\timepath4unity\timepath4unity2compile
in Assets/timepath4unity

then, recopy it back, and recompile, and update the git status.
IMPORTANT make sure to NEVER include the timepath4unity2compile folder in the git commit of timepath4unitydemo











