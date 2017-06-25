# DEVICE TO CLOUD IN A MATTER OF MINUTES


## Description

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/DeviceToCloudInMinutesOverview.png" />


In this lab you’re going to learn how easy it is to connect any device to the cloud in a matter of minutes.

You are going to use a device simulator to send messages to **Azure IoT Hub**. All messages will be directly forwarded to **EventHubs** by adding **Routes** to IoTHub.

A **StreamAnalytics** Job will be created that will stream the output to **PowerBI** to create a realtime dashboard. Next to the PowerBI output we will route certain events (ex: high temperature) towards **Azure ServiceBus**, these events will be picked up by a **Logic App** and posted on a **Slack** channel.


## Pre-Requisites

- Visual Studio
- Azure Subscription
- PowerBI Subscription
- Access to a Slack Channel


## Getting Started

Below you can find a quick list of all the actions that are needed to setup this flow end to end. All these steps will be described below in detail.

- Create Resource Group (Optional)
- Create IoTHub
- Create EventHub
- Create StreamAnalytics
- Create ServiceBus Namespace and Queue
- Configure EventHub
- Configure IoTHub EndPoint
- Configure IoTHub Routes
- Configure StreamAnalytics EventHub Input
- Configure StreamAnalytics PowerBI Output	
- Configure StreamAnalytics ServiceBus Output
- Configure StreamAnalytics Query

### Create Resource Group (Optional)
First step in this tutorial is to create a resource group. Resource Groups are a logical containers that are uses to group related entities.
All resources in a Resource Group should share the same lifecycle. As you deploy, update and delete them togheter. Next to that Resource Groups are used to scope access control to different users.

In our case we will create the **DeviceToCloud** Resource Group. 

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/00_createresourcegroup.png" />

### Create IoTHub
Next step in this walkthrough is to create an **IoTHub** in the Azure Portal. Give it a valid name, choose a pricing tier (Pick Free if it's still applicable for your subscription!). And add it to the Resource Group you created in the previous step. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/01_iothubcreation.png" />

### Create EventHub
When messages arrive in IoTHub we will route them directly to **EventHub**, therefor we need to create an EventHub namespace. In theorie this step is optional as you can directly link your IoTHub to stream analytics. But I've included this in this demo to demonstrate the **routing** functionality of IoTHub further down this excercise. 

To create the **EventHub** give it a valid name, choose the pricing tier and leave all other settings on default. Don't forget to add it to the Resource Group you created in the first step. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/02_eventhubcreation.png" />

### Create StreamAnalytics
Next step is to create a **Stream Analytics Job**. This job will do real-time analytics on our telemetry and write the output to **PowerBI** and to **ServiceBus**.

Give your job an appropriate name and select the correct Resource Group. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/03_streamanalyticscreation.png" />

### Create ServiceBus Namespace and Queue
When a telemetry message hits a certain **temperature threshold** our Stream Analytics job will post a message on a **ServiceBus Queue**. Thefore we are creating the ServiceBus namespace and queue in this step.

Create a namespace, give it a appropriate name and select the correct Resource Group. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/15_sbcreatenamespace.png" />

When the namespace is created add a new **Queue** with the configuration shown below.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/16_sbcreatequeue.png" />

### Configure EventHub
Now that everything is setup we are ready to hook all the components togheter. But let's start first by adding an **EventHub** to our **EventHub Namespace**.
Click on the **EventHub** you created in step 2 and click on **Event Hubs** then select **+Event Hub** and add a new Event hub. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/04_configureEventHubCreation.png" />

### Configure IoTHub EndPoint
Go to your **IoTHub** Endpoint created in Step 1 and choose **Endpoints**, click on **+Add** to add a new Endpoint.
IoTHub uses these endpoints for message routing. 

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/05_configureIotHubEndpoint01.png" />

Add a new **EventHubEndpoint** and choose the **Event Hub** we created in this walkhtrough. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/06_configureEndpointIoTHub.png" />

### Configure IoTHub Routes
Go to your **IoTHub** Endpoint created in Step 1 and choose **Routes**, click on **+Add** to add a new Route.
In this demo we will create a simple Route and just create a **"MatchThemAll"** route, basically we will use IoTHub routing to route all our incomming messages to the configured endpoint. Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/07_configureIoTHubRoute.png" />

### Configure StreamAnalytics EventHub Input
Go to your **Stream Analytics** Endpoint created in Step 4. Go to **Inputs** and add a new Input by choosing **+Add**. 
We will use the **EventHub** created and configured in the steps above as Input for our Stream Analytics Job.
Below is an example of what your configuration should look like.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/08_configureStreamAnalyticsInput.png" />

### Configure StreamAnalytics PowerBI Output	
Go to your **Stream Analytics** Endpoint created in Step 4. Go to **Outputs** and add a new Output by choosing **+Add**. 
In this step we will add a **PowerBI** output. Select **Power BI** as **Sink** and then click **Authorize**, once authorized use the configuration as shown below.
This step will create a new **StreamingDataSet** to your PowerBI subscription.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/09_configureoutputpowerbi_1.png" />
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/09_configureoutputpowerbi_2.png" />

### Configure StreamAnalyrics ServiceBus Output
Go to your **Stream Analytics** Endpoint created in Step 4. Go to **Outputs** and add a new Output by choosing **+Add**. 
In this step we will add a **ServiceBus** output.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/10_configureServiceBusOutput.png" />

### Configure StreamAnalytics Query
Go to your **Stream Analytics** Endpoint created in Step 4. Go to **Query** and add the query as shown below.
This first part of the query will **stream** all the data to **PowerBI**. The second part of the query will add a message on the **ServiceBus Queue** when the **Temperature** is higher than 25.

<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/10_configureStreamanalyticsquery.png" />
