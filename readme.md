# AutoBox - Auto Dependency Injection and caching container
================================================================


The tool dynamically injects dependencies based on convention (IAccountService to AccountService). Also, let you cache  repository calls to memcached (the most poular cross-plaform caching tool). 



## What is Memcached?

Free & open source, high-performance, distributed memory object caching system, generic in nature, but intended for use in speeding up dynamic web applications by alleviating database load.

Memcached is an in-memory key-value store for small chunks of arbitrary data (strings, objects) from results of database calls, API calls, or page rendering.

you will find more information on this at : http://www.memcached.org/


## Getting Started

Lets take that you have a controller like this :


	public AccountCOntroller(IAccountService service)
	{
		this.service = service;
	}

*AccountService* is implemented in MyCoolWebSite.Services folder,  in the global.ascx you just need to type the following line:

	AutoBox.Init();

Next you need to override the __GetControllerInstance__ from DefaultControllerFactory. Since AutoBox is implemented using __CommonServiceLocator__ you just need to have the following
code block:

   	protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return ServiceLocator.Current.GetInstance(controllerType) as IController;
        }


And at the end of the Global.ascx.cs you need to write this line, so that the request goes through our factory:

	ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());


Now, moving forward there is a method in IProuductRepository lets say IProductReposiroty.GetAllProducts();

You just dont want to hit database all the time, unless invalidated. Therefore, you can further specify:

	AutoBox.Setup<ProductRepository>(x=> x.GetAllProducts()).Caches(TimeSpan.FromMinutes(10));

Here you can extend it to the method that will invalidate it.

	AutoBox.Setup<ProductRepository>(x=> x.Create(Arg.Varies<Product>()).Invalidates(x => x.GetAllProducts());

*Arg.Varies* speicifies  Any arguments. It can be also used for caching , this will act similar to varybyparams.

Finally to make *Memcached* working you will need to have the following block in web.config (will be added automatically when installed from NUGet).


	<?xml version="1.0" encoding="utf-8"?>
	<configuration>

 		<configSections>
    			<section name="autoBox" type="AutoBox.AutoBoxSection, AutoBox" />
  		</configSections>

  		<autoBox cacheStore="localhost" cacheProvider="MemcachedProvider" />

	</configuration>


Here you notice that by default its pointing to *localhost* and *MemCacheProvider*. However it can be an external IP pointing to __Amazon ElastiCache__(released a few weeks ago). Howeever MemCached  is a service and the tool uses default memcache protocol so even if *windows azure* has surpports memcache , you will just need to specify the endpoint and you are good to go.

To test caching locally, you can use the  *CouchBase Memcached server*, it gives you a nice web based GUI to monitor the cache usage, configure memory and clusters.

You can download it from this link:

http://www.couchbase.com/products-and-services/memcached

While under a router , i faced an issue in assigning a loopback address, by default it useses IP_Addr.bat from C:\Program Files\Membase\Server\bin

But rather depending on it to resolve your IP which in my case my didnt work since my ip is dynamically assigned by router, you can update the follwoing line in service_register.bat:

	Set IP_ADDR=127.0.0.1

Once your are set, you can run service_register.bat(from command like or just double click it) that will initalize and register the server as a local service.

Finally, you can double click the "Membase Console" from desktop and configure cluster, memory, etc.


Moving forward, there is __AutoBox.Specification__ project. You can use TestDriven.Net to see that all the tests pass and you have configured things correctly.

This is an early preview , so i will add more features overtime based on feedback.


Regards,

Mehfuz

















