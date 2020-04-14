Drawing 绘图模块；
Operating 操作模块；包括上提，下放等；
SetDrawing 设置绘图参数模块，框架设置，线道设置，绘图项设置；
DataProcess 数据处理；
DrawingElement：各个应用到的绘图结构。
DrawingDesign：设计数据，为全局应用；

CurveItem
TextItem
SymbolItem


Well井
DrawingDesign包含了绘图设计需要的结构，包括刻度尺，比例尺，井段等信息。

LJJSDrawing:录井解释绘图实现类；


WorkData：绘制录井曲线的生产数据；
DesignData：绘制录井曲线的定义数据，包括框架、绘图项等设计；


DataTable dt  = new DataTable();
dt.Columns.Add("Sex",typeof(string));
DataRow dr = dt.NewRow();dr["Sex"] ="男";
dt.Rows.Add(dr);DataRow dr1 =dt.NewRow();
dr1["Sex"]="女";dt.Rows.Add(dr1);
this.combobox1.DataSource = dt;
this.combobox1.DataTextField = "Sex";
this.combobox1.DataBind();