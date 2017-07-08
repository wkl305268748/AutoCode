using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;

namespace SpringBoot.Auto
{
    public class AutoController : AutoClass
    {

        public static void CreateController(DbTable table, string package, string path)
        {
            string model_class = toFirstUp(table.getClassName());
            string model_value = toFirstLow(table.getClassName());
            string service_class = toFirstUp(table.getClassName() + "Service");
            string service_value = toFirstLow(table.getClassName() + "Service");

            //创建Controller类
            JavaClass javaClass = new JavaClass(toFirstUp(table.getClassName() + "Controller"), package);
            //头文件
            javaClass.AddImpoert("org.springframework.beans.factory.annotation.Autowired")
                .AddImpoert("io.swagger.annotations.*")
                .AddImpoert("org.springframework.web.bind.annotation.*")
                .AddImpoert(string.Format("{0}.json.JsonBean", package))
                .AddImpoert(string.Format("{0}.json.response.PageResponse", package))
                .AddImpoert(string.Format("{0}.exception.ErrorCodeException", package))
                .AddImpoert(string.Format("{0}.exception.ErrorCode", package))
                .AddImpoert(string.Format("{0}.model.{1}", package, model_class))
                .AddImpoert(string.Format("{0}.service.{1}", package, service_class));
            //添加注解
            javaClass
                .AddAnnotation(string.Format("@Api(value = \"/v1/{0}\", description = \"{1}\")",table.Name,table.Notes))
                .AddAnnotation(string.Format("@RequestMapping(value = \" /v1/{0}\")", table.getClassLowName()))
                .AddAnnotation("@RestController")
                .AddValue("@Autowired", service_class, service_value);

            //Insert函数
            //--------------------
            JavaMethod insert = javaClass.AddMethord("Insert", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation(string.Format("@ApiOperation(value = \"增加{0}\")", table.Name))
                .addAnnotation("@RequestMapping(value = \"\",method = RequestMethod.POST)")
                .addAnnotation("@ResponseBody");
            string param = "";
            int index = 0;
            foreach (DbColumn col in table.Column)
            {
                if (!col.IsKey)
                {
                    insert.addParam(string.Format("@ApiParam(value = \"{0}\",required = {1})@RequestParam(value = \"{2}\",required = {1})", col.Notes, col.IsNull.ToString().ToLower(), col.Name), col.getJavaTyep(), col.Name);
                    if (index == 0)
                        param += col.Name;
                    else
                        param += "," + col.Name;
                    index++;
                }
            }
            insert.addLogicNo(string.Format("return new JsonBean(ErrorCode.SUCCESS, {0}.insert({1}));", service_value, param));


            //update函数
            //------------------
            JavaMethod update = javaClass.AddMethord("update", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation(string.Format("@ApiOperation(value = \"修改指定的{0}\")", table.Name))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.PUT)", table.getColumnKey().Name))
                .addAnnotation("@ResponseBody");
            param = "";
            index = 0;
            foreach (DbColumn col in table.Column)
            {
                if (col.IsKey)
                    update.addParam("@ApiParam(value = \"查询主键\", required = true)@PathVariable()", col.getJavaTyep(),col.Name);
                else
                    update.addParam(string.Format("@ApiParam(value = \"{0}\",required = {1})@RequestParam(value = \"{2}\",required = {1})", col.Notes, col.IsNull.ToString().ToLower(), col.Name), col.getJavaTyep(), col.Name);
                if (index == 0) 
                    param += col.Name;
                else
                    param += "," + col.Name;
                index++;
            }
            update.addLogicException("ErrorCodeException")
                .addTryCode(string.Format("return new JsonBean(ErrorCode.SUCCESS, {0}.update({1}));", service_value, param))
                .addCatchCode("return new JsonBean(e.getErrorCode());");

            //selectByPrimaryKey函数
            //------------------
            JavaMethod selectByPrimaryKey = javaClass.AddMethord("selectByPrimaryKey", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation(string.Format("@ApiOperation(value = \"获取指定的{0}\")", table.Name))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.GET)", table.getColumnKey().Name))
                .addAnnotation("@ResponseBody");

            selectByPrimaryKey.addParam("@ApiParam(value = \"查询主键\", required = true)@PathVariable()", table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);
            selectByPrimaryKey.addLogicException("ErrorCodeException")
                .addTryCode(string.Format("return new JsonBean(ErrorCode.SUCCESS, {0}.selectByPrimaryKey({1}));", service_value, table.getColumnKey().Name))
                .addCatchCode("return new JsonBean(e.getErrorCode());");

            //selectPage函数
            //------------------
            JavaMethod selectPage = javaClass.AddMethord("selectPage", string.Format("JsonBean<PageResponse<{0}>>", model_class))
                .addAnnotation(string.Format("@ApiOperation(value = \"列出所有的{0}\")", table.Name))
                .addAnnotation(string.Format("@RequestMapping(value = \"/page\",method = RequestMethod.GET)"))
                .addAnnotation("@ResponseBody")
                .addParam("@ApiParam(value = \"从第几个开始列出\") @RequestParam(required = false, defaultValue = \"0\")", "Integer", "offset")
                .addParam("@ApiParam(value = \"每页内容数量\") @RequestParam(required = false, defaultValue = \"10\")", "Integer", "pageSize");

            selectPage.addLogicNo(string.Format("return new JsonBean(ErrorCode.SUCCESS, {0}.selectPage(offset,pageSize));",service_value));

           
            //deletePrimaryKey函数
            //------------------
            JavaMethod deletePrimaryKey = javaClass.AddMethord("deletePrimaryKey", string.Format("JsonBean"))
                .addAnnotation(string.Format("@ApiOperation(value = \"删除指定的{0}\")", table.Name))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.DELETE)", table.getColumnKey().Name))
                .addAnnotation("@ResponseBody");

            deletePrimaryKey.addParam("@ApiParam(value = \"查询主键\", required = true)@PathVariable()", table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);
            deletePrimaryKey.addLogicNo(string.Format("return new JsonBean(ErrorCode.SUCCESS, {0}.DeleteAdmin(id));", service_value));

            //输出Controller
            WriteFile(path + "\\controller", toFirstUp(table.getClassName() + "Controller") + ".java", javaClass.toListString());

        }
    }
}
 