# AutoBox - On the fly Dependency Injection and caching container
==================================================================


The tool dynamically[without you to manually set the dependency graph] injects dependencies based on convention. Also, let you cache  repository calls using memcached(the most popular cross-platform caching system). 


## Getting Started

Let us assume that you have a controller and the constructor looks like this:


	public AccountController(IAccountService service)
	{
		this.service = service;
	}

Here *AccountService* is implemented in MyCoolWebSite.Services namespace, in global.ascx you first need to add the following line:

	Container.Init();

However it is possible to specify assembly explictly. For example:

	Container.Init(Resolve.FromCurrentAssembly);

Similarly you can do:

	Container.Init(Resolve.From(<YOUR_ASSEMBLY>));

Here to mention that AutoBox will do the mapping for a service type (interface) on demand thus making it lazy.

Next you need to create a controller factory from *DefaultControllerFactory* that overrides __GetControllerInstance__. Since AutoBox is implemented using __CommonServiceLocator__ you can directly include _ServiceLocator.Current.GetInstance_ that will return the target controller with depencies properly injected.

	protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
	{
            return ServiceLocator.Current.GetInstance(controllerType) as IController;
	}


Finally, set your controller factory as current with the following line:

	ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());


Now, moving forward there is a method in IProductRepository lets say _IProductReposiroty.GetAllProducts()_. You just dont want to hit database all the time, unless invalidated. Therefore you can further specify:

	Container.Setup<ProductRepository>(x=> x.GetAllProducts()).Caches(TimeSpan.FromMinutes(10));

This tells AutoBox to cache the result of the call that will automatically invalidate after ten minutes. Moreover you can explictly specify the method that will invalidate it:

	Container.Setup<ProductRepository>(x=> x.Create(Arg.Varies<Product>()).Invalidates(x => x.GetAllProducts());

_Arg.Varies_ specifies that the repository call will be cached/invalidated for variable/dynamic arguments, this is very much to the asp.net output cache _VaryByParams_ option.However to cache an repository call, instead of _Arg.Varies_ if you just want to pass default values and want to specify varible arguments option fluently, you can always do:

	Container.Setup<ProductRepository>(x=> x.GetPrdocutById(0)).Caches(TimeSpan.FromMinutes(10)).VaryByArgs();

As stated above by default, caching is done using memcached therefore to make *memcached* working you will need to include the following block in web.config (will be added automatically when installed from nuget).


	<?xml version="1.0" encoding="utf-8"?>
	<configuration>

 		<configSections>
    			<section name="autoBox" type="AutoBox.AutoBoxSection, AutoBox" />
  		</configSections>

  		<autoBox cacheStore="localhost" cacheProvider="MemcachedProvider" />

	</configuration>


Here you will notice that by default its pointing to *localhost*. However it can be an external IP pointing to __Amazon ElastiCache__(released a few weeks ago).Therefore its safe to say that memcached is a caching machanism and the tool is agnostic of any underlying cloud service that you may choose.

To test caching locally, you can use the  _CouchBase Membase server_ (Not limited to), it gives you a nice web based GUI to monitor the cache usage, configure memory and add clusters.

You can download it from this link:

http://www.couchbase.com/products-and-services/memcached/ 

Once installed, double-click the "Membase Console" from desktop which will take you through an easy wizard to configure memcached.

Moving forward, there is a __AutoBox.Specification__ project. You can use *TestDriven.Net* to verify that all tests pass and you have filled the gaps correctly.Lastly, to raise any question or file issues, please use the github project url (can be reached using the fork link above(top-right)).

