angular.module("umbraco").controller("Our.Umbraco.uTwit.PropertyEditors.TwitterAuthorizerController",
    function ($scope, editorState)
    {
        function encodeRequestOptions(requestOptions)
        {
            return JSON.stringify(requestOptions).webSafeBase64Encode();
        }

        $scope.hovering = false;
        $scope.hover = function (hovering) {
            $scope.hovering = hovering;
        };

        $scope.authorize = function () {
            var requestOptions = {
                ContentTypeAlias: editorState.current.contentTypeAlias, 
                PropertyAlias: $scope.model.alias,
                ScopeId: $scope.$id,
                Callback: "authorizeCallback"
            };
            window.open('/App_Plugins/utwit/twitteroauth1callback.aspx?o=' + encodeRequestOptions(requestOptions), 'Authorize', 'scrollbars=no,resizable=yes,menubar=no,width=800,height=600');
        };
        $scope.authorizeCallback = function (data) {
            $scope.model.value = $.extend({},
                data,
                $scope.model.config);
        };

        $scope.remove = function () {
            $scope.model.value = undefined;
        };
    });

/* Helpers */
String.prototype.webSafeBase64Encode = function() {
    return this.base64Encode().replace("+", "-").replace("/", "_").replace("=", "*");
}