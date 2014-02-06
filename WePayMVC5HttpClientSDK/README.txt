Patrick Jones hiramdeveloper@gmail.com for any questions.

I patterned the design of this SDK after the original (and only) C# SDK that WePay had linked to on their
site https://www.wepay.com/developer/resources/sdks for easy transition. It uses a generic request and response (RequestT and ResponseT)
pattern that is used to invoke a WebClient instance. It was created by Brad Oyler http://bradoyler.com and his GitHub
page https://github.com/bradoyler. He also has an example to go with the SDK he produced.

I am working on MVC-5 example, but for now you can still use Brads' to understand how to set up calls (simple). The logic is basically to 
instantiate a 'XXXRequest' object off the the class you are interested in and submit it using the "Invoke" method on same class.

EXAMPLE - MVC Controller Action

		public async Task<ActionResult> CreateCheckout()
        {
			//new Request object from 'Checkout.cs' class
            CreateCheckoutRequest req = new CreateCheckoutRequest();

			//Properties to pass as Arguemnts (could come from form)
            req.account_id = WePayConfiguration.accountId;								
            req.short_description = "Test Checkout for dev account";					
            req.amount = 13.33M;															
            req.type = "SERVICE";//this has SPECIFIC values check WePay documentation.	

			//Create 'Checkout' instance
            Checkout chk = new Checkout();												

			//pass Request to XXXMethod on insstance that will Invoke
			//HttpClient (WePayHttpClient.cs) and return response
            var response = await chk.CreateCheckoutAsync(req);							

			//Each Response has an added ErrorResponse property
            if (response.ErrorResponse != null)					
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutCreated"] = response;
            return View();
        }


SETUP -
	
		Add the following to MVC Web.Config in the <appSettings> section:

		<!-- ADDED PER 'WEPAY' REQUIREMENTS -->
		<add key="WepayAccessToken" value=XXXXXXXXXX >
		<add key="WepayAccountId" value="XXXXXXXx"/>
		<add key="WepayClientSecret" value="XXXXXXX"/>
		<add key="WepayClientId" value="XXXXXX"/>
		//set to 'true' for Production Mode
		<add key="ProductionMode" value="false" />
		<!-- END 'WEPAY' -->

		In MVC Global.asax add the following inside the Application_Start method (same as Brads')

		WePayConfiguration.accessToken = ConfigurationManager.AppSettings["WepayAccessToken"];
        WePayConfiguration.accountId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayAccountId"]);
        WePayConfiguration.clientId = Convert.ToInt32(ConfigurationManager.AppSettings["WepayClientId"]);
        WePayConfiguration.clientSecret = ConfigurationManager.AppSettings["WepayClientSecret"];
        WePayConfiguration.productionMode = Convert.ToBoolean(ConfigurationManager.AppSettings["ProductionMode"]);

		*********IMPORTANT************

		In Brads exmaple App he places the 'hosturl' property in Glabal.asax file, I opted to create a GlobalVariables.cs
		and used Ninject to inject into a base controller that all my controllers derive. This is in the Example project I 
		am working on, but here is a snippet of one of the controllers:

		namespace WePayMVC5Example.Controllers
		{
			//Base Controller (Shown below)
			public class CheckoutController : BaseController
			{
				public CheckoutController(IGlobalVariables gVars)
					: base(gVars)
				{ }

				public async Task<ActionResult> GetCheckout()
				{
					GetCheckoutRequest req = new GetCheckoutRequest();
					...code not shown....

					ViewData["CheckoutInfo"] = response;
					return View();
				}
			}
		}

		namespace WePayMVC5Example.Controllers
		{
			/// <summary>
			/// Base Controller that all controllers with derive from.
			/// IGlobalVariables is for DI functionality using Ninject.
			/// It injects 'GlobalVariables.cs' located in IoC folder.
			/// </summary>
			public class BaseController : Controller
			{
				protected IGlobalVariables globals;

				public BaseController(IGlobalVariables globalVars)
				{
					globals = globalVars;
				}

				//will execute for every action method making GlobalVariables.cs proppertiues accessible.
				protected override void OnActionExecuting(ActionExecutingContext ctx)
				{
					base.OnActionExecuting(ctx);
					//Sets the hostUrl property on custom GlobalVariables class in MVC App
					globals.hostUrl = Request.Url.Scheme + "://" + Request.Url.Authority;			<------Sets the hosturl property on custom GlobalVariables class
				}
			}
		}