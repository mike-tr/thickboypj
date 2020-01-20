# stickman_new
Source code, and my stickman project from my youtube channel.

Tutorials:
https://www.youtube.com/playlist?list=PLa8x6KWHarJdjv5YuqLUS2THURdVNCMPk

All the scrips. i made in the tutorials.
https://github.com/miko-t/stickman_new/blob/master/Assets/Scripts/Tutorials


The most important Scirpt would be " the limbic system", with is that : 

https://github.com/miko-t/stickman_new/blob/master/Assets/Scripts/Tutorials/Limb.cs,

https://github.com/miko-t/stickman_new/blob/master/Assets/Scripts/Tutorials/LimbPart.cs


There is clearly more then one way you can go about creating a stickman,
the point being i wanted to make a "limbic" system. where you can set, the desired,
position for each limb/part of the body, and not worry much about how it is done.
while making it good enough so by rotations only it would be able to counter gravity,
while no applying any unnesecarry force to the rigidbodies ( non rotation force ).

making this allow me to controll the stickman, body parts. (legs/arms) 
( it can be even a huge snake or something, not necessarily, a stickman )
( you can add tail or something or wings whatever as long as you have a position it needs to be at relative to the body ).
while i can controll body parts easilly by setting the "Rest position" 
or using "SetPosition method" - that overrides rest position for one frame.
and the body part would follow that position.

i plan to add IK system so i can break one limb into several parts and each one can follow different positions.
( to be made, should be easilly done tho ).


Anyway: the point being with the IK system, ill be able to create animations,
that are fluid and are programmatically made, so are physic based.
the only challage i have is

When is the animation finished? like if i wanna make an attack animation i would set the Arm to go to Possition B
but because it animation based it wont necesarrily do it in the same time each time.
It can be stuck at the ground, it can just do it wrong, it can have multiple problems.

so making animations this way have their own set of problems.
and here comes The Unity engine Animator, with i can speciffy a "path" for the hand to take, to attack,
and mayble cancle the attack if it hits object hard in the way.

Anyway i hope ill continue with the series, i also plan to learn more machine learning.


