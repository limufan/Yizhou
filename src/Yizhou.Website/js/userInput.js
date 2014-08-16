(function($){
    $.widget("ui.userInput", $.webui.input, {
            options: {
                required: false,
                name: null,
                single: null,
                defaultValue: null
	        },
	        _onCreated: function(){
                var thiz = this;
                if(this.options.name){
                    this.element.attr("name", this.options.name);
                }
                else{
                    this.options.name = this.element.attr("name");
                }
                this._btnSelect = this.element.find(".btnSelect");
                this._txtName = this.element.find("input");
                this._btnSelect.click(function(){
                    $.chengyuanDialog.chengyuanDialog("xuanze", {
                        yonghu: true,
                        xuanzehou: function(args){
                            var yonghu = args.yonghuList[0];
                            thiz._txtName.val(yonghu.name);
                            thiz._user = {};
                            thiz._user.name = yonghu.name;
                            thiz._user.account = yonghu.account;
                            return true;
                        }             
                    });
                    return false;
                });
                if(this.options.defaultValue){
                    this.setValue(this.options.defaultValue);
                }
	        },
            _setError: function(){
                this.element.focus();
                this.element.closest('.form-group').addClass('has-error');
                this.element.next('.help-block').show();
            },
            validate: function(){
                var value = this._user;
                if(this.options.required){
                    if(!value){
                        this._setError();
                        return false;
                    }
                    else{
                        this.element.closest('.form-group').removeClass('has-error');
                        this.element.next('.help-block').hide();
                    }
                }
                return true;
            },
            getValue: function(){
                return this._user;
            },
            setValue: function(value){
                this._user = value;
                var text = "";
                if(value){
                    text = value.name;
                }
                this.element.find("input").val(text);
                this._textElement.html(text);
            }
        }
    );
})(jQuery);