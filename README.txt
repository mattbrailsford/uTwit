Description:
uTwit is a combination of two packages to allow you to easily embedd twitter statuses on your website. 

uTwit.DataType
The uTwit.DataType package contains a Twitter OAuth datatype to allow you to easily obtain the OAuth tokens required to talk to the Twitter API.

uTwit.Web
The uTwit.Web package contains some helper methods / example code for rendering out Twitter elememts to the page.
How to use:

You'll want to start by installing the data type package first. Once you install it, you should have available a new Data Type definition named Twitter OAuth. Simply add this as a new property to your doc type and hit the Authorize link to obtain a Twitter OAuth token.

Once you have a property setup and configured, you'll want to go ahead and install the web package. Once this is installed, you should have available to you some new XSLT Macros and MVC Partials (Depending on your prefered language, you can just delet the ones you don't use). Simply drop them on the page, and configure the variables within them and you should have tweets displaying on your website.

In addition to the Macros and Partials, the web project will also install some helper methods and xslt extensions as follows:

Helpers
    uTwit.DeserializeValue(...)
    Deserializes a JSON property value, to a TwitterOAuthDataValue entity

    uTwit.GetLatestTweets(...)
    Gets a list of latest tweets. Various overloads for confguring the user / number of tweets are also available.

    uTwit.SearchTweets(...)
    Searches tweets for the passed in query. Various overloads for configuraing the query term / number of tweets are also available.

    uTwit.GetUser(...)
    Gets the user info for a given screen name.

    uTwit.GetUsers(...)
    Gets the user info for a given list of screen names.

    uTwit.FormatDate(...)
    Formats a DataTime object into the official Twitter date format.

XSLT Extensions
    uTwit:GetLatestTweets(...)
    Gets a list of latest tweets. Various overloads for confguring the user / number of tweets are also available.

    uTwit:SearchTweets(...)
    Searches tweets for the passed in query. Various overloads for configuraing the query term / number of tweets are also available.

    uTwit:GetUser(...)
    Gets the user info for a given screen name.

    uTwit:GetUsers(...)
    Gets the user info for a given list of screen names.

    uTwit:FormatDate(...)
    Formats a DataTime object into the official Twitter date format.

IMPORTANT!
By default uTwit will install with a demo consumer key / secret to allow you to test it's functionality. It is strongly encouraged that for production use, you go to http://dev.twitter.com and setup your own app and obtain your own consumer key / secrets. You can then enter these into the prevalue editor for the datatype.

Don't forgot, you'll need to re-authorize any currently configured properties with your new keys.

IMPORTANT!
When displaying tweets on your website, it is your responsibility to abide by the display requirements as defined on the twitter website (https://dev.twitter.com/terms/display-requirements). Failing to meet these requirements could result in your app being banned.