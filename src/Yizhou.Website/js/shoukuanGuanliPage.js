(function($){
    $.widget("ui.shoukuanGuanliPage", {
            options: {
                
	        },
	        _create: function(){
                var thiz = this;
                this._searchPopover = $("#searchPopover");
                this._searchForm = this._searchPopover.find("form").form().data("form");
                this._searchPopoverTrigger = $("#btnPopoverSearch").popoverTrigger({popover: this._searchPopover}).data("popoverTrigger");
                this._shoukuanGrid = $("#shoukuanGrid").datagrid({
                        columns:[
			                {title: "订单号", width: 80, field:"dingdanhao", name:"dingdanhao"},
			                {title: "业务员", width: 50, field:"yewuyuan"},
			                {title: "客户", width: 100, field:"kehu"},
			                {title: "下单日期", width: 80, field:"xiadanRiqi", render: "date"},
			                {title: "结款日期", width: 80, field:"jiekuanRiqi", render: "date"},
			                {title: "收款金额", width: 80, field:"shoukuanJine", name:"shoukuanJine", render: "number2"},
			                {title: "提成", width: 80, field:"ticheng", name: "ticheng", render: "number2"}
                        ],
                        height: "auto"
                    }).data("datagrid");
        
                this._shoukuanGridPager = $("#shoukuanGridPager").pager({change: function(event, args){
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
                $("#btnExport").click(function(){
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
                $.get($.resolveUrl("Shoukuan/GetShoukuanList"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        thiz._shoukuanGrid.setValue(model.shoukuanList);
                        var footer = [
                            {columnName: "dingdanhao", valueType: "fixed", value: "合计"},
                            {columnName: "shoukuanJine", valueType: "fixed", value: model.shoukuanJineSum},
                            {columnName: "ticheng", valueType: "fixed", value: model.tichengSum}
                        ]
                        thiz._shoukuanGrid.setFooter(footer);
                        thiz._shoukuanGridPager.setPageInfo({start: start, size: 20, count: model.totalCount})
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
                $.get($.resolveUrl("Shoukuan/ExportToExcel"), {argsJson : $.toJSON(args)}, function(model){
                    if(model.result){
                        $.download("/Shoukuan/Download?fileName=" + model.fileName);
                    }
                    else{
                        alert(model.message);
                    }
                });
            }
        }
    );
})(jQuery);