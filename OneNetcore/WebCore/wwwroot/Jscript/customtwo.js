layui.define(['form', 'table'], function (exports) { //提示：模块也可以依赖其它模块，如：layui.define('layer', callback);
    var form = layui.form, table = layui.table;
    var objtwo = {
        customtwo: function () {
            $(document).on("click", ".layui-table-body table.layui-table tbody tr", function () {
                var obj = event ? event.target : event.srcElement;
                var tag = obj.tagName;
                //获取已选中的数据
                var checkedlist = $(this).siblings().find("td div.laytable-cell-checkbox div.layui-form-checked I");
                if (checkedlist !== 0) {//取消前面选中的数据
                    checkedlist.click();
                }
                //当前行checkbox操作
                var checkbox = $(this).find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
                if (checkbox.length !== 0) {
                    //if (tag == 'DIV') {
                    checkbox.click();
                    //}
                }
            });
            table.on('checkbox(demoguid)', function (obj) {
                //判断是否选中，选中则添加背景色
                if (obj.checked) {
                    $(this).parents("tr").css("backgroundColor", "#fbec88");
                }
                else {
                    if (obj.data.PrintCount !== undefined && obj.data.PrintCount !== "" && obj.data.PrintCount !== "0") {
                        $(this).parents("tr").css("background-color", "rgb(241, 189, 189)");
                    }
                    else {
                        $(this).parents("tr").css("background-color", "");
                    }
                }
            });
            $(document).on("click", "td div.laytable-cell-checkbox div.layui-form-checkbox", function (e) {
                e.stopPropagation();
            });
        }
    };
    //输出接口
    exports('mycustomtwo', objtwo);
});