﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpringBoot.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SpringBoot.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 package #package#.exception;
        ///
        ////**
        /// * #notes#
        /// */
        ///public class ErrorCode{
        ///
        ///    public static final ErrorCode SUCCESS = new ErrorCode(0, &quot;成功&quot;, true);
        ///
        ///    private boolean success;
        ///    private int code;
        ///    private String msg;
        ///
        ///    public ErrorCode(int code, String msg) {
        ///        this.code = code;
        ///        this.msg = msg;
        ///        this.success = false;
        ///    }
        ///
        ///    public ErrorCode(int code, String msg, boolean success) {
        ///        this.code = code;
        ///        this.msg = msg;
        ///        this.success  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ErrorCode {
            get {
                return ResourceManager.GetString("ErrorCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 package #package#.exception;
        ///
        ////**
        /// * 用户自定义异常
        /// * #notes#
        /// */
        ///public class ErrorCodeException extends Exception{
        ///
        ///    ErrorCode errorCode;
        ///    public static final ErrorCode PARAM_ERROR = new ErrorCode(1, &quot;参数错误&quot;);
        ///
        ///    public ErrorCodeException(ErrorCode errorCode){
        ///        this.errorCode = errorCode;
        ///    }
        ///
        ///    public ErrorCode getErrorCode() {
        ///        return errorCode;
        ///    }
        ///}
        /// 的本地化字符串。
        /// </summary>
        internal static string ErrorCodeException {
            get {
                return ResourceManager.GetString("ErrorCodeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 package #package#.json;
        ///
        ///
        ///import #package#.exception.ErrorCode;
        ///
        ////**
        /// * #notes#
        /// */
        ///public class JsonBean&lt;T&gt; {
        ///    int error_code;
        ///    boolean success;
        ///    String msg;
        ///    T data;
        ///    public JsonBean() {
        ///
        ///    }
        ///
        ///    public JsonBean(ErrorCode errorCode, T data) {
        ///        this.error_code = errorCode.getCode();
        ///        this.success = errorCode.isSuccess();
        ///        this.msg = errorCode.getMsg();
        ///        this.data = data;
        ///    }
        ///
        ///    public JsonBean(ErrorCode errorCode) {
        ///        this.err [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string JsonBean {
            get {
                return ResourceManager.GetString("JsonBean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 package #package#.json.response;
        ///
        ///
        ///import io.swagger.annotations.ApiModelProperty;
        ///
        ///import java.util.List;
        ///
        ////**
        /// * #notes#
        /// */
        ///public class PageResponse&lt;T&gt; {
        ///    @ApiModelProperty(&quot;总数&quot;)
        ///    private Integer total;
        ///    @ApiModelProperty(&quot;从第几个开始&quot;)
        ///    private Integer offset;
        ///    @ApiModelProperty(&quot;每页数量&quot;)
        ///    private Integer pageSize;
        ///    @ApiModelProperty(&quot;内容列表&quot;)
        ///    private List&lt;T&gt; item;
        ///
        ///    public PageResponse(){}
        ///
        ///    public Integer getTotal() {
        ///        return total;
        ///    }
        ///
        ///    pu [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string PageResponse {
            get {
                return ResourceManager.GetString("PageResponse", resourceCulture);
            }
        }
    }
}
