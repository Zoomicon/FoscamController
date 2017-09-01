FoscamController
=======================================
http://github.com/Zoomicon/FoscamController


Description
-----------

This is a Library and Demo WPF program for controlling MJPEG and HD models of Foscam IP PTZ (Pan-Tilt-Zoom) Cameras and displaying MJPEG and H.264/RTSP video


Features
--------

* Motion and Zoom (where available) Control: both MJPEG and HD Foscam camera models

Alternative is to use ONVIF (see port settings in the camera browser-based configuration pages).
Can use OZEKI ONVIF IP Camera SDK for C# (commercial - http://www.camera-sdk.com/p_113-ozeki-camera-sdk-licencing-faq-onvif.html),
or use opensource code from ONVIF Device Manager (ODM - http://sourceforge.net/projects/onvifdm/),
or can develop your own code using Web Services (WSDL/SOAP) using the WSDL definitions available
at  http://http://www.onvif.org/Documents/Specifications.aspx (note that not all cameras support the latest ONVIF spec)


* Video: both MJPEG and HD Foscam (using RTSP+H.264) camera models

Uses xZune.Vlc - https://github.com/higankanshi/xZune.Vlc (via NuGet package - see note below on copying the native VLC libraries from that repository for your executable to find)

MJPEG part is based on information and code from this article:
http://blogs.infosupport.com/writing-an-ip-camera-viewer-in-c-5-0


Usage
-----

Edit MainWindow.xaml.cs:

* find
    //#define USE_FOSCAM_HD_CAMERA
uncomment this to use a Foscam HD Camera model instead of an MJPEG model

* find
    private const string CAMERA_URL = "http://cameraAddressAndPort";
change to IP or DNS name and port (say http://someIPaddress:somePort) for the camera

* find
    private const string USERNAME = "username";
    private const string PASSWORD = "password";
change to camera username and password respectively

* Note:
When using FOSCAM_HD_CAMERA, the VLC libraries have to be placed in a "LibVlc" subfolder, located in the
same folder as the application executable (the bin\Debug folder when using Visual Studio).
Can override that path using an optional parameter passed to FoscamHDVideo class constructor (there is also an
extra optional parameter at that class for VLC options)
That subfolder can be copied from the repository available at http://github.com/birbilis/xZune.Vlc


Change History
--------------

* Newer versions
[George Birbilis / Zoomicon.com]
- see changes at http://foscamcontroller.codeplex.com/SourceControl/list/changesets

* 20130604
[Chris van Beek / InfoSupport.com]
- Original sample app for Foscam MJPEG models
