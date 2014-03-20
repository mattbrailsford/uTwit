uTwit_SetValue = function(wrapperId, screenName, token, tokenSecret) {

    var $wrapper = $("#" + wrapperId);
    
    var $screenNameField = $("input[name$=hdnScreenName]", $wrapper);
    var $tokenField = $("input[name$=hdnToken]", $wrapper);
    var $tokenSecretField = $("input[name$=hdnTokenSecret]", $wrapper);
    var $screenNameLabel = $("span[id$=lblScreenName]", $wrapper);
    var $valueWrapper = $("span[id$=lblValueWrapper]", $wrapper);

    $screenNameField.val(screenName);
    $tokenField.val(token);
    $tokenSecretField.val(tokenSecret);

    $screenNameLabel.text("@" + screenName);
    
    $valueWrapper.show();
};

uTwit_ClearValue = function(wrapperId) {

    var $wrapper = $("#" + wrapperId);

    var $screenNameField = $("input[name$=hdnScreenName]", $wrapper);
    var $tokenField = $("input[name$=hdnToken]", $wrapper);
    var $tokenSecretField = $("input[name$=hdnTokenSecret]", $wrapper);
    var $screenNameLabel = $("span[id$=lblScreenName]", $wrapper);
    var $valueWrapper = $("span[id$=lblValueWrapper]", $wrapper);

    $screenNameField.val("");
    $tokenField.val("");
    $tokenSecretField.val("");

    $screenNameLabel.text("");

    $valueWrapper.hide();

};