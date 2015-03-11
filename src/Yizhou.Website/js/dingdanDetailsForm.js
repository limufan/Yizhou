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
                var chanpinGridColumns = [
			        {title: "产品名称", width: 100, field:"chanpin", name:"chanpin", render: "name"},
			        {title: "型号", width: 80, field:"xinghao"},
			        {title: "规格", width: 50, field:"guige"},
			        {title: "单位", width: 50, field:"danwei"},
			        {title: "数量", width: 80, field:"shuliang"},
			        {title: "发货数量", width: 80, field:"fahuoShuliang"},
			        {title: "销售单价", width: 80, field:"xiaoshouDanjia"},
			        {title: "销售底价", width: 80, field:"xiaoshouDijia"},
			        {title: "是否开票", width: 80, field:"shifouKaipiao", render: "shifou"}
                ];
                var chanpinGridFooter = null;
                if($.hasCaiwuQuanxian){
                    chanpinGridColumns = chanpinGridColumns.concat([
			            {title: "实际单价", width: 80, field:"shijiDanjia"},
			            {title: "总金额", width: 80, field:"zongjine", name:"zongjine", render: "number2"},
			            {title: "业务率", width: 80, field:"yewulv"},
			            {title: "业务率方式", width: 80, field:"yewulvFangshi"},
			            {title: "业务费", width: 80, field:"yewufei", name:"yewufei", render: "number2"}
                    ]);
                    chanpinGridFooter = [
                        {columnName: "chanpin", valueType: "fixed", value: "合计"}, 
                        {columnName: "zongjine", valueType:"sum"},
                        {columnName: "yewufei", valueType:"sum"}
                    ];
                }
                this._chanpinGrid = $("#chanpinGrid").datagrid({
                    columns: chanpinGridColumns,
                    required: true,
                    footer: chanpinGridFooter,
                    selectedRow: function(){
                        thiz._refreshChanpinGridToolbar();
                    },
                    unselectedRow: function(){
                        thiz._refreshChanpinGridToolbar();
                    }
                }).data("datagrid");
                this._form.setInput("mingxiList", this._chanpinGrid);
                this._shoukuanGrid = $("#shoukuanGrid").datagrid({
                    columns:[
			            {title: "收款日期", width: 80, field:"shoukuanRiqi", name:"shoukuanRiqi", render: "date"},
			            {title: "收款金额", width: 80, field:"shoukuanJine", name:"shoukuanJine"},
			            {title: "提成", width: 80, field:"ticheng", name:"ticheng", render: "number2"}
                    ],
                    footer:[
                        {columnName: "shoukuanRiqi", valueType: "fixed", value: "合计"}, 
                        {columnName:"shoukuanJine", valueType:"sum"},
                        {columnName:"ticheng", valueType:"sum"},
                    ],
                    selectedRow: function(){
                        thiz._refreshShoukuanGridToolbar();
                    },
                    unselectedRow: function(){
                        thiz._refreshShoukuanGridToolbar();
                    }
                }).data("datagrid");
                this._form.setInput("shoukuanList", this._shoukuanGrid);
                this._chanpinDetailsModal = $("#chanpinDetailsModal").chanpinDetailsModal().data("chanpinDetailsModal");
                this._shoukuanDetailsModal = $("#shoukuanDetailsModal").shoukuanDetailsModal({dingdanForm: this._form}).data("shoukuanDetailsModal");
                this._btnEditChanpin = $("#btnEditChanpin");
                this._btnDeleteChanpin = $("#btnDeleteChanpin");
                this._btnEditShoukuan = $("#btnEditShoukuan");
                this._btnDeleteShoukuan = $("#btnDeleteShoukuan");
                this._bindEvent();
            },
            _bindEvent: function(){
                var thiz = this;
                $("#btnAddChanpin").click(function(){
                    thiz._chanpinDetailsModal.add(function(value){
                        thiz._chanpinGrid.appendRow(value);
                    });
                    return false;
                });
                this._btnEditChanpin.click(function(){
                    var selectedRow = thiz._chanpinGrid.getSelectedRow();
                    var rowValue = selectedRow.datarow("getValue");
                    thiz._chanpinDetailsModal.edit(function(value){
                        selectedRow.datarow("setValue", value);
                    }, rowValue);
                    return false;
                });
                this._btnDeleteChanpin.click(function(){
                    $.messageBox.confirm("确实要删除吗?", function(){
                        thiz._chanpinGrid.deleteSelectedRows();
                    });
                    return false;
                });
                $("#btnAddShoukuan").click(function(){
                    thiz._shoukuanDetailsModal.add(function(value){
                        thiz._shoukuanGrid.appendRow(value);
                    });
                    return false;
                });
                this._btnEditShoukuan.click(function(){
                    var selectedRow = thiz._shoukuanGrid.getSelectedRow();
                    var rowValue = selectedRow.datarow("getValue");
                    thiz._shoukuanDetailsModal.edit(function(value){
                        selectedRow.datarow("setValue", value);
                    }, rowValue);
                    return false;
                });
                this._btnDeleteShoukuan.click(function(){
                    $.messageBox.confirm("确实要删除吗?", function(){
                        thiz._shoukuanGrid.deleteSelectedRows();
                    });
                    return false;
                });
                this._kehuInput.changed(function(input, value){
                    thiz.setValue({
                        jiekuanFangshi: value.jiekuanFangshi, shouhuoren: value.shouhuoren,
                        shouhuorenDianhua: value.shouhuorenDianhua, shouhuoDizhi: value.shouhuoDizhi
                    });
                    thiz._chanpinDetailsModal.setKehu(value);
                });
            },
            _refreshChanpinGridToolbar: function(){
                var selectedRows = this._chanpinGrid.getSelectedRows();
                this._btnEditChanpin.prop("disabled", selectedRows.length != 1);
                this._btnDeleteChanpin.prop("disabled", selectedRows.length == 0);
            },
            _refreshShoukuanGridToolbar: function(){
                var selectedRows = this._shoukuanGrid.getSelectedRows();
                this._btnEditShoukuan.prop("disabled", selectedRows.length != 1);
                this._btnDeleteShoukuan.prop("disabled", selectedRows.length == 0);
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
                if(!$.hasCaiwuQuanxian){
                    $(".chanpinCaiwuForm").hide();
                }
                this._bindEvent();
            },
            _bindEvent: function(){
                var thiz = this;
                this._form.changed(function(input, value){
                    var formValue = thiz._form.getValue();
                    var numberInputs = ["shuliang", "tongshu", "fahuoShuliang", "xiaoshouDanjia", "xiaoshouDijia", "zongjine", "yewuwei", "shijiDanjia", "zongjine", "ticheng", "butie", "yewufei", "yewulv"]
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
                    var defaultValue = {xinghao: chanpin.xinghao, guige: chanpin.guige, danwei: chanpin.danwei, xiaoshouDijia: chanpin.xiaoshouDijia};
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

(function($){
    $.widget("ui.shoukuanDetailsModal", {
            options: {
                dingdanForm: null
	        },
            _create: function(){
                this._dingdanForm = this.options.dingdanForm;
                this._modal = this.element.modal({ show: false, backdrop: "static" }).data("bs.modal");
	            this._form = this.element.find("form").form().data("form");
                this._bindEvent();
            },
            _bindEvent: function(){
                var thiz = this;
                this._form.inputing(function(input, value){
                    if(thiz._form.validate()){
                        var dingdan = thiz._dingdanForm.getValue();
                        var shoukuan = thiz._form.getValue();
                        shoukuan.ticheng = 0;
                        $.post("/Dingdan/JisuanShoukuanTicheng", {dingdanJson: $.toJSON(dingdan), shoukuanJson: $.toJSON(shoukuan)}, function(model){
                            if(model.result){
                                thiz._form.setValue({ticheng: model.ticheng});
                            }
                        });
                    }
                }, 500);
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