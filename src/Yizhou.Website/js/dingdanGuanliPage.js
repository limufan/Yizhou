(function($){
    $.widget("ui.dingdanGuanliPage", {
            options: {
                
	        },
	        _create: function(){
                var thiz = this;
                this._searchPopover = $("#searchPopover");
                this._searchForm = this._searchPopover.find("form").form().data("form");
                this._searchPopoverTrigger = $("#btnPopoverSearch").popoverTrigger({popover: this._searchPopover}).data("popoverTrigger");
                this._dingdanGrid = $("#dingdanGrid").datagrid({
                    columns:[
			            {title: "订单号", width: 100, field:"danhao", name:"danhao", render: function(datarow, args){ 
                            return $("<a></a>").text(args.value).attr("href", $.resolveUrl("Dingdan/Edit", {id: args.data.id})) ;
                        }},
			            {title: "业务员", width: 80, field:"yewuyuan"},
			            {title: "客户名称", width: "50%", field:"kehu"},
			            {title: "下单日期", width: 80, field:"xiadanRiqi", render: "date"},
			            {title: "发货日期", width: 80, field:"fahuoRiqi", render: "date"},
			            {title: "结款日期", width: 80, field:"jiekuanRiqi", render: "date"},
			            {title: "应收款金额", width: 80, field:"yingshoukuanJine", name:"yingshoukuanJine", render: "number2"},
			            {title: "已收款金额", width: 80, field:"yishoukuanJine", name:"yishoukuanJine", render: "number2"},
			            {title: "未收款金额", width: 80, field:"weishoukuanJine", name:"weishoukuanJine", render: "number2"},
			            {title: "收货地址", width: "50%", field:"shouhuoDizhi"}
                    ],
                    height: "auto"
                }).data("datagrid");
        
                this._dingdanGridPager = $("#dingdanGridPager").pager({change: function(event, args){
                        thiz.search(args.start, thiz._searchInfo);
                    }}).data("pager");
                this._btnShengchengChuhuodan = $("#btnShengchengChuhuodan");
        
                this._bindEvent();
                this.search(0);
	        },
            _bindEvent: function(){
                var thiz = this;
                $("#btnSearch").click(function(){
                    var keyword = $("#txtKeyword").val();
                    thiz.search(0, {keyword: keyword});
                    return false;
                });
                $("#btnDelete").click(function(){
                    var rows = thiz._dingdanGrid.getSelectedRows();
                    if(!rows.length){
                        $.messageBox.info("请选择要删除的订单");
                        return false;
                    }
                    if(!confirm("确实要删除吗?")){
                        return false;
                    }
                    var dingdanIdArray = $.map(rows, function(row){
                        return row.datarow("getValue").id;
                    })
                    $.post("/Dingdan/Delete", { dingdanId: dingdanIdArray.toString() }, function(model){
                        if(model.result){
                            thiz._dingdanGrid.deleteSelectedRows();
                        }
                        else{
                            $.messageBox.info(model.message);
                        }
                    });
                    return false;
                });
                $("#btnExport").click(function(){
                    thiz.exportToExcel();
                    return false;
                });
                this._btnShengchengChuhuodan.click(function(){
                    var rows = thiz._dingdanGrid.getSelectedRows();
                    if(!rows.length){
                        $.messageBox.info("请先选择订单！");
                        return false;
                    }
                    $.each(rows, function(){
                        var dingdanId = this.datarow("getValue").id;
                        thiz.shengchengChuhuodan(dingdanId);
                    })
                    return false;
                });
                this._searchPopover.find(".btnOk").click(function(){
                    var searchValue = thiz._searchForm.getValue();
                    thiz._searchPopover.hide();
                    thiz.search(0, searchValue);
                });
                this._searchPopover.find(".btnCancel").click(function(){
                    thiz._searchPopover.hide();
                });
                this._searchPopover.find(".btnReset").click(function(){
                    thiz._searchForm.reset();
                });
            },
            search: function(start, searchInfo){
                var thiz = this;
                this._searchInfo = searchInfo;
                var args =  {start: start, size: 20};
                $.extend(args, this._searchInfo);
                $.get($.resolveUrl("Dingdan/GetDingdanList"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        thiz._dingdanGrid.setValue(model.dingdanList);
                        var footer = [
                                {columnName: "danhao", valueType: "fixed", value: "合计"},
                                {columnName: "yingshoukuanJine", valueType: "fixed", value: model.yingshoukuanJineSum},
                                {columnName: "yishoukuanJine", valueType: "fixed", value: model.yishoukuanJineSum},
                                {columnName: "weishoukuanJine", valueType: "fixed", value: model.weishoukuanJineSum}
                            ];
                        thiz._dingdanGrid.setFooter(footer);
                        thiz._dingdanGridPager.setPageInfo({start: start, size: 20, count: model.totalCount});
                    }
                    else{
                        alert(model.message);
                    }
                });
            },
            exportToExcel: function(){
                var thiz = this;
                var args = {}; 
                $.extend(args, this._searchInfo);
                $.get($.resolveUrl("Dingdan/ExportToExcel"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        $.download("/Dingdan/Download?fileName=" + model.fileName);
                    }
                    else{
                        alert(model.message);
                    }
                });
            },
            shengchengChuhuodan: function(dingdanId){
                var thiz = this;
                $.download($.resolveUrl("Dingdan/ShengchengChuhuodan", {dingdanId: dingdanId}));
            }
        }
    );
})(jQuery);