
jQuery.extend({
    resolveUrl: function(path, param){
        var query = "";
        if(param){
            query = $.param(param);
        }
        if(path.indexOf("?") > -1){
            return $.baseUrl + path + query;
        }
        else{
            return $.baseUrl + path + "?" + query;
        }
    },
    setRequired: function(input, required){
        input.element
            .closest(".form-group")
            .find(".control-label span").remove();
        if (required) {
            input.element
                .closest(".form-group")
                .find(".control-label")
                .prepend("<span class='requiredSign'>*</span>");
        }
        input.setRequired(required);
    }
});
$.widget(
    "ui.ajaxProgress", {
        options: {
        },
        _create: function(){
            var _this = this;
            var self = this.element;
            self.hide();
            
            this.element.ajaxComplete(function(event,request, settings){
                $(this).hide();
            });
            
            this.element.ajaxSend(function(event,request, settings){
                _this.show();
            });
        },
        show: function(){
            this.element.css({ top: 0, left: 0 }).show();
            
			this.element.position({
			    my: "center center",
			    at: "center center",
				of: window,
				offset: "0 0",
				collision: 'fit'
			});
        }
    }
);

$.ajaxSetup ({
    cache: false
});

$(function () {
    $("<div class='loading'></div>").appendTo($("body")).ajaxProgress();
});