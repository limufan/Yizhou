(function($){
    $.widget("ui.userInfoMoidfyDialog", {
            options: {
                
	        },
	        _create: function(){
                var thiz = this;
	            this._form = this.element.form().data("form");
                this._modal = this.element.find(".modal");
                this._actionUrl = $.baseUrl + "Org/MoidfyCurrentUserInfo";
                this.element
                    .find(".btnOk")
                    .click(function(){
                        var formValue = thiz._form.getValue();
                        if (formValue.newPassword != formValue.confirmNewPassword) {
                            alert("两次输入密码不一致, 请重新输入!");
                            return;
                        }
                        $.post(thiz._actionUrl, formValue, function(model){
                            if(model.result){
                                thiz._modal.modal("hide");
                            }
                            else{
                                alert(model.message)
                            }
                        });
                        return false;
                    });
	        },
            modify: function(){
                var thiz = this;
                this._modal.modal("show");
                $.post($.baseUrl + "Org/GetCurrentUserInfo", null, function (model) {
                    if (model.result) {
                        thiz._form.setValue(model.data);
                    }
                    else {
                        alert(model.message)
                    }
                });
            }
        }
    );
})(jQuery);