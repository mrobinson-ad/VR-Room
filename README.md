# Carcassonne virtual visit
The seventh project in the MakeYourGame online XR App developper course is to make a VR app where you can visit the city of Carcassonne through 360 panoramic photos and videos.

> [!IMPORTANT]
> You can try the app on the oculus quest by downloading the release.


## XR Interaction toolkit
The app heavily relies on the tools provided by the XR interaction toolkit. By using the XR rig we have access to a preconfigured VR camera as well as the controllers. Interactions are easily implemented using XR interactables and their different events.
To change the photos we're viewing we simply change the skybox's material (we can do the same with the videos by setting the output of the video player to a texture to be used in a panoramic material).

# The features of the app

<br>
 
- Easily expandable:
    -
    By using a wrapped list for what constitutes a picture (sphere material, skybox material, location name) we can easily expand or even adjust the order of our visit.

- Wrist menu:
    -
    The watch on the left hand lets us open a menu attached to our wrist where we can pause and play the music, access the videos and return to the starting room. The dev console also displays FPS in real time as well as the currently rendered vertices and triangles.
  
- "Analog" music system:
    -
    In order to showcase sockets and their possible applications, the music system is implemented as a vinyl player where you have to manually switch the vinyls and engage the needle. These rather simple interactions add some engagement and also a way to explore the possibilities of coding with the XR classes.
  

 
