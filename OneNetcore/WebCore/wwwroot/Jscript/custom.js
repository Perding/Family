layui.define(['form', 'table'], function (exports) { //提示：模块也可以依赖其它模块，如：layui.define('layer', callback);
    var form = layui.form, table = layui.table;
    var obj = {
        custom: function () {
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
            table.on('checkbox(demo)', function (obj) {
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
        },
        customtabletr: function () {
            $(".layui-table tbody tr").click(function () {
                if ($(this).find("td:first").find("input[type='checkbox']").is(":checked")) {
                    $(this).attr("tp", "0");
                    $(this).attr("style", "background-color: inherit;");
                    $(this).find("td:first").find("input[type='checkbox']").prop("checked", false);;
                }
                else {
                    $(this).siblings().attr("style", "background-color: inherit;");
                    $(this).siblings().attr("tp", "0");
                    $(this).attr("style", "background-color:#fbec88;");
                    $(this).attr("tp", "1");
                    $(this).siblings().find("td:first").find("input[type='checkbox']").prop("checked", false);;
                    $(this).find("td:first").find("input[type='checkbox']").prop("checked", true);
                }
                form.render("checkbox");
            });
        }
        , dataFamt: function (cellval, formate) { //时间转换
            
            if (cellval === undefined || cellval == null || cellval == "null") {
                return "";
            }
            var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            if (date.getFullYear() == 1) {
                return "";
            }
            if (date.getFullYear() == "0001" || date.getFullYear() == "1900") {
                return "";
            }
            //var time = cellval.replace("/Date(", "").replace(")/", "");
            //var time_diff = new Date().getTime() - time;
            //// 计算相差天数
            //var days = Math.floor(time_diff / (24 * 3600 * 1000));
            //if (days == 0) {
            //    // 计算相差小时数
            //    var leave1 = time_diff % (24 * 3600 * 1000);
            //    var hours = Math.floor(leave1 / (3600 * 1000));
            //    if (hours == 0) {
            //        // 计算相差分钟数
            //        var leave2 = leave1 % (3600 * 1000);
            //        var minutes = Math.floor(leave2 / (60 * 1000));
            //        if (minutes > 0) {
            //            return minutes + '分钟前';
            //        }
            //        else {
            //            return '1分钟';
            //        }
            //    }
            //    else if (hours > 0 && hours < 3) {
            //        return hours + '小时前';
            //    }
            //}

            if (formate == "yyyy-MM-dd") {
                return date.getFullYear() + "-" + month + "-" + currentDate;
            }
            else if (formate == "yyyy年MM月dd日") {
                return date.getFullYear() + "年" + month + "月" + currentDate+"日";
            }
            else if (formate == "yyyy.MM") {
                return date.getFullYear() + "." + month;
            }
            else {
                var hour = date.getHours();
                if (hour < 10) {
                    hour = "0" + hour;
                }
                var min = date.getMinutes();
                if (min < 10) {
                    min = "0" + min;
                }
                var sec = date.getSeconds();
                if (sec < 10) {
                    sec = "0" + sec;
                }
                return date.getFullYear() + "-" + month + "-" + currentDate + " " + hour + ":" + min + ":" + sec;
            }
        }

    };
    //输出接口
    exports('mycustom', obj);
});