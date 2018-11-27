using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
using System.Collections;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Entity;

namespace Common
{
    public class OfficeHelper
    {



       /// <summary>  
/// 将Excel文件中的数据读出到DataTable中(xlsx)  
/// </summary>  
/// <param name="file">文件路径</param>  
/// <returns>DataTable</returns>  
public static DataTable ExcelToTableForXLSX(string file)
        {
            DataTable dt = new DataTable();
          
          
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                IWorkbook xssfworkbook = WorkbookFactory.Create(fs);
                //XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheetAt(0);

                // 表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        // continue;  
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }

                // 数据  
                for (int i = sheet.FirstRowNum + 2; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    for (int j = row.FirstCellNum; j < columns.Count; ++j)
                    {
                        dr[j] = GetValueTypeForXLSX(row.GetCell(j) as XSSFCell);
                        var d = dr[j];
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static Tuple<List<AStudent>,List<AStudentPay>> ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
			List<AStudent> AS = new List<AStudent>();
			List<AStudentPay> PAY = new List<AStudentPay>();
			//定义要返回的datatable对象
			DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
			
				//根据指定路径读取文件
				FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                ////如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {

					int rowCount = sheet.LastRowNum+1;
					if (rowCount>2)
					{
						for (int i = 2; i <= rowCount; ++i)
						{
							AStudent aStudent = new AStudent();
							AStudentPay aStudentPay = new AStudentPay();
							IRow row = sheet.GetRow(i);
							if (row == null) continue; //没有数据的行默认是null　　
							try
							{
								aStudent.Name = row.GetCell(0).ToString();
								aStudent.Cards = row.GetCell(1).ToString();
								aStudent.TeacherID = row.GetCell(2).ToString();
								aStudent.GradeID = row.GetCell(3).ToString();
								aStudent.SchoolID = row.GetCell(4).ToString();
								aStudent.PhoneNum = row.GetCell(5).ToString();

								aStudentPay.PayDateString = row.GetCell(6).ToString();
								aStudentPay.PaymentID = row.GetCell(7).ToString();
								aStudentPay.CollectionID = row.GetCell(8).ToString();

								if (row.GetCell(9) != null)
								{
									aStudentPay.xa = Convert.ToDecimal(row.GetCell(9).NumericCellValue);
								}
								else
								{
									aStudentPay.xa = 0;
								}
								if (row.GetCell(10) != null)
								{
									aStudentPay.xb = Convert.ToDecimal(row.GetCell(10).NumericCellValue);
								}
								else
								{
									aStudentPay.xb = 0;
								}
								if (row.GetCell(11) != null)
								{
									aStudentPay.xc = Convert.ToDecimal(row.GetCell(11).NumericCellValue);
								}
								else
								{
									aStudentPay.xc = 0;
								}

								if (row.GetCell(12) != null)
								{
									aStudentPay.za = Convert.ToDecimal(row.GetCell(12).NumericCellValue);
								}
								else
								{
									aStudentPay.za = 0;
								}
								if (row.GetCell(13) != null)
								{
									aStudentPay.zb = Convert.ToDecimal(row.GetCell(13).NumericCellValue);
								}
								else
								{
									aStudentPay.zb = 0;
								}
								if (row.GetCell(14) != null)
								{
									aStudentPay.zc = Convert.ToDecimal(row.GetCell(14).NumericCellValue);
								}
								else
								{
									aStudentPay.zc = 0;
								}
								if (row.GetCell(15) != null)
								{
									aStudentPay.wa = Convert.ToDecimal(row.GetCell(15).NumericCellValue);
								}
								else
								{
									aStudentPay.wa = 0;
								}
								if (row.GetCell(16) != null)
								{
									aStudentPay.wb = Convert.ToDecimal(row.GetCell(16).NumericCellValue);
								}
								else
								{
									aStudentPay.wb = 0;
								}
								if (row.GetCell(17) != null)
								{
									aStudentPay.wc = Convert.ToDecimal(row.GetCell(17).NumericCellValue);
								}
								else
								{
									aStudentPay.wc = 0;
								}
								AS.Add(aStudent);
								PAY.Add(aStudentPay);
							}
							catch (Exception ex)
							{
								LogHelp.Error("导入excel出错行"+i+"="+ex.Message);
							}
							
						}
					}
                    
                
                }
                return new Tuple<List<AStudent>, List<AStudentPay>>(AS,PAY);
            }
            catch (Exception ex)
            {
				LogHelp.Error(ex.Message);
            }
			return new Tuple<List<AStudent>, List<AStudentPay>>(AS, PAY);
		}

        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        /// <summary>
        /// 将文件流读取到DataTable数据表中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadStreamToDataTable(Stream fileStream, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString().Trim();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
