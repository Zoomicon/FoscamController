FoscamController
=======================================
http://FoscamController.codeplex.com


Description
-----------

This project is a WPF C# program for displaying MJPEG video stream and controlling a Foscam PTZ IP Camera


Change History
--------------

* 20151026
[George Birbilis / Zoomicon.com]
- Added IPanTiltController, IVideoController and IFoscamController (this extends the former two ones) interfaces
- FoscamController class now implements IFoscamController interface
- using Camera, Camera.Foscam and Camera.MJPEG namespaces
- reorganized code in folders based on the respective namespaces
- cleaned up the XAML
- added error handling and respective message dialog suggesting to set Camera URL, Username and Password in the code (that is in MainWindow.xaml.cs)

* 20151025
[George Birbilis / Zoomicon.com]
- Centering window on screen
- Cleaned up and refactored XAML and code
- Renamed to FoscamController (since control code is specific to Foscam PTZ IP cameras)
- Using namescape Camera.Foscam
- Uploaded to CodePlex

* 20130604
[Chris van Beek / InfoSupport.com]
- Original version (http://blogs.infosupport.com/writing-an-ip-camera-viewer-in-c-5-0/)