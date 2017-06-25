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
- (missing create service bus queue !!!)
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
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/01_iothubcreation.png" />
### Create EventHub
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/02_eventhubcreation.png" />
### Create StreamAnalytics
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/03_streamanalyticscreation.png" />
### (missing create service bus queue !!!)
### Configure EventHub
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/04_configureEventHubCreation.png" />
### Configure IoTHub EndPoint
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/05_configureIotHubEndpoint01.png" />
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/06_configureEndpointIoTHub.png" />
### Configure IoTHub Routes
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/07_configureIoTHubRoute.png" />
### Configure StreamAnalytics EventHub Input
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/08_configureStreamAnalyticsInput.png" />
### Configure StreamAnalytics PowerBI Output	
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/09_configureoutputpowerbi_1.png" />
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/09_configureoutputpowerbi_2.png" />
### Configure StreamAnalyrics ServiceBus Output
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/10_configureServiceBusOutput.png" />
### Configure StreamAnalytics Query
<img src="https://github.com/GlennColpaert/IoTDemos/blob/master/media/walkthrough/10_configureStreamanalyticsquery.png" />
