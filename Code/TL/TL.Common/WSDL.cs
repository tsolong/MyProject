using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace TL.Common
{
    /// <summary>
    /// 动态调用WEBSERVICES
    /// </summary>
    public class WSDL
    {
        private object agent;
        private Type agentType;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">WebServices地址</param>
        /// <param name="Code_Namespace">WebServices命名空间</param>
        public WSDL(string url, string Code_Namespace)
        {
            XmlTextReader reader = new XmlTextReader(url + "?wsdl");

            //创建和格式化 WSDL 文档
            ServiceDescription sd = ServiceDescription.Read(reader);

            //创建客户端代理代理类
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);

            //使用 CodeDom 编译客户端代理类
            CodeNamespace cn = new CodeNamespace(Code_Namespace);
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            CompilerResults cr = icc.CompileAssemblyFromDom(cp, ccu);
            agentType = cr.CompiledAssembly.GetTypes()[0];
            agent = Activator.CreateInstance(agentType);
        }

        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="methodName">方法名，大小写敏感</param>
        ///<param name="args">参数，按照参数顺序赋值</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }

        ///<summary>
        ///调用指定方法
        ///</summary>
        ///<param name="method">方法信息</param>
        ///<param name="args">参数，按照参数顺序赋值</param>
        ///<returns>Web服务的返回值</returns>
        private object Invoke(MethodInfo method, params object[] args)
        {
            return method.Invoke(agent, args);
        }

        private MethodInfo[] Methods
        {
            get
            {
                return agentType.GetMethods();
            }
        }
    }
}
