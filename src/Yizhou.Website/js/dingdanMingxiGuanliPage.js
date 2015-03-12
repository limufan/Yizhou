(function($){
    $.widget("ui.dingdanMingxiGuanliPage", {
            options: {
                
	        },
	        _create: function(){
                var thiz = this;
                this._btnExport = $("#btnExport");
                this._searchPopover = $("#searchPopover");
                this._searchForm = this._searchPopover.find("form").form().data("form");
                this._searchPopoverTrigger = $("#btnPopoverSearch").popoverTrigger({popover: this._searchPopover}).data("popoverTrigger");
                var dingdanMingxiGridColumns = [
			        {title: "发货单号", width: 100, field:"fahuoDanhao", name:"fahuoDanhao"},
			        {title: "订单号", width: 80, field:"dingdanhao", name:"dingdanhao"},
			        {title: "业务员", width: 50, field:"yewuyuan"},
			        {title: "客户", width: "100%", field:"kehu"},
			        {title: "下单日期", width: 80, field:"xiadanRiqi", render: "date"},
			        {title: "产品名称", width: 80, field:"chanpin", name:"chanpin", render: "name"},
			        {title: "型号", width: 80, field:"xinghao", name:"xinghao"},
			        {title: "数量", width: 60, field:"shuliang", name: "shuliang"},
			        {title: "发货日期", width: 80, field:"fahuoRiqi", render: "date"},
			        {title: "发货数量", width: 70, field:"fahuoShuliang", name: "shuliang"},
			        {title: "销售单价", width: 70, field:"xiaoshouDanjia"},
			        {title: "销售底价", width: 70, field:"xiaoshouDijia"}
                ];
                if($.hasCaiwuQuanxian){
                    dingdanMingxiGridColumns = dingdanMingxiGridColumns.concat([
                        {title: "实际单价", width: 70, field:"shijiDanjia"},
			            {title: "总金额", width: 80, field:"zongjine", name:"zongjine", render: "number2"},
			            {title: "业务费", width: 70, field:"yewufei", name:"yewufei", render: "number2"}
                    ]);
                    this._btnExport.show();
                }
                this._dingdanMingxiGrid = $("#dingdanMingxiGrid").datagrid({
                        columns: dingdanMingxiGridColumns,
                        height: "auto"
                    }).data("datagrid");
                this._dingdanMingxiGridPager = $("#dingdanMingxiGridPager").pager({change: function(event, args){
                        thiz.search(args.start, thiz._searchInfo);
                    }}).data("pager");
        
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
                this._btnExport.click(function(){
                    thiz.exportToExcel();
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
                $.get($.resolveUrl("DingdanMingxi/GetDingdanMingxiList"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        thiz._dingdanMingxiGrid.setValue(model.dingdanMingxiList);
                        var footer = null;
                        if($.hasCaiwuQuanxian){
                            footer = [
                                {columnName: "dingdanhao", valueType: "fixed", value: "合计"},
                                {columnName: "zongjine", valueType: "fixed", value: model.zongjineSum},
                                {columnName: "yewufei", valueType: "fixed", value: model.yewufeiSum}
                            ]
                        }
                        thiz._dingdanMingxiGrid.setFooter(footer);
                        thiz._dingdanMingxiGridPager.setPageInfo({start: start, size: 20, count: model.totalCount})
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
                $.get($.resolveUrl("DingdanMingxi/ExportToExcel"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        $.download("/DingdanMingxi/Download?fileName=" + model.fileName);
                    }
                    else{
                        alert(model.message);
                    }
                });
            }
        }
    );
})(jQuery);