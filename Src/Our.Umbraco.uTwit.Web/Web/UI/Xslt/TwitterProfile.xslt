<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [
  <!ENTITY nbsp "&#x00A0;">
]>
<xsl:stylesheet
  version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxml="urn:schemas-microsoft-com:xslt"
  xmlns:umbraco.library="urn:umbraco.library"
  xmlns:uTwit="urn:uTwit"
  exclude-result-prefixes="msxml umbraco.library uTwit">

  <xsl:output method="html" omit-xml-declaration="yes" />

  <xsl:param name="currentPage"/>

  <!-- 
      *****************************************************************
      uTwit - Twitter Profile
      *****************************************************************
  -->

  <xsl:param name="twitterOAuthPropertyAlias" select="'twitter'" />

  <!--
      !IMPORTANT!
      The display of twitter timelines is strictly governed by the 
      Developer Display Requirements as defined on the twitter website.
      https://dev.twitter.com/terms/display-requirements
      It is your responsibility to ensure that these requirements are met.
      Failing to meet these requirements could result in the uTwit app
      being banned. If you do not intend to follow these requirements
      you do so at your own risk, and we ask that you do so using your
      own app definition. To create your own twitter app, login to 
      http://dev.twitter.com and go to "My Application". From there, click
      "Create a new Application" and follow the on screen instructions
      (Set access to Read and be sure to set a callback url. It doesn't 
      have to be valid, we just found one had to be set for it to work). 
      Once created simply copy your Consumer Key and Consumer Secret and 
      paste them into fields on the prevalue editor for the Twitter OAuth
      data type.
  -->

  <xsl:template match="/">
    <xsl:variable name="config" select="$currentPage/*[local-name() = $twitterOAuthPropertyAlias]/uTwit" />
    <xsl:variable name="user" select="uTwit:GetUser($config/OAuthToken, $config/OAuthTokenSecret, $config/ConsumerKey, $config/ConsumerSecret, $config/ScreenName)" />

    <div class="uTwit profile">
      <a href="{$user/permalinkurl}">
        <img src="{$user/profileimageurl}" alt="{$user/name}" class="avatar" />
      </a>
      <h3>
        <a href="{$user/permalinkurl}">
          <xsl:value-of select="$user/name"/> <em>@<xsl:value-of select="$user/screenname"/></em>
        </a>
      </h3>
      <p class="stats">Tweets: <xsl:value-of select="$user/statusescount"/> | Following: <xsl:value-of select="$user/friendscount"/> | Followers: <xsl:value-of select="$user/followerscount"/></p>
    </div>

  </xsl:template>

</xsl:stylesheet>
