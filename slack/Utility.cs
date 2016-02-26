using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack
{


    public class Utility
    {


        public static dynamic TryGetProperty(dynamic dynamicObject, String PropertyName)
        {
            return TryGetProperty(dynamicObject, PropertyName, "");
        }


        public static dynamic TryGetProperty(dynamic dynamicObject, String PropertyName, dynamic Default)
        {
            try
            {
                if (!HasProperty(dynamicObject, PropertyName))
                {
                    return Default;
                }
                if (dynamicObject.GetType() == typeof(System.Web.Helpers.DynamicJsonObject))
                {
                    // good thing this type of documentation was easy to find
                    System.Web.Helpers.DynamicJsonObject obj = (System.Web.Helpers.DynamicJsonObject)dynamicObject;
                    Type scope = obj.GetType();
                    System.Dynamic.IDynamicMetaObjectProvider provider = obj as System.Dynamic.IDynamicMetaObjectProvider;
                    if (provider != null)
                    {
                        System.Linq.Expressions.ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(object));
                        System.Dynamic.DynamicMetaObject mobj = provider.GetMetaObject(param);
                        System.Dynamic.GetMemberBinder binder = (System.Dynamic.GetMemberBinder)Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, PropertyName, scope, new Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo[] { Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) });
                        System.Dynamic.DynamicMetaObject ret = mobj.BindGetMember(binder);
                        System.Linq.Expressions.BlockExpression final = System.Linq.Expressions.Expression.Block(
                            System.Linq.Expressions.Expression.Label(System.Runtime.CompilerServices.CallSiteBinder.UpdateLabel),
                            ret.Expression
                        );
                        System.Linq.Expressions.LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(final, param);
                        Delegate del = lambda.Compile();
                        return del.DynamicInvoke(obj);
                    }
                    else
                    {
                        return obj.GetType().GetProperty(PropertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(obj, null);
                    }
                }
                else if (dynamicObject.GetType() == typeof(System.Collections.IDictionary))
                {
                    return (Dictionary<String, object>)dynamicObject[PropertyName];
                }
                return Default;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not determine if dynamic object has property.", ex);
            }
        }


        public static dynamic HasProperty(dynamic dynamicObject, String PropertyName)
        {
            try
            {
                if (dynamicObject.GetType() == typeof(System.Web.Helpers.DynamicJsonObject))
                {
                    System.Web.Helpers.DynamicJsonObject obj = (System.Web.Helpers.DynamicJsonObject)dynamicObject;
                    foreach (String strName in obj.GetDynamicMemberNames())
                    {
                        if (strName == PropertyName)
                        {
                            return true;
                        }
                    }
                }
                else if (dynamicObject.GetType() == typeof(System.Collections.IDictionary))
                {
                    if (((IDictionary<String, object>)dynamicObject).ContainsKey(PropertyName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not determine if dynamic object has property.", ex);
            }
        }


    }


}
