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
    /// ��̬����WEBSERVICES
    /// </summary>
    public class WSDL
    {
        private object agent;
        private Type agentType;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="url">WebServices��ַ</param>
        /// <param name="Code_Namespace">WebServices�����ռ�</param>
        public WSDL(string url, string Code_Namespace)
        {
            XmlTextReader reader = new XmlTextReader(url + "?wsdl");

            //�����͸�ʽ�� WSDL �ĵ�
            ServiceDescription sd = ServiceDescription.Read(reader);

            //�����ͻ��˴��������
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);

            //ʹ�� CodeDom ����ͻ��˴�����
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
        ///����ָ���ķ���
        ///</summary>
        ///<param name="methodName">����������Сд����</param>
        ///<param name="args">���������ղ���˳��ֵ</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }

        ///<summary>
        ///����ָ������
        ///</summary>
        ///<param name="method">������Ϣ</param>
        ///<param name="args">���������ղ���˳��ֵ</param>
        ///<returns>Web����ķ���ֵ</returns>
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
