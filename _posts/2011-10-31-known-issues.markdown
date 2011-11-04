---
layout : default
title : Known issues
---

__VerificationException -> Operation could destablize the runtime__
	
This happens due to the conflict between NewtonSoft.dll and Intellitrace.Try to add NewtonSoft.dll to intellitrace ignore list.

+ Tools -> Options -> Intellitrace -> Modules
+ Add -> NewtonSoft


