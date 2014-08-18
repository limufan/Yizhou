(function($){
    $.widget("ui.dingdanDetailsForm", {
            options: {
                required: false,
                name: null,
                single: null,
                defaultValue: null
	        },
            _create: function(){
                var thiz = this;
                this.element.horizontalForm();
	            this._form = this.element.form().data("form");
                this._kehuInput = this._form.getInput("kehu");
                this._chanpinGrid = $("#chanpinGrid").datagrid({
                    columns:[
			            {title: "产品名称", width: 80, field:"chanpin", render: "name"},
			            {title: "规格", width: 80, field:"guige"},
			            {title: "单位", width: 80, field:"danwei"},
			            {title: "数量", width: 80, field:"shuliang"},
			            {title: "桶数", width: 80, field:"tongshu"},
			            {title: "销售单价", width: 80, field:"xiaoshouDanjia"},
			            {title: "实际单价", width: 80, field:"shijiDanjia"},
			            {title: "销售底价", width: 80, field:"xiaoshouDijia"},
			            {title: "总金额", width: 80, field:"zongjine"},
			            {title: "业务率", width: 80, field:"yewulv"},
			            {title: "业务率方式", width: 80, field:"yewulvFangshi"},
			            {title: "业务费", width: 80, field:"yewufei"},
			            {title: "是否开票", width: 80, field:"shifouKaipiao", render: "shifou"}
                    ],
                    required: true
                }).data("datagrid");
                this._form.setInput("mingxiList", this._chanpinGrid);
                this._shoukuanGrid = $("#shoukuanGrid").datagrid({
                    columns:[
			            {title: "收款日期", width: 80, field:"shoukuanRiqi", render: "date"},
			            {title: "收款金额", width: 80, field:"shoukuanJine"},
			            {title: "提成", width: 80, field:"ticheng"}
                    ]
                }).data("datagrid");
                this._form.setInput("shoukuanList", this._shoukuanGrid);
                this._chanpinDetailsModal = $("#chanpinDetailsModal").chanpinDetailsModal().data("chanpinDetailsModal");
                $("#btnAddChanpin").click(function(){
                    thiz._chanpinDetailsModal.add(function(value){
                        thiz._chanpinGrid.appendRow(value);
                    });
                    return false;
                });
                $("#btnEditChanpin").click(function(){
                    var selectedRow = thiz._chanpinGrid.getSelectedRow();
                    var rowValue = selectedRow.datarow("getValue");
                    thiz._chanpinDetailsModal.edit(function(value){
                        selectedRow.datarow("setValue", value);
                    }, rowValue);
                    return false;
                });
                this._bindEvent();
            },
            _bindEvent: function(){
                var thiz = this;
                this._kehuInput.changed(function(input, value){
                    thiz.setValue({
                        jiekuanFangshi: value.jiekuanFangshi, shouhuoren: value.shouhuoren,
                        shouhuorenDianhua: value.shouhuorenDianhua, shouhuoDizhi: value.shouhuoDizhi
                    });
                    thiz._chanpinDetailsModal.setKehu(value);
                });
            },
            validate: function(){
                return this._form.validate();
            },
            getValue: function(){
                return this._form.getValue();
            },
            setValue: function(value){
                this._form.setValue(value);
            }
        }
    );
})(jQuery);
(function($){
    $.widget("ui.chanpinDetailsModal", {
            options: {
                
	        },
            _create: function(){
                this._modal = this.element.modal({ show: false, backdrop: "static" }).data("bs.modal");
	            this._form = this.element.find("form").form().data("form");
                this._chanpinInput = this._form.getInput("chanpin");
                this._bindEvent();
            },
            _bindEvent: function(){
                var thiz = this;
                this._form.changed(function(input, value){
                    var formValue = thiz._form.getValue();
                    var numberInputs = ["shuliang", "tongshu", "xiaoshouDanjia", "xiaoshouDijia", "zongjine", "yewuwei", "shijiDanjia", "zongjine", "ticheng", "butie", "yewufei", "yewulv"]
                    $.each(numberInputs, function(){
                        if(formValue[this] == null){
                            formValue[this] = 0;
                        }
                    })
                    if(formValue.shifouKaipiao == null){
                        formValue.shifouKaipiao = false;
                    }
                    $.post("/Dingdan/JisuanDingdanMingxi", {argsJson: $.toJSON(formValue)}, function(model){
                        if(model.result){
                            var mingxi = model.dingdanMingxi;
                            thiz._form.setValue({shijiDanjia: mingxi.shijiDanjia, zongjine: mingxi.zongjine,
                                ticheng: mingxi.ticheng, butie: mingxi.butie, shoukuanJine: mingxi.shoukuanJine, 
                                yewufei: mingxi.yewufei});
                        }else{
                            $.messageBox.info(model.message);
                        }
                    });
                });
                this._chanpinInput.changed(function(input, chanpin){
                    var defaultValue = {guige: chanpin.guige, danwei: chanpin.danwei, xiaoshouDijia: chanpin.xiaoshouDijia};
                    if(thiz._kehu){
                        $.extend(defaultValue, {yewulv: thiz._kehu.yewulv, yewulvFangshi: thiz._kehu.yewulvFangshi});
                    }
                    thiz.setValue(defaultValue);
                });
                this.element.find(".btnOk").click(function(){
                    if(thiz._form.validate()){
                        var value = thiz._form.getValue();
                        thiz._callback(value);
                        thiz.hide();
                    }
                    return false;
                });
            },
            validate: function(){
                return this._form.validate();
            },
            getValue: function(){
                return this._form.getValue();
            },
            setValue: function(value){
                this._form.setValue(value);
            },
            edit: function(callback, value){
                this._callback = callback;
                this._modal.show();
                this.setValue(value);
            },
            add: function(callback){
                this._callback = callback;
                this._modal.show();
                this._form.reset();
            },
            hide: function(){
                this._modal.hide();
            },
            setKehu: function(kehu){
                this._kehu = kehu;
            }
        }
    );
})(jQuery);