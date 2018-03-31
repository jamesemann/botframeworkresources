# intro-to-botframework-12-fbwebview
This example shows you how to utilise the Facebook Messenger extension to provide a Webview experience in your Bot Framework chatbot.
Used *sparingly and judiciously*, Webview elements can improve interaction while maintaining the 'flow' of the conversation
This is appropriate any time you want to enable rich content which is above and beyond text or card interactions. 
Use cases:
- Displaying interactive maps/visualisations
- Capturing payment
- etc.

## Usage
1. Clone this repo to a directory of your choice `git clone https://github.com/jamesemann/intro-to-botframework-12-fbwebview.git`
2. Change directory `cd intro-to-botframework-12-fbwebview/`
3. Create an azure web app to host your webapp.  Download the publishing profile
	Make a note of:
	- `Web App URL`	
4. Register your bot with dev.botframework.com
   Make a note of:
	- `Bot Id`
	- `App Id`
	- `App Password`
	
	Enter the messaging endpoint : `Web App URL/api/messages`
4. Create a Facebook page/messenger app
   Make a note of:
	- `Page ID`
	- `Page token`
	- `App ID`
	- `App Secret`
5. Configure your bot at dev.botframework.com to distribute your bot on the Facebook Messenger channel.  Use the values collected previously.
6. Open Webviews.sln (in the current folder) and open web.config.  Fill in using values collected in step 2.  Edit the URL on line 36 of MessagesController.cs to point to your URL noted in step 3.
7. Publish it out using the publishing profile downloaded in step 1.
8. Whitelist your WebView https URL using the HTTP client of your choice (Fiddler, CURL, etc.)
```
POST https://graph.facebook.com/v2.6/me/thread_settings?access_token=<accesstoken>
Content-Type: application/json
Host: graph.facebook.com
Content-Length: 161

{
  "setting_type" : "domain_whitelisting",
  "domain_action_type": "add",
  "whitelisted_domains":[
    "https://Web App URL/"
  ]
}
```
8. Test!
