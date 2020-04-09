using GreenWhale.Application.SerialPorts;
using GreenWhale.BootLoader.Implements;
using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0
{
    public static class RequestContentExtension
    {

        /// <summary>
        /// 读取状态
        /// </summary>
        /// <param name="content"></param>
        /// <param name="resourceDictoary"></param>
        /// <param name="normalMode"></param>
        /// <param name="valueModel"></param>
        /// <returns></returns>
        public static bool Get(this RequestContent content,ISerialPortContext serialPortContext, ResourceStore resourceDictoary, Action<NormalModel> normalMode,Action<ValueModel> valueModel, IExportBoxService exportBoxService)
        {
            try
            {
                if (content.IsEnd && content.IsStart)
                {
                    if (!content.HasValue)
                    {
                        var nor = content.Content.ParseNormal();
                        if (nor == null)
                        {
                            return false;
                        }
                        if (resourceDictoary.Get(nor.Number) != null)
                        {
                            var dd = resourceDictoary.Get(nor.Number);
                            if (nor.ResourceState == ResourceState.E)
                            {
                                var failed = dd.OnFailed;
                                var NormalizationForm = new NormalModel() { Caption = failed.Caption, KeyName = failed.KeyName, Number = failed.Number, ResourceState = failed.ResourceState };
                                normalMode?.Invoke(NormalizationForm);
                            }
                            else
                            {
                                var failed = dd.OnSuccess;
                                var NormalizationForm = new NormalModel() { Caption = failed.Caption, KeyName = failed.KeyName, Number = failed.Number, ResourceState = failed.ResourceState };
                                normalMode?.Invoke(NormalizationForm);
                            }
                        }
                        else
                        {
                            serialPortContext.Clear();
                            exportBoxService.Log($"已定义的资源字典中不存在{content.Content}");
                        }
                    }
                    else
                    {
                        var nor = content.Content.ParseValue();
                        if (nor == null)
                        {
                            return false;
                        }
                        if (resourceDictoary.Get(nor.Number) != null)
                        {
                            var dd = resourceDictoary.Get(nor.Number);
                            if (nor.ResourceState == ResourceState.E)
                            {
                                var failed = dd.OnFailed;
                                if (dd.OnFailed.Value==null)
                                {
                                    exportBoxService.Log($"设计的资源字典中不存在值:{dd.Name}");
                                }
                                else
                                {
                                    var model = new ValueModel() { Caption = failed.Caption, KeyName = failed.KeyName, Number = failed.Number, ResourceState = failed.ResourceState, };
                                    model.Value = new ValueCompare(dd.OnFailed.Value.Key, nor.Value.Value);
                                    valueModel?.Invoke(model);
                                }
                            }
                            else
                            {
                                if (dd.OnSuccess.Value == null)
                                {
                                    serialPortContext.Clear();

                                    exportBoxService.Log($"设计的资源字典中不存在值:{dd.Name}");
                                }
                                else
                                {
                                    var success = dd.OnSuccess;
                                    var model = new ValueModel() { Caption = success.Caption, KeyName = success.KeyName, Number = success.Number, ResourceState = success.ResourceState };
                                    model.Value = new ValueCompare(dd.OnSuccess.Value.Key, nor.Value.Value);
                                    valueModel?.Invoke(model);
                                }

                            }
                        }
                        else
                        {
                            serialPortContext.Clear();
                            exportBoxService.Log($"已定义的资源字典中不存在{content.Content}");
                        }
                    }
                }
                return false;
            }
            catch (Exception err)
            {
                exportBoxService.Log(err.Message);
                return false;
            }

        }
        /// <summary>
        /// 解析为普通值
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static NormalModel ParseNormal(this string content)
        {
            var resource = content.ParseResource();
            var state = resource.ParseResourceState();
            if (state==null)
            {
                return null;
            }
            var number = resource.ParseResourceNumber();
            if (number==null)
            {
                return null;
            }
            return new NormalModel() {ResourceState= state.Value,Number=number.Value };
        }
        private static ValueModel ParseValue(this string content)
        {
            var resource = content.ParseResource();
            var state = resource.ParseResourceState();
            if (state == null)
            {
                return null;
            }
            var number = resource.ParseResourceNumber();
            if (number == null)
            {
                return null;
            }
            var value = resource.ParseResourceValue();
            if (value == null)
            {
                return null;
            }
            return new ValueModel() { ResourceState = state.Value, Number = number.Value, Value = new ValueCompare(string.Empty,value) };
        }
        private static int? ParseResourceNumber(this string content)
        {
            var temp = content.ParseResource();
            if (temp.Length>=4)
            {
              var nn=  temp.Substring(2, 2);
            if (int.TryParse(nn,out var dd))
            {
                return dd;
            }
            else
            {
                return null;
            }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 解析资源状态
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static ResourceState? ParseResourceState(this string content)
        {
            var temp = content.ParseResource();
            if (temp.Length != 1)
            {
               var nn=  temp.ToUpper().Substring(0,1);
                switch (nn)
                {
                    case "E":return ResourceState.E;
                    case "S":return ResourceState.S;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析资源值
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string ParseResourceValue(this string content)
        {
            return  content.Substring(5);//跳过+号
        }
        /// <summary>
        /// 去除无用信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string ParseResource(this string content)
        {
            var temp = content.Replace("<", string.Empty).Replace(">", string.Empty);
            return temp;

        }
    }
}
