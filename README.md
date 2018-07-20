#Example API Tool
A WebAPI with Swagger UI which can call each ADXE Falcon Service.  POSTS and receives XML, as required by the Falcon Services.

TO DO:

1. Deserialize the XML Output so it displays correctly.
2. Create a separate Controller for every service, use Falcon Base URL and add the appropriate .asp file to it when making Falcon calls.
3. Set the comments path for the Swagger JSON and UI (is it needed)?
4. Create examples (if not using classes), and a description, for each service call.
5. May need to add checks for existing CoSec ticket so that a new one isn't created with each call.