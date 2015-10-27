FoscamController
=======================================
http://FoscamController.codeplex.com


Description
-----------

This project is a WPF C# program for displaying MJPEG video stream and controlling a Foscam PTZ IP Camera

based on information and code from this article:
http://blogs.infosupport.com/writing-an-ip-camera-viewer-in-c-5-0


note that this is for older MJPEG Foscam cameras, not for their newer HD ones. For those better use ONVIF
(see port settings in the camera browser-based configuration pages). Can use OZEKI ONVIF IP Camera SDK
for C# (commercial -  http://www.camera-sdk.com/p_113-ozeki-camera-sdk-licencing-faq-onvif.html),
or use opensource code from ONVIF Device Manager (ODM - http://sourceforge.net/projects/onvifdm/),
or can develop your own code using Web Services (WSDL/SOAP) using the WSDL definitions available
at  http://http://www.onvif.org/Documents/Specifications.aspx (note that not all cameras support the latest ONVIF spec)

for RTSP video can use FFMPEG (like ODM does), also
see another sample using FFMPEG at http://www.codeproject.com/Articles/885869/Stream-Player-control
or use VLC ActiveX control (see  http://miteshsureja.blogspot.gr/2011/11/creating-simple-video-player-using-vlc.html)
or use WPF MediaKit (which uses DirectShow layer - https://github.com/Sascha-L/WPF-MediaKit/wiki)


Change History
--------------

* 20151027
[George Birbilis / Zoomicon.com]
- Split FoscamController.cs into FoscamMJPEG.cs (for MJPEG video) and FoscamPanTilt.cs (for Pan/Tilt control)
- Updated MainWindow.xaml.cs (demo application) to use IVideoController and IPanTiltController interface variables set to instances of FoscamMJPEG and FoscamPanTilt classes respectively
- Split into separate class library projects

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
- Original version