# SQL通用客户端

## 功能描述

​			通用SQL客户端通过提供统一的GUI访问界面，这些界面获取数据可以调用标准的DAL(Data Access Layer)接口定义来实现不同数据库的数据适配，也就是说用户只要按照DAL定义的数据标准，实现相关的接口，就可以使用SQLClient来展示新型数据库中的表，视图，索引等。
    新增SQLClient登录控制,人脸识别自动登录(AForge, libarcsoft)

## SQLClient程序结构

### 	SQLClient

​		SQLClient主程序，GUI实现与集成。

​		左侧为TreeView用来展示所有的数据库连接，数据库管理，表管理，视图管理，查询脚本管理（本地）

​		右侧为数据库内容展示区域，包括查看表结构，表内容，视图内容，新建查询文本框，图形化创表，修改表

### 	SQLDAL

​				SQLClient数据适配的接口

### 	SQLUserControl

​				SQLClient用户组件的GUI实现，以及事件定义。

### 	SQLHelper

​				SQLClient的帮助类，提供日志管理，DAL用户动态库反射功能

### 	ICSharpCode.TextEditor

​				SQLClient查询编辑器使用到开源TextBox的语法高亮，代码折叠，代码提示功能

### 	FaceDetection

​				SQLClient登录人脸识别功能，使用到了虹软的免费工具包

### 	OracleDAL

​				Oracle数据库的数据适配实现

### 	MySQLDAL

​				MySQL数据库的数据适配实现

### 	SQLServerDAL

​				SQLServer数据库的数据适配实现

### 	PostgreSQLDAL

​				PostgresQL数据库的数据适配实现

### 	SQLiteDAL

​			SQLite数据库的数据适配实现

### 	AccessDAL

​			Access数据库的数据适配实现
