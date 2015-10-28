FoscamController
=======================================
http://FoscamController.codeplex.com


Description
-----------

This project is a WPF C# program for displaying MJPEG video stream and controlling a Foscam PTZ IP Camera

based on information and code from this article:
http://blogs.infosupport.com/writing-an-ip-camera-viewer-in-c-5-0


Features
--------

* Motion Control: both MJPEG and HD Foscam camera models

Alternative is to use ONVIF (see port settings in the camera browser-based configuration pages).
Can use OZEKI ONVIF IP Camera SDK for C# (commercial - http://www.camera-sdk.com/p_113-ozeki-camera-sdk-licencing-faq-onvif.html),
or use opensource code from ONVIF Device Manager (ODM - http://sourceforge.net/projects/onvifdm/),
or can develop your own code using Web Services (WSDL/SOAP) using the WSDL definitions available
at  http://http://www.onvif.org/Documents/Specifications.aspx (note that not all cameras support the latest ONVIF spec)


* Video: only MJPEG models currently

for RTSP video can use FFMPEG (like ODM does),
also see another sample using FFMPEG at http://www.codeproject.com/Articles/885869/Stream-Player-control
or use VLC ActiveX control (see  http://miteshsureja.blogspot.gr/2011/11/creating-simple-video-player-using-vlc.html)
or use WPF MediaKit (which uses DirectShow layer - https://github.com/Sascha-L/WPF-MediaKit/wiki)


Change History
--------------

* Newer versions
[George Birbilis / Zoomicon.com]
- see changes at http://foscamcontroller.codeplex.com/SourceControl/list/changesets

* 20130604
[Chris van Beek / InfoSupport.com]
- Original version