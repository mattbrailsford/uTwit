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
      uTwit - Tweet List
      *****************************************************************

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

  <xsl:template name="RenderTweets">
    <xsl:param name="tweets" />

    <div class="uTwit tweets">
      <xsl:for-each select="$tweets/status">

        <!--
            Choose the tweet to display.
            If it's a retweet, show the original but add "Retweeted by" at the end,
            otherwise just show the current tweet.
        -->
        <xsl:variable name="tweetToDisplayXml">
          <xsl:choose>
            <xsl:when test="string(isretweet) = 'true'">
              <xsl:copy-of select="./retweetedstatus"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:copy-of select="." />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:variable>

        <xsl:variable name="tweetToDisplay" select="msxml:node-set($tweetToDisplayXml)/*[1]" />

        <div class="tweet">
          <a href="{$tweetToDisplay/permalinkurl}">
            <img src="{$tweetToDisplay/user/profileimageurl}" alt="{$tweetToDisplay/user/name}" class="avatar" />
          </a>
          <h3>
            <a href="{$tweetToDisplay/permalinkurl}">
              <xsl:value-of select="$tweetToDisplay/user/name"/>
              <xsl:text> </xsl:text>
              <em>
                @<xsl:value-of select="$tweetToDisplay/user/screenname"/>
              </em>
            </a>
          </h3>
          <a href="{$tweetToDisplay/permalinkurl}" class="timestamp">
            <xsl:value-of select="uTwit:FormatDate($tweetToDisplay/createdat)"/>
          </a>
          <p class="text">
            <xsl:value-of select="$tweetToDisplay/linkifiedtext" disable-output-escaping="yes"/>
          </p>
          <xsl:if test="string(isretweet) = 'true'">
            <p class="retweet">
              <a href="{./user/permalinkurl}">
                Retweeted by @<xsl:value-of select="./user/screenname"/>
              </a>
            </p>
          </xsl:if>
          <div class="actions">
            <a href="{concat('https://twitter.com/intent/tweet?in_reply_to=', ./id)}" class="reply">Reply</a>
            <a href="{concat('https://twitter.com/intent/retweet?tweet_id=', ./id)}" class="retweet">Retweet</a>
            <a href="{concat('https://twitter.com/intent/favorite?tweet_id=', ./id)}" class="favorite">Favorite</a>
          </div>
        </div>
      </xsl:for-each>
    </div>

  </xsl:template>

</xsl:stylesheet>
