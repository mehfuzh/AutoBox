---
layout : default
title : Troubleshoot
---

_Configuring Membase server_

Since membase uses *IP_Addr.bat* from %Program_Files%\Membase\Server\bin that dynamically resolves the IP for your machine. In case that you are under a router and your IP is assigned dynamically each time, you can try the following:

Update the follwoing line in *service_register.bat*:

	Set IP_ADDR=127.0.0.1

Once you are done, you can run *service_register.bat*(from command line or just double click it) that will initalize and register the membase server as a local service with your specified IP.

Moreover, you might find that membase server is not starting if you dont have VC++ redist installed. In that regard please check the following thread:

http://couchbase.org/forums/thread/membase-17-error-startup

