ADXE API Tool
A WebAPI with Swagger UI which can call each ADXE Falcon Service.  POSTS and receives XML, as required by the Falcon Services.

TO DO:
Deserialize the XML Output so it displays correctly.
Create a separate Controller for every service, use Falcon Base URL and add the appropriate .asp file to it when making Falcon calls.
Set the comments path for the Swagger JSON and UI (is it needed)?
Create examples (if not using classes), and a description, for each service call.
May need to add checks for existing CoSec ticket so that a new one isn't created with each call.

Input Example for, Mobile Services, OON Lookup Vehicle:
<Request><Header><Action>OONShopLookupVehicle</Action></Header>
<ServiceInput>
	<CreatedForProfileId>033DCTJQHQ1</CreatedForProfileId>
	<VehId>25 11594P1232AA                               NC                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </VehId>
	<AudaVINUsed></AudaVINUsed>
	<AudaVINGUID></AudaVINGUID>
	<EstID></EstID>
	<HQ_Engine></HQ_Engine>
	<HQ_Transmission></HQ_Transmission>
</ServiceInput></Request>