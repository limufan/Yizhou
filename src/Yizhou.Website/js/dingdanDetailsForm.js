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
                $("#chanpinGrid").datagrid({
                    columns:[
			            {title: "产品名称", width: 80, field:"chanpinName"},
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
			            {title: "是否开票", width: 80, field:"shifouKaipiao"}
                    ]
                });
                $("#shoukuanGrid").datagrid({
                    columns:[
			            {title: "收款日期", width: 80, field:"shoukuanRiqi", render: "date"},
			            {title: "收款金额", width: 80, field:"shoukuanJine"},
			            {title: "提成", width: 80, field:"ticheng"}
                    ]
                });
                $("#chanpinDetailsModal").modal({ show: false, backdrop: "static" });
	            this._chanpinDetailsModalForm = $("#chanpinDetailsModal form").form().data("form");
                this._chanpinDetailsModal = $("#chanpinDetailsModal").data("bs.modal");
                $("#btnAddChanpin").click(function(){
                    thiz._chanpinDetailsModal.show();
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
            }
        }
    );
})(jQuery);