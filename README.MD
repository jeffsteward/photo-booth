# Photo Booth

June 2009

The whole system is mostly cobbled together from spare parts. 

### Hardware
- old laptop with at least two USB ports; screen removed 
- 17" LCD monitor with the stand removed
- camera is an Olympus C-5060
- Arduino microcontroller (switches are wired to this then the Arduino sends commands to the computer)
- various switches and pedals (for triggering the software/camera)
- hard shell suitcase
- mount from a really cheap table top tripod
- 25' of black fabric 
- 20' of PVC pipe
- various PVC pipe fittings

### Software
- Windows 2000
- Photobooth (custom VB.NET application; source at http://www.assembla.com/wiki/show/photobooth)
- Cam2Com

A very high level view of the system is this: A button is pressed which sends a command to the Photobooth software. The software interprets the command and sends another command to Cam2Com. Cam2Com triggers the camera. The camera takes a picture. Cam2Com downloads the photo from the camera to the computer. The Photobooth software detects the new photo then displays it on the screen.

1. button is pushed
2. serial command is received by the Photobooth software
3. screen is cleared
4. "Get Ready" message is displayed
5. Photobooth invokes Cam2Com
6. Cam2Com triggers the camera
7. camera takes the picture
8. Cam2Com downloads the image from the camera to a specified folder
9. Photobooth watches for a new image file to appear in the specified folder
10. Photobooth loads the image for display
11. Photobooth creates a cached thumbnail version
12. the system is put back in a ready state

All of the source code for Photobooth and for the Arduino microcontroller is available here http://www.assembla.com/wiki/show/photobooth. The Cam2Com software I had to buy. The reason for that is because I'm using the older Olympus C-5060 camera which doesn't support newer computer-to-camera protocols. There is an alternate version of Photobooth (Photobooh_WIA) that does not require Cam2Com but it does require a camera that can operate in PTP mode and Windows XP or higher. I've used it quite successfully with a Nikon D40. The software the principles are the same as teh non-WIA version. The nice thing is that Photobooth_WIA is capable of triggering the camera and downloading the photos so you can cut out the Cam2Com layer.

As for the physical setup I just got an old hard shell suitcase from the Goodwill. I mounted the monitor in the lid and used dense foam to make a cradle for the laptop in the bottom half of the suitcase. There are a few photos of the setup here http://cardboardrobotics.com/photobooth.asp. And a few additional photos of its various iterations here http://www.flickr.com/photos/robotsareeverywhere/sets/72157605111189961/.

I looked at the software alternative gPhoto which looks great but only runs on unix/linux operating systems. You could probably do something much simpler on a Mac. It comes down to how clean you want the interface to be. I wanted to strip it down to just the photo and a status message and the quickest way for me to do that was writing the Photobooth application. 

I intend to post a wiring diagram for the Arduino and switches/pedals in the near future.
