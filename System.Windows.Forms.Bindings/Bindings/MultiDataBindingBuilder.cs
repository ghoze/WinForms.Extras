﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public class MultiDataBindingBuilder
    {
        private readonly Control _control;
        private readonly string _controlPropertyName;
        private List<DataSourceMemeber> _parameters = new List<DataSourceMemeber>();
        private object _dataSourceNullValue;
        private ControlUpdateMode _controlUpdateMode = ControlUpdateMode.Never;
        private DataSourceUpdateMode _dataSourceUpdateMode = DataSourceUpdateMode.Never;
        private IFormatProvider _formatInfo;
        private string _formatString;
        private bool _formattingEnabled;
        private object _convertParameter;
        private IMultiValueConverter _converter;
        private CultureInfo _culture;

        /// <summary>
        /// 初始化 <see cref="MultiDataBindingBuilder"/> 新实例。
        /// </summary>
        /// <param name="control">控件。</param>
        /// <param name="controlPropertyName">属性。</param>
        public MultiDataBindingBuilder(Control control, string controlPropertyName)
        {
            _control = control;
            _controlPropertyName = controlPropertyName;
        }

        public MultiDataBindingBuilder SetParameters(params DataSourceMemeber[] parameters)
        {
            _parameters.AddRange(parameters);
            return this;
        }

        /// <summary>
        /// 设置控件更新模式。
        /// </summary>
        /// <param name="controlUpdateMode">更新模式。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetControlUpdateMode(ControlUpdateMode controlUpdateMode)
        {
            _controlUpdateMode = controlUpdateMode;
            return this;
        }

        /// <summary>
        /// 设置数据源更新模式。
        /// </summary>
        /// <param name="dataSourceUpdateMode">更新模式。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetDataSourceUpdateMode(DataSourceUpdateMode dataSourceUpdateMode)
        {
            _dataSourceUpdateMode = dataSourceUpdateMode;
            return this;
        }
        /// <summary>
        /// 设置数据源为空时的值。
        /// </summary>
        /// <param name="value">默认值。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder DataSourceNullValue(object value)
        {
            _dataSourceNullValue = value;
            return this;
        }

        /// <summary>
        /// 设置格式化信息。
        /// </summary>
        /// <param name="formatString">格式。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetFormatString(string formatString)
        {
            _formatString = formatString;
            return this;
        }

        /// <summary>
        /// 设置格式化信息。
        /// </summary>
        /// <param name="formatInfo">格式化信息。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetFormatInfo(IFormatProvider formatInfo)
        {
            _formatInfo = formatInfo;
            return this;
        }
        /// <summary>
        /// 设置是否对控件属性数据应用类型转换和格式设置。
        /// </summary>
        /// <param name="formattingEnabled">是否设置。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetFormattingEnabled(bool formattingEnabled)
        {
            _formattingEnabled = formattingEnabled;
            return this;
        }

        /// <summary>
        /// 设置值转换区域信息。
        /// </summary>
        /// <param name="culture">转换区域信息。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetCulture(CultureInfo culture)
        {
            _culture = culture;
            return this;
        }

        /// <summary>
        /// 设置值转换器。
        /// </summary>
        /// <param name="converter">转换器。</param>
        /// <param name="convertParameter">转换参数。</param>
        /// <param name="culture">转换区域信息。</param>
        /// <returns>返回设置完成后的 <see cref="MultiDataBindingBuilder"/>。</returns>
        public MultiDataBindingBuilder SetConverter(IMultiValueConverter converter, object convertParameter)
        {
            _converter = converter;
            _convertParameter = convertParameter;
            return this;
        }

        /// <summary>
        /// 返回构建绑定到控件后的绑定信息。
        /// </summary>
        /// <returns></returns>
        public MultiDataBinding Build()
        {
            var db = new MultiDataBinding(_controlPropertyName, _parameters)
                .SetControlUpdateMode(_controlUpdateMode)
                .SetDataSourceUpdateMode(_dataSourceUpdateMode)
                .SetDataSourceNullValue(_dataSourceNullValue)
                .SetFormattingEnabled(_formattingEnabled)
                .SetFormatInfo(_formatInfo)
                .SetFormatString(_formatString)
                .SetConverter(_converter, _convertParameter)
                .SetCulture(_culture);
            _control.DataBindings.Add(db);
            return db;
        }
    }
}