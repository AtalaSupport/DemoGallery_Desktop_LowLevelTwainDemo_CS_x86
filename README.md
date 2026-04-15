# LowLevel TWAIN Demo

This demo is a sort of 'deconstruction of scanning using DotTwain'.  It isn't really meant to provide an example of making user-oriented DotTwain scanning apps (for that, see [Advanced Scan to File Demo](https://github.com/AtalaSupport/DemoGallery_Desktop_AdvancedScanToFileDemo_CS_x86) and/or [Twain Acquisition Demo[(https://github.com/AtalaSupport/DemoGallery_Desktop_TwainAcquisitionDemo_CS_x86)).  Instead, this demo provides direct access to the various stages/steps within the process of scanning.

Unlike the other aforementioned demos which use our Acquisition object, this uses our TwainController class to provide the lower-level access to TWAIN.  You Open the Source Manager, Select your source, Open communication with the source get/set capabilities, then perform the acquisition.  It populates the get/set capabilities with values obtained from querying the current device.

It can also be a very useful diagnostic tool to show what various capability settings are available, and to see how the TWAIN communication process is working between a given scanner and the TWAIN controller.
           

This is the C# version. We also have a [VB.NET version](https://github.com/AtalaSupport/DemoGallery_Desktop_LowLevelTwainDemo_VB_x86) available.  

NOTE that there is no x64 version. This is due to most TWAIN drivers being native 32 bit.

Please see [FAQ: Common Problems with x64 TWAIN Scanning](https://www.atalasoft.com/kb2/KB/50140/FAQ-Common-Problems-with-x64-TWAIN-Scanning)

## Prerequisites
This demo assumes you have the Atalasoft DotImage SDK installed, but you only need
licsning for DotTwain (Or DOtImage Document Imaging)

You may also request a 30 day evaluation when installing / activating.

[Download DotImage](https://www.atalasoft.com/BeginDownload/DotImageDownloadPage)

## Cloning
We recommend the following to ensure you clone with the required submodule

Example: git for windows
```bash
git clone https://github.com/AtalaSupport/DemoGallery_Desktop_LowLevelTwainDemo_CS_x86.git LowLevelTwainDemo
```


## Related documentation
In addition to this README, the Atalasoft documentation set includes the following:  
- [AtalaSupport Github](https://github.com/AtalaSupport/) For an extensive set of sample apps.  
- [Atalasoft's APIs & Developer Guides page](https://www.atalasoft.com/Support/APIs-Dev-Guides) for our Developers guide and API references.  
- [Atalasoft Support](http://www.atalasoft.com/support/) for our main support portal.
- [Atalasoft Knowledgebase](http://www.atalasoft.com/kb2) where you can find answers to common questions / issues.  


## Getting Help for Atalasoft products
Atalasoft regularly updates our support [Knowledgebase](http://www.atalasoft.com/kb2) with the latest information about our products. To access some resources, you must have a valid Support Agreement with an authorized Atalasoft Reseller/Partner or with Atalasoft directly. Use the tools that Atalasoft provides for researching and identifying issues. 

Customers with an active evaluation, or those with active support / maintenance may [create a support case](https://www.atalasoft.com/Support/my-portal/Cases/Create-Case) 24/7, or call in to support ([+1 949 236-6510](tel:19492366510) ) during our normal support hours (Monday - Friday 8:00am to 5:00PM Eastern (New York) time).  

Customers who are unable to create a case or call in may [email our Sales Team](email:sales@atalasoft.com).  
