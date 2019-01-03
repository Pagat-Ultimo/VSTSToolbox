# VSTSToolbox
Collection of funtions which utilizes the VSTS API

# Setup for build
You should add a valid Syncfusion License Key in the App.xaml.cs file.
Syncfuison offers free community license (https://www.syncfusion.com/products/communitylicense)

# Configuration
On the first start there will be an error message that the tool is not proper configured. To configure the application hit the Settings button.

Set the Team foundation service URL to the URL of your Team Foundation Server or DevOps Site.

Create a Personal Access Token on the TFS site.
Insert your Personal Access Token into the Settings of the VSTSToolbox.
Hit Save.
If everything works correctly you should now see that the Organizations, Projects, Repos and Branches comboboxes are filled

# Functions

Commit Summary
Under the Pullrequests Tab you find the function to export every commit for a period of time which are checked if the come from a pullrequest. The exported file is an Excel.


Commit Id
The Id of the commit. Colored red if there is no corresponding pullrequest, green if there is
PullRequest Id
The Id of the corresponding pullrequest
Comment from Code Reviewer
Will be empty after the expert can be used during the manual code review
Commit Link
A Link directly to the commit on the TFS server

