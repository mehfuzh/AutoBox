---
layout : default
title : Gettting Started
---

Lets take that you have a controller and the constructor looks like this :


	public AccountController(IAccountService service)
	{
		this.service = service;
	}

*AccountService* is implemented in MyCoolWebSite.Services folder, in global.ascx you first need to add the following line:

	Container.Init();

However it is possible to specify assembly explictly. For example:

	Container.Init(Resolve.FromCurrentAssembly);

Similarly, you can do:

	Container.Init(Resolve.From(<YOUR_ASSEMBLY>));

Here to mention that AutoBox is a convention based tool, it means IAccountService will search for AccountService class that implements it.

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

This tells AutoBox to cache the result of the call that will automatically invalidate after 10 minutes. However you can explictly specify the method that will invalidate it.

	Container.Setup<ProductRepository>(x=> x.Create(Arg.Varies<Product>()).Invalidates(x => x.GetAllProducts());

_Arg.Varies_ speicifies *Any* arguments. It can be also used for caching , this will act similar to _VaryByParams_.

However to cache an repository call, instead of Arg.Varies if you just want to pass default values and want to specify varible argument setup fluently, you can always do:

	Container.Setup<ProductRepository>(x=> x.GetPrdocutById(0)).Caches(TimeSpan.FromMinutes(10)).VaryByArgs();

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

[http://www.couchbase.com/products-and-services/memcached/](http://www.couchbase.com/products-and-services/memcached/)


Once installed, you can double-click the "Membase Console" from desktop which will take you through an easy wizard to configure memcached.

Moving forward, there is a __AutoBox.Specification__ project. You can use *TestDriven.Net* to verify that all the tests pass and you have configured things correctly.

This is an early preview, therefore there might be glitches and feel free to file them and raise questions.


















