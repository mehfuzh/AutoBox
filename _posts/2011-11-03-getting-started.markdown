---
layout : default
title : Gettting Started
---

## What is Memcached?

Free & open source, high-performance, distributed memory object caching system, generic in nature, but intended for use in speeding up dynamic web applications by alleviating database load.

Memcached is an in-memory key-value store for small chunks of arbitrary data (strings, objects) from results of database calls, API calls, or page rendering.

you will find more information on this at : http://www.memcached.org/


## Getting Started

Lets take that you have a controller and the constructor looks like this :


	public AccountCOntroller(IAccountService service)
	{
		this.service = service;
	}

*AccountService* is implemented in MyCoolWebSite.Services folder,  in the global.ascx you first need to add the following line:

	Container.Init();



Next you need to override the __GetControllerInstance__ from DefaultControllerFactory. Since AutoBox is implemented using __CommonServiceLocator__ you can directly include _ServiceLocator.Current.GetInstance_ that will return the target controller with depencies properly injected.

	protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
	{
            return ServiceLocator.Current.GetInstance(controllerType) as IController;
	}


And at the end of the Global.ascx.cs you need to write this line, so that the request goes through _AutoBox_:

	ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());


Now, moving forward there is a method in IProuductRepository lets say IProductReposiroty.GetAllProducts();

You just dont want to hit database all the time, unless invalidated. Therefore, you can further specify:

	Container.Setup<ProductRepository>(x=> x.GetAllProducts()).Caches(TimeSpan.FromMinutes(10));

This lets AutoBox to store the data to memcache that will automatically invalidate after 10 minutes. However you can explictly specify the method that will invalidate it.

	Container.Setup<ProductRepository>(x=> x.Create(Arg.Varies<Product>()).Invalidates(x => x.GetAllProducts());

_Arg.Varies_ speicifies *Any* arguments. It can be also used for caching , this will act similar to _VaryByParams_.

Finally to make *Memcached* working you will need to have the following block in web.config (will be added automatically when installed from NUGet).


	<?xml version="1.0" encoding="utf-8"?>
	<configuration>

 		<configSections>
    			<section name="autoBox" type="AutoBox.AutoBoxSection, AutoBox" />
  		</configSections>

  		<autoBox cacheStore="localhost" cacheProvider="MemcachedProvider" />

	</configuration>


Here you will notice that by default its pointing to *localhost* and *MemcachedProvider*. However it can be an external IP pointing to __Amazon ElastiCache__(released a few weeks ago).Here to note that memcached is a caching machanism and therefore the tool is agnostic of underlying cloud service that you may use.

To test caching locally, you can use the  _CouchBase Membase server_ (Not limited to), it gives you a nice web based GUI to monitor the cache usage, configure memory and clusters.

You can download it from this link:

http://www.couchbase.com/products-and-services/memcached

While under a router , i faced an issue in getting an IP, by default it useses IP_Addr.bat from %Program_Files%\Membase\Server\bin

But rather depending on it to resolve your IP which in my case my didnt work since my ip is dynamically assigned by router, you can update the follwoing line in *service_register.bat*:

	Set IP_ADDR=127.0.0.1

Once you are done, you can run service_register.bat(from command line or just double click it) that will initalize and register the membase server as a local service.

Finally, you can double-click the "Membase Console" from desktop which will take you through an easy wizard.


Moving forward, there is a __AutoBox.Specification__ project. You can use *TestDriven.Net* to verify that all the tests pass and you have configured things correctly.

This is an early preview, therefore there might be glitches and feel free to file them and raise questions.


Regards,

Mehfuz

















